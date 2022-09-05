namespace Game.Code.Core.Configs
{
	using Game.Code.Core.Enums;
	using UnityEngine;

	[CreateAssetMenu( fileName = "Asteroids", menuName = "Configs/Asteroids", order = (int) EConfig.Asteroids )]
	public class AsteroidsConfig : ScriptableObject
	{
		public float SpawnDelay = 5;
		public float StartAcceleration = 5;

		[Space]
		public int DestroyPartsMin = 3;
		public int DestroyPartsMax = 5;

		[Space]
		public float SpeedMin = 0.1f;
		public float SpeedMax = 1.5f;
		public float PartSpeedMin = 2f;
		public float PartSpeedMax = 3f;
		
		[Space]
		public float RotationSpeedMin = 0.25f;
		public float RotationSpeedMax = 2.25f;

		[Space]
		public float RotationAcceleration = 5f;

		public float RandomSpeed => Random.Range( SpeedMin, SpeedMax );
		public float RandomRotationSpeed => Random.Range( RotationSpeedMin, RotationSpeedMax );
		public int RandomDestroyParts => Random.Range( DestroyPartsMin, DestroyPartsMax + 1 );
		public float RandomPartSpeed => Random.Range( PartSpeedMin, PartSpeedMax );
	}
}