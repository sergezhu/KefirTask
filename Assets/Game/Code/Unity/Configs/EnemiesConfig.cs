namespace Game.Code.Unity.Configs
{
	using Game.Code.Unity.Enums;
	using UnityEngine;

	[CreateAssetMenu( fileName = "Enemies", menuName = "Configs/Enemies", order = (int) EConfig.Enemies )]
	public class EnemiesConfig : ScriptableObject
	{
		public float SpawnDelay = 5;
		public float Acceleration = 5;
		[Range( 0, 1 )]
		public float SmoothDirection;

		[Space]
		public float SpeedMin = 0.1f;
		public float SpeedMax = 2;
		
		[Space]
		public float RotationAcceleration = 5f;

		public float RandomSpeed => Random.Range( SpeedMin, SpeedMax );
	}
}