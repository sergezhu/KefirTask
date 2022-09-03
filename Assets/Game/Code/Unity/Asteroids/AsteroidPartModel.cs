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

	public class AsteroidPartModel : BaseModel
	{
		public event Action<DestroyInfo> Destroyed;

		private readonly AsteroidPartView _view;
		private readonly Mover _mover;
		private readonly Rotator _rotator;
		private readonly AsteroidsConfig _asteroidsConfig;

		public AsteroidPartModel(AsteroidPartView view, Mover mover, Rotator rotator, AsteroidsConfig asteroidsConfig)
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
			_view.LaserHit += OnLaserHit;
		}

		private void Unsubscribe()
		{
			_view.Collided -= OnCollided;
			_view.LaserHit -= OnLaserHit;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( info.OtherEntityType == EEntityType.Ship || 
			     info.OtherEntityType == EEntityType.Bullet )
			{
				Destroy();
			}
			else
			{
				ChangeDirectionWhenCollision( info.OtherVelocity );
			}
		}

		private void OnLaserHit()
		{
			Destroy();
		}

		private void Destroy()
		{
			Unsubscribe();

			_view.Destroy();
			_mover.OnDestroy();

			InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type} );
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