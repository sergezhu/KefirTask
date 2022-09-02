namespace Game.Code.Unity.Weapons
{
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public class LaserCannon : IWeapon
	{
		private readonly LaserCannonView _view;
		private readonly ShipConfig _shipConfig;

		public LaserCannon(LaserCannonView view, ShipConfig shipConfig)
		{
			_view = view;
			_shipConfig = shipConfig;

			var r = 0.5f * _shipConfig.LaserThickness;
			_view.SetLaserSize( new Vector3( r, r, _shipConfig.LaserLength ) );
		}

		public EWeapon Type => EWeapon.LaserCannon;

		public void Shot()
		{
			_view.ShowLaser( _shipConfig.LaserShotDuration );
		}
	}
}