namespace Game.Code.Core.Weapons
{
	using Game.Code.Core.Collisions;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Enums;
	using Game.Code.Core.Move;
	using UnityEngine;

	public class BulletModel : BaseModel
	{
		private readonly BulletView _view;
		private readonly Mover _mover;
		private readonly ShipConfig _shipConfig;

		public BulletModel(BulletView view, Mover mover, ShipConfig shipConfig)
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

		public void StartMove()
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
			if ( info.OtherEntityType == EEntityType.Asteroid ||
			     info.OtherEntityType == EEntityType.AsteroidPart ||
			     info.OtherEntityType == EEntityType.Enemy )
			{
				Unsubscribe();

				_view.Destroy();
				_mover.OnDestroy();

				InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type} );
			}
		}

		private void UpdateView()
		{
			_view.Position = _mover.Position;
			_view.Velocity = _mover.Velocity;
		}

		private void SetDirection( Vector3 dir )
		{
			_mover.SetDirection( dir );
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