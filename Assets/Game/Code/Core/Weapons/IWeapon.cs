namespace Game.Code.Core.Weapons
{
	using Game.Code.Core.Common;
	using Game.Code.Core.Enums;

	public interface IWeapon : ITickable
	{
		EWeapon Type { get; }
		void Shot();
	}
}