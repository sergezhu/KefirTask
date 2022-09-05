namespace Game.Code.Core.Common
{
	using Game.Code.Common.Enums;

	public struct DestroyInfo
	{
		public BaseModel Model;
		public EEntityType EntityType;
		public bool HasBeenDestroyedByPlayerWeapon;
	}
}