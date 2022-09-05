namespace Game.Code.View.Ship
{
	using Game.Code.Common.Enums;
	using Game.Code.View.Common;
	using Game.Code.View.Weapons;
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