namespace Game.Configs
{
	using UnityEngine;

	[CreateAssetMenu( fileName = "Ship", menuName = "Configs/Ship", order = (int) EConfig.Ship )]
	public class ShipConfig : ScriptableObject
	{
		public Vector3 StartPosition;
		
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