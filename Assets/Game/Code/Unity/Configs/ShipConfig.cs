namespace Game.Configs
{
	using Game.Code.Unity.Enums;
	using UnityEngine;

	[CreateAssetMenu( fileName = "Ship", menuName = "Configs/Ship", order = (int) EConfig.Ship )]
	public class ShipConfig : ScriptableObject
	{
		public Vector3 StartPosition;
		[Range(0, 1)] 
		public float SmoothDirection;

		[Space]
		public float MaxSpeed;
		public float Acceleration;
		public float Deceleration;

		[Space]
		public float MaxRotationSpeed;
		public float RotationAcceleration;
		public float RotationDeceleration;
	}
}