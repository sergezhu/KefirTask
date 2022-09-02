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

	public class AsteroidPresenter : BasePresenter
	{
		private readonly Mover _mover;
		private readonly Rotator _rotator;
		private readonly AsteroidsConfig _asteroidsConfig;

		public AsteroidPresenter(AsteroidView view, Mover mover, Rotator rotator, AsteroidsConfig asteroidsConfig)
		{
			View             = view;
			
			_mover           = mover;
			_rotator         = rotator;
			_asteroidsConfig = asteroidsConfig;

			SetupMover();
			Subscribe();
			
			UpdateView();
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );
			_rotator.Tick( deltaTime );

			UpdateView();
		}

		public void StartMoveAlongDirection( Vector3 dir )
		{
			SetDirection( dir );
			_mover.StartMove();
		}

		private void Subscribe()
		{
			View.Collided += OnCollided;
		}

		private void Unsubscribe()
		{
			View.Collided -= OnCollided;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( info.OtherEntityType == EEntityType.Ship )
			{
				Unsubscribe();

				View.Destroy();
				_mover.OnDestroy();

				InvokeDestroy( new DestroyInfo() {Presenter = this, EntityType = View.Type} );
			}
			else
			{
				ChangeDirectionWhenCollision( info.OtherVelocity );
			}
		}

		private void UpdateView()
		{
			View.Position = _mover.Position.ToUnityVector3();
			View.Rotation = _rotator.CurrentRotation.ToUnityQuaternion();

			View.Velocity = _mover.Velocity.ToUnityVector3();
		}

		private void ChangeDirectionWhenCollision( Vector3 otherVelocity )
		{
			SetDirection( otherVelocity.normalized );
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir.ToNumericsVector3() );
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _asteroidsConfig.StartAcceleration;
			_mover.Deceleration         = 0;
			_mover.RotationAcceleration = _asteroidsConfig.RotationAcceleration;
			_mover.RotationDeceleration = _asteroidsConfig.RotationAcceleration;
			_mover.MaxSpeed             = _asteroidsConfig.RandomSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}