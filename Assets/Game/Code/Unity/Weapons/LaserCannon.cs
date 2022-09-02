namespace Game.Code.Unity.Weapons
{
	using Game.Code.Unity.Enums;

	public class LaserCannon : IWeapon
	{
		private readonly LaserCannonView _view;

		public LaserCannon(LaserCannonView view)
		{
			_view = view;
		}

		public EWeapon Type => EWeapon.LaserCannon;

		public void Shot()
		{
			throw new System.NotImplementedException();
		}
	}
}