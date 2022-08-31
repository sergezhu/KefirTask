using Game.Code.Core.InternalTypes;
using Game.Code.Core.InternalUtils;
using Game.Code.Core.Move;

public class Mover : IMover
{
	private const float  Epsilon = 0.001f;
	
	private float _desiredSpeed;
	private float _desiredRotationSpeed;
	private float _currentSpeed;
	private float _currentRotationSpeed;

	public float Acceleration { get; set; }
	public float Deceleration { get; set; }
	public float RotationAcceleration { get; set; }
	public float RotationDeceleration { get; set; }
	public float MaxSpeed { get; set; }
	public float MaxRotationSpeed { get; set; }
	public Vector3Internal Position { get; private set; }
	public float Rotation { get; private set; }
	

	public Mover(Vector3Internal position, float rotation)
	{
		Position = position;
		Rotation = rotation;
	}

	public void Tick( float deltaTime )
	{
		UpdateSpeed( deltaTime );
		UpdateRotation( deltaTime );
		UpdatePosition( deltaTime );
	}

	public void SetDesiredSpeed( float speed )
	{
		_desiredSpeed = Math.Clamp(speed, 0, speed);
	}

	public void SetDesiredRotationSpeed( float speed )
	{
		_desiredRotationSpeed = speed;
	}

	private void UpdateSpeed( float deltaTime )
	{
		var acc = _desiredSpeed > _currentSpeed 
			? Acceleration 
			: Deceleration;

		_currentSpeed += acc * deltaTime;
		_currentSpeed =  Math.Clamp( _currentSpeed, 0, MaxSpeed );

		var rotAcc = _desiredRotationSpeed > _currentRotationSpeed
			? RotationAcceleration
			: RotationDeceleration;

		_currentRotationSpeed += rotAcc * deltaTime;
		_currentRotationSpeed =  Math.Clamp( _currentRotationSpeed, -MaxRotationSpeed, MaxRotationSpeed );
	}

	private void UpdateRotation( float deltaTime )
	{
		Rotation += _currentRotationSpeed * deltaTime; 
	}

	private void UpdatePosition( float deltaTime )
	{
		var delta = _currentSpeed * deltaTime;
		Position += new Vector3Internal( delta * Math.Cos( Rotation ), delta * Math.Sin( Rotation ), 0 );
	}
}