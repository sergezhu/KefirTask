namespace Game.Code.Core.Move
{
	using System.Numerics;
	using Game.Code.Core.InternalUtils;

	public class Mover : IMover
	{
		private const float  Epsilon = 0.001f;

		private float _desiredSpeed;
		private float _desiredRotationSpeed;
		private float _currentSpeed;
		private float _currentRotationSpeed;
		private float _cvDesiredRotationSpeed;
		private float _ccvDesiredRotationSpeed;
		
		private readonly float _directionSmooth;
		

		public float Acceleration { get; set; }
		public float Deceleration { get; set; }
		public float RotationAcceleration { get; set; }
		public float RotationDeceleration { get; set; }
		public float MaxSpeed { get; set; }
		public float MaxRotationSpeed { get; set; }
		public Vector3 Position { get; private set; }
		public float DesiredDirectionAngle { get; private set; }
		public float CurrentDirectionAngle { get; private set; }
	

		public Mover(Vector3 position, float directionAngle, float directionSmooth)
		{
			Position              = position;
			DesiredDirectionAngle = directionAngle;
			CurrentDirectionAngle = directionAngle;
			_directionSmooth      = directionSmooth;
		}

		public void Tick( float deltaTime )
		{
			UpdateMoveSpeed( deltaTime );
			UpdateRotationSpeed( deltaTime );
			UpdateDirectionAngle( deltaTime );
			UpdatePosition( deltaTime );
		}

		public void StartMove()
		{
			_desiredSpeed = MaxSpeed;
		}

		public void EndMove()
		{
			_desiredSpeed = 0;
		}

		public void StartCVRotation()
		{
			_cvDesiredRotationSpeed = MaxRotationSpeed;
			UpdateDesiredRotationDirection();
		}

		public void EndCVRotation()
		{
			_cvDesiredRotationSpeed = 0;
			UpdateDesiredRotationDirection();
		}

		public void StartCCVRotation()
		{
			_ccvDesiredRotationSpeed = -1 * MaxRotationSpeed;
			UpdateDesiredRotationDirection();
		}

		public void EndCCVRotation()
		{
			_ccvDesiredRotationSpeed = 0;
			UpdateDesiredRotationDirection();
		}

		private void UpdateDesiredRotationDirection()
		{
			_desiredRotationSpeed = _cvDesiredRotationSpeed + _ccvDesiredRotationSpeed;
		}

		private void UpdateMoveSpeed( float deltaTime )
		{
			if ( Math.Abs( _desiredSpeed - _currentSpeed ) < Epsilon )
			{
				_currentSpeed = _desiredSpeed;
			}
			else
			{
				var acc = _desiredSpeed > _currentSpeed
					? Acceleration
					: -1 * Deceleration;

				_currentSpeed += acc * deltaTime;
				_currentSpeed =  Math.Clamp( _currentSpeed, 0, MaxSpeed );
			}
		}

		private void UpdateRotationSpeed( float deltaTime )
		{
			if ( Math.Abs( _desiredRotationSpeed - _currentRotationSpeed ) < Epsilon )
			{
				_currentRotationSpeed = _desiredRotationSpeed;
			}
			else
			{
				var rotAcc = _desiredRotationSpeed > _currentRotationSpeed
					? RotationAcceleration
					: -1 * RotationDeceleration;

				_currentRotationSpeed += rotAcc * deltaTime;
				_currentRotationSpeed =  Math.Clamp( _currentRotationSpeed, -MaxRotationSpeed, MaxRotationSpeed );
			}
		}

		private void UpdateDirectionAngle( float deltaTime )
		{
			DesiredDirectionAngle += _currentRotationSpeed * deltaTime;
			CurrentDirectionAngle =  Math.Lerp( CurrentDirectionAngle, DesiredDirectionAngle, _directionSmooth );
		}

		private void UpdatePosition( float deltaTime )
		{
			var delta = _currentSpeed * deltaTime;
			Position += new Vector3( delta * Math.Sin( CurrentDirectionAngle ), 0, delta * Math.Cos( CurrentDirectionAngle ) );
		}
	}
}