namespace Game.Code.Unity.Asteroids
{
	using System;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class AsteroidPresenter : ITickable
	{
		public event Action<DestroyInfo> Destroyed;
		
		private readonly AsteroidView _view;
		private readonly Mover _mover;
		private readonly Rotator _rotator;
		private readonly AsteroidsConfig _asteroidsConfig;

		public AsteroidPresenter(AsteroidView view, Mover mover, Rotator rotator, AsteroidsConfig asteroidsConfig)
		{
			_view            = view;
			_mover           = mover;
			_rotator         = rotator;
			_asteroidsConfig = asteroidsConfig;

			SetupMover();
		}

		private void Subscribe()
		{
			_view.Collided += OnCollided;
		}

		private void Unsubscribe()
		{
			_view.Collided -= OnCollided;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( info.OtherEntityType == EEntityType.Ship )
			{
				Unsubscribe();

				_view.OnDestroy();
				_mover.OnDestroy();

				Destroyed?.Invoke( new DestroyInfo() { EntityType =  _view.Type } );
			}
			else
			{
				ChangeDirectionAtNextFrame( info.OtherVelocity );
			}
		}

		private void ChangeDirectionAtNextFrame( Vector3 otherVelocity )
		{
			throw new NotImplementedException();
		}

		public void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );
			_rotator.Tick( deltaTime );

			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = _rotator.CurrentRotation.ToUnityQuaternion();

			_view.Velocity = _mover.Velocity.ToUnityVector3();
		}

		public void StartMoveAlongDirection( Vector3 dir )
		{
			_mover.SetDirection( dir.ToNumericsVector3() );
			_mover.StartMove();
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _asteroidsConfig.StartAcceleration;
			_mover.Deceleration         = 0;
			_mover.RotationAcceleration = 0;
			_mover.RotationDeceleration = 0;
			_mover.MaxSpeed             = _asteroidsConfig.RandomSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}