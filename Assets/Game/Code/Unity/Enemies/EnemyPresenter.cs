namespace Game.Code.Unity.Enemies
{
	using System;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class EnemyPresenter : BasePresenter
	{
		private readonly Mover _mover;
		private readonly Mover _hero;
		private readonly EnemiesConfig _enemiesConfig;

		public EnemyPresenter(EnemyView view, Mover mover, Mover hero, EnemiesConfig enemiesConfig)
		{
			View             = view;
			
			_mover           = mover;
			_hero            = hero;
			_enemiesConfig   = enemiesConfig;

			SetupMover();
			Subscribe();
		}

		public override void Tick( float deltaTime )
		{
			SetToHeroDirection();
			
			_mover.Tick( deltaTime );

			View.Position = _mover.Position.ToUnityVector3();
			View.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 );

			View.Velocity = _mover.Velocity.ToUnityVector3();
		}

		public void StartMove( Vector3 dir )
		{
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

		private void ChangeDirectionWhenCollision( Vector3 otherVelocity )
		{
			SetDirection( otherVelocity.normalized );
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir.ToNumericsVector3() );
		}

		private void SetToHeroDirection()
		{
			var vector = (_hero.Position - _mover.Position).ToUnityVector3();
			vector.y = 0;
			vector = vector.normalized;
			
			SetDirection( vector );
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _enemiesConfig.StartAcceleration;
			_mover.Deceleration         = 0;
			_mover.RotationAcceleration = _enemiesConfig.RotationAcceleration;
			_mover.RotationDeceleration = _enemiesConfig.RotationAcceleration;
			_mover.MaxSpeed             = _enemiesConfig.RandomSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}