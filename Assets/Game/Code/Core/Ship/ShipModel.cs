namespace Game.Code.Core.Ship
{
	using System.Collections.Generic;
	using Game.Code.Common;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Input;
	using Game.Code.Core.Move;
	using Game.Code.Core.RX;
	using Game.Code.Core.Weapons;
	using Game.Code.View.Ship;
	using UnityEngine;

	public class ShipModel : BaseModel
	{
		private readonly ShipView _view;
		private readonly MouseAndKeyboardControl _control;
		private readonly Mover _mover;
		private readonly ShipConfig _shipConfig;
		private readonly BulletViewFactory _bulletViewFactory;
		private readonly BulletSystem _bulletSystem;
		private readonly ScreenPortal _screenPortal;

		private BulletCannon _bulletCannonModel;
		private LaserCannon _laserCannonModel;

		public ReactiveProperty<Vector3> Position { get; } = new();
		public ReactiveProperty<Vector3> Velocity { get; } = new();
		public ReactiveProperty<float> CurrentDirectionAngle { get; } = new();
		public ReactiveProperty<float> CurrentSpeed { get; } = new();
		
		public IEnumerable<LaserCharge> LaserCharges => _laserCannonModel.Charges;

		public ShipModel( ShipView view, MouseAndKeyboardControl control, Mover mover, ShipConfig shipConfig, BulletViewFactory bulletViewFactory,
						  BulletSystem bulletSystem, ScreenPortal screenPortal )
		{
			_view              = view;
			_control           = control;
			_mover             = mover;
			_shipConfig        = shipConfig;
			_bulletViewFactory = bulletViewFactory;
			_bulletSystem      = bulletSystem;
			_screenPortal      = screenPortal;

			SetupMover();
			SetupWeapons();
			Subscribe();
		}

		public override void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );

			CheckScreenPortal();

			_view.Position = _mover.Position;
			_view.Rotation = Quaternion.Euler( 0, _mover.DesiredDirectionAngle * Mathf.Rad2Deg, 0 );
			_view.Velocity = _mover.Velocity;
			
			_bulletCannonModel.Tick( deltaTime );
			_laserCannonModel.Tick( deltaTime );

			UpdateReactiveProperties();
		}

		private void CheckScreenPortal()
		{
			var checkedPos = _screenPortal.RecalculatePosition( _mover.Position );
			_mover.Position = checkedPos;
		}

		private void UpdateReactiveProperties()
		{
			Position.Value = _mover.Position;
			Velocity.Value = _mover.Velocity;
			CurrentDirectionAngle.Value = _mover.CurrentDirectionAngle;
			CurrentSpeed.Value = _mover.CurrentSpeed;
		}

		private void SetupMover()
		{
			_mover.Acceleration         = _shipConfig.Acceleration;
			_mover.Deceleration         = _shipConfig.Deceleration;
			_mover.RotationAcceleration = _shipConfig.RotationAcceleration;
			_mover.RotationDeceleration = _shipConfig.RotationDeceleration;
			_mover.MaxSpeed             = _shipConfig.MaxSpeed;
			_mover.MaxRotationSpeed     = _shipConfig.MaxRotationSpeed;
		}

		private void SetupWeapons()
		{
			_bulletCannonModel = new BulletCannon( _view.BulletCannonView, _bulletViewFactory, _shipConfig, _bulletSystem );
			_laserCannonModel = new LaserCannon( _view.LaserCannonView, _shipConfig );
		}

		private void Subscribe()
		{
			ControlsSubscribe();

			_view.Collided += OnCollided;
		}

		private void Unsubscribe()
		{
			ControlsUnsubscribe();

			_view.Collided -= OnCollided;
		}

		private void OnCollided( CollisionInfo info )
		{
			if ( IsDestroyed )
				return;
			
			if ( info.OtherEntityType == EEntityType.Asteroid ||
			     info.OtherEntityType == EEntityType.Enemy ||
			     info.OtherEntityType == EEntityType.AsteroidPart )
			{
				Unsubscribe();

				_view.Destroy();
				_mover.OnDestroy();
				
				InvokeDestroy( new DestroyInfo() {Model = this, EntityType = _view.Type, HasBeenDestroyedByPlayerWeapon = false} );
			}
		}

		private void ControlsSubscribe()
		{
			_control.MoveStart      += OnMoveStart;
			_control.MoveEnd        += OnMoveEnd;
			_control.RotateCWStart  += OnRotateCWStart;
			_control.RotateCWEnd    += OnRotateCWEnd;
			_control.RotateCCWStart += OnRotateCCWStart;
			_control.RotateCCWEnd   += OnRotateCCWEnd;

			_control.Fire1 += OnFire1;
			_control.Fire2 += OnFire2;
		}

		private void ControlsUnsubscribe()
		{
			_control.MoveStart      -= OnMoveStart;
			_control.MoveEnd        -= OnMoveEnd;
			_control.RotateCWStart  -= OnRotateCWStart;
			_control.RotateCWEnd    -= OnRotateCWEnd;
			_control.RotateCCWStart -= OnRotateCCWStart;
			_control.RotateCCWEnd   -= OnRotateCCWEnd;

			_control.Fire1 -= OnFire1;
			_control.Fire2 -= OnFire2;
		}

		private void OnMoveStart() => _mover.StartMove();
		private void OnMoveEnd() => _mover.EndMove();
		private void OnRotateCWStart() => _mover.StartCVRotation();
		private void OnRotateCWEnd() => _mover.EndCVRotation();
		private void OnRotateCCWStart() => _mover.StartCCVRotation();
		private void OnRotateCCWEnd() => _mover.EndCCVRotation();

		private void OnFire1() => _bulletCannonModel.Shot();
		private void OnFire2() => _laserCannonModel.Shot();
	}
}