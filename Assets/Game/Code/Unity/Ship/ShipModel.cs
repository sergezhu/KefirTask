namespace Game.Code.Unity.Ship
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class ShipModel : BaseModel
	{
		private readonly ShipView _view;
		private readonly MouseAndKeyboardControl _control;
		private readonly Mover _mover;
		private readonly ShipConfig _shipConfig;

		public ShipModel(ShipView view, MouseAndKeyboardControl control, Mover mover, ShipConfig shipConfig)
		{
			_view       = view;
			_control    = control;
			_mover      = mover;
			_shipConfig = shipConfig;

			SetupMover();
			SetupWeapons();
			Subscribe();
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );

			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 );

			_view.Velocity = _mover.Velocity.ToUnityVector3();
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _shipConfig.Acceleration;
			_mover.Deceleration         = _shipConfig.Deceleration;
			_mover.RotationAcceleration = _shipConfig.RotationAcceleration;
			_mover.RotationDeceleration = _shipConfig.RotationDeceleration;
			_mover.MaxSpeed             = _shipConfig.MaxSpeed;
			_mover.MaxRotationSpeed     = _shipConfig.MaxRotationSpeed;
		}

		private void SetupWeapons()
		{
		}

		private void Subscribe()
		{
			ControlsSubscribe();

			_view.Collided += OnCollided;
		}

		private void Unsubscribe()
		{
			ControlsUnsubscribe();

			_view.Collided -= OnCollided;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( IsDestroyed )
				return;
			
			if ( info.OtherEntityType == EEntityType.Asteroid ||
			     info.OtherEntityType == EEntityType.Enemy ||
			     info.OtherEntityType == EEntityType.AsteroidPart )
			{
				Unsubscribe();

				_view.Destroy();
				_mover.OnDestroy();
				
				InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type} );
			}
		}

		private void ControlsSubscribe()
		{
			_control.MoveStart      += OnMoveStart;
			_control.MoveEnd        += OnMoveEnd;
			_control.RotateCWStart  += OnRotateCWStart;
			_control.RotateCWEnd    += OnRotateCWEnd;
			_control.RotateCCWStart += OnRotateCCWStart;
			_control.RotateCCWEnd   += OnRotateCCWEnd;
		}

		private void ControlsUnsubscribe()
		{
			_control.MoveStart      -= OnMoveStart;
			_control.MoveEnd        -= OnMoveEnd;
			_control.RotateCWStart  -= OnRotateCWStart;
			_control.RotateCWEnd    -= OnRotateCWEnd;
			_control.RotateCCWStart -= OnRotateCCWStart;
			_control.RotateCCWEnd   -= OnRotateCCWEnd;
		}

		private void OnMoveStart() => _mover.StartMove();
		private void OnMoveEnd() => _mover.EndMove();
		private void OnRotateCWStart() => _mover.StartCVRotation();
		private void OnRotateCWEnd() => _mover.EndCVRotation();
		private void OnRotateCCWStart() => _mover.StartCCVRotation();
		private void OnRotateCCWEnd() => _mover.EndCCVRotation();
	}
}