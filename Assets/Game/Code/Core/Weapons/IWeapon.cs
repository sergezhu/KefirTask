namespace Game.Code.Core.Weapons
{
	using Game.Code.Common.Enums;
	using Game.Code.Core.Common;

	public interface IWeapon : ITickable
	{
		EWeapon Type { get; }
		void Shot();
	}
}