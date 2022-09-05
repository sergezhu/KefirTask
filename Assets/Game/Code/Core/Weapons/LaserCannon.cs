namespace Game.Code.Core.Weapons
{
	using System.Collections.Generic;
	using System.Linq;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Enums;
	using UnityEngine;

	public class LaserCannon : IWeapon
	{
		private readonly LaserCannonView _view;
		private readonly ShipConfig _shipConfig;

		private RaycastHit[] _hits;
		private LaserCharge[] _charges;
		
		public EWeapon Type => EWeapon.LaserCannon;
		public IEnumerable<LaserCharge> Charges => _charges;

		public LaserCannon(LaserCannonView view, ShipConfig shipConfig)
		{
			_view = view;
			_shipConfig = shipConfig;

			_hits    = new RaycastHit[10];
			
			InitChargesArray();
			SetLaserSize();
		}

		public void Tick( float deltaTime )
		{
			foreach ( var charge in _charges )
				charge.Tick( deltaTime );
		}

		public void Shot()
		{
			if ( HasCharges() == false )
				return;

			var charge = GetFirstReadyCharge();
			charge.Recall();
			
			ShotInternal();
		}

		private void ShotInternal()
		{
			var ray = new Ray( _view.ShootPoint.position, _view.ShootPoint.forward );
			Physics.RaycastNonAlloc( ray, _hits, _shipConfig.LaserLength );

			Debug.DrawLine( ray.origin, ray.origin + ray.direction * _shipConfig.LaserLength, Color.red, 2f );

			_hits?
				.Where( hit => hit.transform != null && hit.transform.TryGetComponent<ILaserDamageableView>( out var otherView ) )
				.GroupBy( hit => hit.transform.gameObject )
				.Select( group =>
				{
					@group.Key.TryGetComponent<ILaserDamageableView>( out var otherView );
					return otherView;
				} )
				.ToList()
				.ForEach( view => view.ApplyLaserDamage() );

			_view.ShowLaser( _shipConfig.LaserShotDuration );
		}

		private void InitChargesArray()
		{
			_charges = new LaserCharge[_shipConfig.LaserCharges];

			for ( var i = 0; i < _charges.Length; i++ )
				_charges[i] = new LaserCharge( _shipConfig.LaserRecallDelay );
		}

		private void SetLaserSize()
		{
			var r = 0.5f * _shipConfig.LaserThickness;
			_view.SetLaserSize( new Vector3( r, r, _shipConfig.LaserLength ) );
		}

		private bool HasCharges()
		{
			return _charges.Count( c => c.IsReady.Value ) > 0;
		}

		private LaserCharge GetFirstReadyCharge()
		{
			return _charges.First( c => c.IsReady.Value );
		}
	}
}