namespace Game.Code.Core.Asteroids
{
	using System;
	using Game.Code.Common;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Move;
	using Game.Code.View.Asteroids;
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
			Subscribe();
			
			UpdateView();
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
				Destroy( info.OtherEntityType == EEntityType.Bullet );
			}
			else
			{
				ChangeDirectionWhenCollision( info.OtherVelocity );
			}
		}

		private void OnLaserHit()
		{
			Destroy(true);
		}

		private void Destroy( bool hasBeenDestroyedByPlayerWeapon )
		{
			Unsubscribe();

			_view.Destroy();
			_mover.OnDestroy();

			InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type, HasBeenDestroyedByPlayerWeapon = hasBeenDestroyedByPlayerWeapon} );
		}

		private void ChangeDirectionWhenCollision( Vector3 otherVelocity )
		{
			SetDirection( otherVelocity.normalized );
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir );
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );
			_rotator.Tick( deltaTime );

			UpdateView();
		}

		private void UpdateView()
		{
			_view.Position = _mover.Position;
			_view.Rotation = _rotator.CurrentRotation;

			_view.Velocity = _mover.Velocity;
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