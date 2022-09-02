namespace Game.Code.Unity.Weapons
{
	using System;
	using System.Collections.Generic;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Utils;

	public class BulletCannon : IWeapon
	{
		private readonly BulletCannonView _view;
		private readonly BulletViewFactory _bulletViewFactory;
		private readonly ShipConfig _shipConfig;

		private List<BulletModel> _bullets;

		public BulletCannon(BulletCannonView view, BulletViewFactory bulletViewFactory, ShipConfig shipConfig)
		{
			_view				= view;
			_bulletViewFactory	= bulletViewFactory;
			_shipConfig			= shipConfig;

			_bullets = new List<BulletModel>();
		}

		public EWeapon Type => EWeapon.BulletCannon;
		
		public void Shot()
		{
			var view = _bulletViewFactory.CreateBullet();

			var bulletStartPos = _view.ShootPoint.position.ToNumericsVector3();
			var bulletStartDir = _view.ShootPoint.forward.ToNumericsVector3();
			var mover          = new Mover( bulletStartPos, bulletStartDir, 1 );
			var bullet         = new BulletModel( view, mover, _shipConfig );
			
			_bullets.Add( bullet );
			bullet.StartMove();
			
			bullet.DestroyRequest += OnDestroyRequest;
		}

		public void Tick( float deltaTime )
		{
			_bullets.ForEach( b => b.Tick( deltaTime ) );
		}

		private void OnDestroyRequest( DestroyInfo info )
		{
			if ( info.Model is BulletModel bullet )
			{
				bullet.DestroyRequest -= OnDestroyRequest;
				_bullets.Remove( bullet );
			}
			else
				throw new InvalidOperationException( $"You try handle not an bullet" );
		}
	}
}