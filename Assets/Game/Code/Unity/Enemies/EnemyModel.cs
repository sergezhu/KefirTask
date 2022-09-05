namespace Game.Code.Unity.Enemies
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Ship;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class EnemyModel : BaseModel
	{
		private const float DistanceThreshold = 0.2f;
		
		private readonly EnemyView _view;
		private readonly Mover _mover;
		private readonly HeroFacade _hero;
		private readonly EnemiesConfig _enemiesConfig;

		private bool _isHeroDead;

		public EnemyModel(EnemyView view, Mover mover, HeroFacade hero, EnemiesConfig enemiesConfig)
		{
			_view          = view;
			_mover         = mover;
			_hero          = hero;
			_enemiesConfig = enemiesConfig;

			SetupMover();
			Subscribe();
			
			UpdateView();
		}

		public override void Tick( float deltaTime )
		{
			if(_isHeroDead == false)
				SetToHeroDirection();
			
			_mover.Tick( deltaTime );

			UpdateView();
		}

		private void Subscribe()
		{
			_view.Collided += OnCollided;
			_view.LaserHit += OnLaserHit;
			_hero.Dead     += OnHeroDead;
		}

		private void Unsubscribe()
		{
			_view.Collided -= OnCollided;
			_view.LaserHit -= OnLaserHit;
			_hero.Dead     -= OnHeroDead;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( info.OtherEntityType == EEntityType.Ship || 
			     info.OtherEntityType == EEntityType.Bullet )
			{
				Destroy( info.OtherEntityType == EEntityType.Bullet );
			}
		}

		private void OnLaserHit()
		{
			Destroy(true);
		}
		
		private void OnHeroDead()
		{
			_isHeroDead = true;
			_mover.EndMove();
		}

		private void Destroy( bool hasBeenDestroyedByPlayerWeapon )
		{
			Unsubscribe();

			_view.Destroy();
			_mover.OnDestroy();

			InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type, HasBeenDestroyedByPlayerWeapon = hasBeenDestroyedByPlayerWeapon} );
		}

		private void UpdateView()
		{
			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = Quaternion.Euler( 0, _mover.CurrentDirectionAngle * Mathf.Rad2Deg, 0 );

			_view.Velocity = _mover.Velocity.ToUnityVector3();
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
			var vector = _hero.Position.Value - _mover.Position.ToUnityVector3();

			if ( vector.magnitude < DistanceThreshold )
			{
				_mover.EndMove();
			}
			else
			{
				_mover.StartMove();

				vector.y = 0;
				vector   = vector.normalized;

				SetDirection( vector );
			}
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _enemiesConfig.Acceleration;
			_mover.Deceleration         = _enemiesConfig.Acceleration;
			_mover.RotationAcceleration = _enemiesConfig.RotationAcceleration;
			_mover.RotationDeceleration = _enemiesConfig.RotationAcceleration;
			_mover.MaxSpeed             = _enemiesConfig.RandomSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}