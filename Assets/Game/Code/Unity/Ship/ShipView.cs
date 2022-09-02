namespace Game.Code.Unity.Ship
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Weapons;
	using UnityEngine;

	public class ShipView : BaseView
	{
		[SerializeField] private BulletCannonView _bulletCannonView;
		[SerializeField] private LaserCannonView _laserCannonView;
		
		public override EEntityType Type => EEntityType.Ship;

		public BulletCannonView BulletCannonView => _bulletCannonView;
		public LaserCannonView LaserCannonView => _laserCannonView;
	}
}