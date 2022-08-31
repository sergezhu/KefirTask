namespace Game.Code.Core.Move
{
	using Game.Code.Core.InternalTypes;

	public interface IMover
	{
		void Tick( float deltaTime );
		void StartMove();
		void EndMove();
		void StartCVRotation();
		void EndCVRotation();
		void StartCCVRotation();
		void EndCCVRotation();
		
		float Rotation { get; }
		Vector3Internal Position { get; }
		
		float Acceleration { get; set; }
		float Deceleration { get; set; }
		float RotationAcceleration { get; set; }
		float RotationDeceleration { get; set; }
		float MaxSpeed { get; set; }
		float MaxRotationSpeed { get; set; }
	}
}