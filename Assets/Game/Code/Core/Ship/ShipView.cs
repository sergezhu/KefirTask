namespace Game.Code.Core.Ship
{
	using Game.Code.Core.Common;
	using Game.Code.Core.Enums;
	using Game.Code.Core.Weapons;
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