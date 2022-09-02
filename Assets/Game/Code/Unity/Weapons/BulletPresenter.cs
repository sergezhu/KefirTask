namespace Game.Code.Unity.Weapons
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class BulletPresenter : BasePresenter
	{
		private readonly BulletView _view;
		private readonly Mover _mover;
		private readonly ShipConfig _shipConfig;

		public BulletPresenter(BulletView view, Mover mover, ShipConfig shipConfig)
		{
			_view       = view;
			_mover      = mover;
			_shipConfig = shipConfig;

			SetupMover();
			Subscribe();
			
			UpdateView();
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );

			UpdateView();
		}

		public void StartMoveAlongDirection( Vector3 dir )
		{
			SetDirection( dir );
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
			if ( info.OtherEntityType == EEntityType.Asteroid ||
			     info.OtherEntityType == EEntityType.AsteroidPart ||
			     info.OtherEntityType == EEntityType.Enemy )
			{
				Unsubscribe();

				_view.Destroy();
				_mover.OnDestroy();

				InvokeDestroy( new DestroyInfo() {Presenter = this, EntityType = _view.Type} );
			}
		}

		private void UpdateView()
		{
			_view.Position = _mover.Position.ToUnityVector3();
			_view.Velocity = _mover.Velocity.ToUnityVector3();
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir.ToNumericsVector3() );
		}

		private void SetupMover()
		{
			_mover.Acceleration         = 100;
			_mover.Deceleration         = 0;
			_mover.RotationAcceleration = 0;
			_mover.RotationDeceleration = 0;
			_mover.MaxSpeed             = _shipConfig.BulletMaxSpeed;
			_mover.MaxRotationSpeed     = 0;
		}
	}
}