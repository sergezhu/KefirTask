namespace Game.Code.Unity.Configs
{
	using Game.Code.Unity.Enums;
	using UnityEngine;

	[CreateAssetMenu( fileName = "Enemies", menuName = "Configs/Enemies", order = (int) EConfig.Enemies )]
	public class EnemiesConfig : ScriptableObject
	{
		public float SafeRadiusAroundShip = 2;
		public float StartAcceleration = 5;

		[Space]
		public float ScaleMin = 1;
		public float ScaleMax = 2;

		[Space]
		public float SpeedMin = 0.1f;
		public float SpeedMax = 2;
		
		[Space]
		public float RotationSpeedMin = 0.25f;
		public float RotationSpeedMax = 2.25f;

		public float RandomScale => Random.Range( ScaleMin, ScaleMax );
		public float RandomSpeed => Random.Range( SpeedMin, SpeedMax );
		public float RandomRotationSpeed => Random.Range( RotationSpeedMin, RotationSpeedMax );
	}
}