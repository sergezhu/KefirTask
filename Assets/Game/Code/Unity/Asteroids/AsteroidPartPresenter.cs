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

	public class AsteroidPartPresenter : BasePresenter
	{
		public event Action<DestroyInfo> Destroyed;
		
		private readonly Mover _mover;
		private readonly Rotator _rotator;
		private readonly AsteroidsConfig _asteroidsConfig;

		public AsteroidPartPresenter(AsteroidView view, Mover mover, Rotator rotator, AsteroidsConfig asteroidsConfig)
		{
			View			 = view;
			
			_mover           = mover;
			_rotator         = rotator;
			_asteroidsConfig = asteroidsConfig;

			SetupMover();
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

				View.OnDestroy();
				_mover.OnDestroy();

				InvokeDestroy( new DestroyInfo() {Presenter = this, EntityType = View.Type} );
			}
			else
			{
				ChangeDirectionWhenCollision( info.OtherVelocity );
			}
		}

		private void ChangeDirectionWhenCollision( Vector3 otherVelocity )
		{
			SetDirection( otherVelocity.normalized );
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir.ToNumericsVector3() );
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );
			_rotator.Tick( deltaTime );

			View.Position = _mover.Position.ToUnityVector3();
			View.Rotation = _rotator.CurrentRotation.ToUnityQuaternion();

			View.Velocity = _mover.Velocity.ToUnityVector3();
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