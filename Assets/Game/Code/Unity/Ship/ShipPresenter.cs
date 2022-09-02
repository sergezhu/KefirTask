namespace Game.Code.Unity.Ship
{
	using System;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Utils;
	using Game.Configs;
	using UnityEngine;

	public class ShipPresenter : ITickable
	{
		public event Action Destroyed;
		
		private readonly ShipView _view;
		private readonly MouseAndKeyboardControl _control;
		private readonly Mover _mover;
		private readonly ShipConfig _shipConfig;

		private bool _isDestroyed;

		public ShipPresenter(ShipView view, MouseAndKeyboardControl control, Mover mover, ShipConfig shipConfig)
		{
			_view       = view;
			_control    = control;
			_mover      = mover;
			_shipConfig = shipConfig;

			SetupMover();
			Subscribe();
		}

		public void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );

			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 );

			_view.Velocity = _mover.Velocity.ToUnityVector3();
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
			if ( _isDestroyed )
				return;
			
			if ( info.OtherEntityType == EEntityType.Asteroid ||
			     info.OtherEntityType == EEntityType.Enemy ||
			     info.OtherEntityType == EEntityType.AsteroidPart )
			{
				Destroy();
			}
		}

		private void Destroy()
		{
			if(_isDestroyed)
				return;
			
			_isDestroyed = true;
			
			Unsubscribe();
			
			_view.OnDestroy();
			_mover.OnDestroy();
			
			Destroyed?.Invoke();
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

		private void SetupMover()
		{
			_mover.Acceleration         = _shipConfig.Acceleration;
			_mover.Deceleration         = _shipConfig.Deceleration;
			_mover.RotationAcceleration = _shipConfig.RotationAcceleration;
			_mover.RotationDeceleration = _shipConfig.RotationDeceleration;
			_mover.MaxSpeed             = _shipConfig.MaxSpeed;
			_mover.MaxRotationSpeed     = _shipConfig.MaxRotationSpeed;
		}

		private void OnMoveStart() => _mover.StartMove();
		private void OnMoveEnd() => _mover.EndMove();
		private void OnRotateCWStart() => _mover.StartCVRotation();
		private void OnRotateCWEnd() => _mover.EndCVRotation();
		private void OnRotateCCWStart() => _mover.StartCCVRotation();
		private void OnRotateCCWEnd() => _mover.EndCCVRotation();
	}
}