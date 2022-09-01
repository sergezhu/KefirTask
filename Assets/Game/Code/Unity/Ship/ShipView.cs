namespace Game.Code.Unity.Ship
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;

	public class ShipView : BaseView
	{
		public override ECollisionLayer Layer => ECollisionLayer.Ship;
	}
}