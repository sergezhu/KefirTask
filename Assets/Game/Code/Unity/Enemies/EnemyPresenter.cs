namespace Game.Code.Unity.Enemies
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Utils;
	using Game.Configs;
	using UnityEngine;

	public class EnemyPresenter : ITickable
	{
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
		}

		public void Tick( float deltaTime )
		{
			SetToHeroDirection();
			
			_mover.Tick( deltaTime );

			_view.Position = _mover.Position.ToUnityVector3();
			_view.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 ); 
		}

		public void StartMove( Vector3 dir )
		{
			_mover.StartMove();
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