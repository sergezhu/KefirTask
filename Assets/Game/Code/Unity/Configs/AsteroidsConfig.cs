namespace Game.Configs
{
	using UnityEngine;

	[CreateAssetMenu( fileName = "Asteroids", menuName = "Configs/Asteroids", order = (int) EConfig.Asteroids )]
	public class AsteroidsConfig : ScriptableObject
	{
		public float SafeRadiusAroundShip = 2;

		[Space]
		public float ScaleMin = 1;
		public float ScaleMax = 2;
		
		[Space]
		public float DestroyPartsMin = 3;
		public float DestroyPartsMax = 5;

		[Space]
		public float SpeedMin = 0.1f;
		public float SpeedMax = 2;
	}
}