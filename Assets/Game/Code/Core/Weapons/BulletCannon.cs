namespace Game.Code.Core.Weapons
{
	using System;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Move;
	using Game.Code.View.Weapons;

	public class BulletCannon : IWeapon
	{
		private readonly BulletCannonView _view;
		private readonly BulletViewFactory _bulletViewFactory;
		private readonly ShipConfig _shipConfig;
		private readonly BulletSystem _bulletSystem;

		public BulletCannon( BulletCannonView view, BulletViewFactory bulletViewFactory, ShipConfig shipConfig, BulletSystem bulletSystem )
		{
			_view              = view;
			_bulletViewFactory = bulletViewFactory;
			_shipConfig        = shipConfig;
			_bulletSystem      = bulletSystem;
		}

		public EWeapon Type => EWeapon.BulletCannon;
		
		public void Shot()
		{
			var view = _bulletViewFactory.Create();

			var bulletStartPos = _view.ShootPoint.position;
			var bulletStartDir = _view.ShootPoint.forward;
			var mover          = new Mover( bulletStartPos, bulletStartDir, 1 );
			var bullet         = new BulletModel( view, mover, _shipConfig );
			
			_bulletSystem.Add( bullet );
			bullet.StartMove();
			
			bullet.DestroyRequest += OnDestroyRequest;
		}

		public void Tick( float deltaTime )
		{
		}

		private void OnDestroyRequest( DestroyInfo info )
		{
			if ( info.Model is BulletModel bullet )
			{
				bullet.DestroyRequest -= OnDestroyRequest;
				_bulletSystem.Remove( bullet );
			}
			else
				throw new InvalidOperationException( $"You try handle not an bullet" );
		}
	}
}