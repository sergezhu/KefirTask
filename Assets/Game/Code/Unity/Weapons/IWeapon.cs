namespace Game.Code.Unity.Weapons
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;

	public interface IWeapon
	{
		EWeapon Type { get; }
		void Shot();
	}
}