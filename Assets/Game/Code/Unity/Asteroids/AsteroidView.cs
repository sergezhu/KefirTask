namespace Game.Code.Unity.Asteroids
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;

	public class AsteroidView : BaseView
	{
		public override ECollisionLayer Layer => ECollisionLayer.Asteroid;
	}
}