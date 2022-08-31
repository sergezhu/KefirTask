namespace Game.Code.Unity.Ship
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Utils;
	using Game.Configs;

	public class ShipPresenter : ITickable
	{
		private readonly IShipView _view;
		private readonly MouseAndKeyboardControl _control;
		private readonly IMover _mover;
		private readonly ShipConfig _shipConfig;

		public ShipPresenter(IShipView view, MouseAndKeyboardControl control, IMover mover, ShipConfig shipConfig)
		{
			_view       = view;
			_control    = control;
			_mover      = mover;
			_shipConfig = shipConfig;

			SetupMover();
		}

		public void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );

			_view.SetPosition( _mover.Position.ToUnityVector3() );
			_view.SetRotation( _mover.DesiredDirectionAngle );
		}

		public void Subscribe()
		{
			_control.MoveStart      += OnMoveStart;
			_control.MoveEnd        += OnMoveEnd;
			_control.RotateCWStart  += OnRotateCWStart;
			_control.RotateCWEnd    += OnRotateCWEnd;
			_control.RotateCCWStart += OnRotateCCWStart;
			_control.RotateCCWEnd   += OnRotateCCWEnd;
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

		private void OnMoveStart() => _mover.StartMove();
		private void OnMoveEnd() => _mover.EndMove();
		private void OnRotateCWStart() => _mover.StartCVRotation();
		private void OnRotateCWEnd() => _mover.EndCVRotation();
		private void OnRotateCCWStart() => _mover.StartCCVRotation();
		private void OnRotateCCWEnd() => _mover.EndCCVRotation();
	}
}