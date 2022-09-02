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

	public class EnemyPresenter : ITickable
	{
		public event Action<DestroyInfo> Destroyed;
		
		private readonly EnemyView _view;
		private readonly Mover _mover;
		private readonly Mover _hero;
		private readonly EnemiesConfig _enemiesConfig;

		public EnemyPresenter(EnemyView view, Mover mover, Mover hero, EnemiesConfig enemiesConfig)
		{
			_view            = view;
			_mover           = mover;
			_hero            = hero;
			_enemiesConfig   = enemiesConfig;

			SetupMover();
			Subscribe();
		}

		public void Tick( float deltaTime )
		{
			SetToHeroDirection();
			
			_mover.Tick( deltaTime );

			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 );

			_view.Velocity = _mover.Velocity.ToUnityVector3();
		}

		public void StartMove( Vector3 dir )
		{
			_mover.StartMove();
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
				
				Destroyed?.Invoke( new DestroyInfo(){ EntityType = _view.Type } );
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