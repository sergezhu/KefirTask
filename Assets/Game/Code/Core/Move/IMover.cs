namespace Game.Code.Core.Move
{
	using System.Numerics;

	public interface IMover
	{
		void Tick( float deltaTime );
		void StartMove();
		void EndMove();
		void StartCVRotation();
		void EndCVRotation();
		void StartCCVRotation();
		void EndCCVRotation();
		
		float DesiredDirectionAngle { get; }
		float CurrentDirectionAngle { get; }
		Vector3 Position { get; }
		
		float Acceleration { get; set; }
		float Deceleration { get; set; }
		float RotationAcceleration { get; set; }
		float RotationDeceleration { get; set; }
		float MaxSpeed { get; set; }
		float MaxRotationSpeed { get; set; }
	}
}