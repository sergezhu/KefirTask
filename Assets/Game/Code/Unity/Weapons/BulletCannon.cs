namespace Game.Code.Unity.Weapons
{
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

			var presenter = new BulletModel( view, mover, _shipConfig );
			
			_bullets.Add( presenter );
		}

		public void Tick( float deltaTime )
		{
			_bullets.ForEach( b => b.Tick( deltaTime ) );
		}
	}
}