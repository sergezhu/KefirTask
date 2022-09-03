namespace Game.Code.Unity.Weapons
{
	using System.Linq;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public class LaserCannon : IWeapon
	{
		private readonly LaserCannonView _view;
		private readonly ShipConfig _shipConfig;

		private RaycastHit[] _hits;

		public LaserCannon(LaserCannonView view, ShipConfig shipConfig)
		{
			_view = view;
			_shipConfig = shipConfig;

			_hits = new RaycastHit[10];

			SetLaserSize();
		}

		public EWeapon Type => EWeapon.LaserCannon;

		public void Shot()
		{
			var ray = new Ray( _view.ShootPoint.position, _view.ShootPoint.forward );
			Physics.RaycastNonAlloc( ray, _hits, _shipConfig.LaserLength );
			
			Debug.DrawLine( ray.origin, ray.origin + ray.direction * _shipConfig.LaserLength, Color.red, 2f );
			
			_hits?
				.Where( hit => hit.transform != null && hit.transform.TryGetComponent<ILaserDamageableView>( out var otherView ) )
				.GroupBy( hit => hit.transform.gameObject )
				.Select( group =>
				{
					group.Key.TryGetComponent<ILaserDamageableView>( out var otherView );
					return otherView;
				} )
				.ToList()
				.ForEach( view => view.ApplyLaserDamage() );

			_view.ShowLaser( _shipConfig.LaserShotDuration );
		}

		private void SetLaserSize()
		{
			var r = 0.5f * _shipConfig.LaserThickness;
			_view.SetLaserSize( new Vector3( r, r, _shipConfig.LaserLength ) );
		}
	}
}