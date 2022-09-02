namespace Game.Code.Unity.Enemies
{
	using System;
	using System.Numerics;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Utils;
	using Game.Configs;
	using UnityEngine;
	using Quaternion = UnityEngine.Quaternion;
	using Vector3 = UnityEngine.Vector3;

	public class EnemyPresenter : ITickable
	{
		public event Action<DestroyInfo> Destroyed;
		
		private readonly EnemyView _view;
		private readonly Mover _mover;
		private readonly Mover _hero;
		private readonly AsteroidsConfig _asteroidsConfig;

		public EnemyPresenter(EnemyView view, Mover mover, Mover hero, AsteroidsConfig asteroidsConfig)
		{
			_view            = view;
			_mover           = mover;
			_hero            = hero;
			_asteroidsConfig = asteroidsConfig;

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
				ChangeDirectionAtNextFrame( info.OtherVelocity );
			}
		}

		private void ChangeDirectionAtNextFrame( Vector3 otherVelocity )
		{
			throw new System.NotImplementedException();
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
			_mover.Acceleration         = _asteroidsConfig.StartAcceleration;
			_mover.Deceleration         = 0;
			_mover.RotationAcceleration = 0;
			_mover.RotationDeceleration = 0;
			_mover.MaxSpeed             = _asteroidsConfig.RandomSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}