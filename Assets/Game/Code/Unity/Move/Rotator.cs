namespace Game.Code.Unity.Move
{
	using UnityEngine;

	public class Rotator
	{
		private readonly float _rotationSpeed;
		private readonly Quaternion _rotationQuaternionSpeed;
		private readonly Quaternion _delta;

		public Quaternion CurrentRotation { get; private set; }

		public Rotator(Quaternion startRotation, Quaternion delta, float rotationSpeed )
		{
			CurrentRotation = startRotation;
			_rotationSpeed = rotationSpeed;
			_delta         = delta;
		}

		public void Tick( float deltaTime )
		{
			var desiredRotation = _delta * CurrentRotation;
			CurrentRotation = Quaternion.Slerp( CurrentRotation, desiredRotation, deltaTime * _rotationSpeed );
		}
	}
}