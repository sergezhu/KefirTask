namespace Game.Code.Unity.Ship
{
	using Game.Code.Core.Move;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Utils;

	public class ShipPresenter : ITickable
	{
		private readonly IShipView _view;
		private readonly MouseAndKeyboardControl _control;
		private readonly IMover _mover;

		public ShipPresenter(IShipView view, MouseAndKeyboardControl control, IMover mover)
		{
			_view    = view;
			_control = control;
			_mover   = mover;
		}

		public void Tick( float deltaTime )
		{
			_mover.Tick( deltaTime );
			_view.SetPosition( _mover.Position.ToUnityVector3() );
			_view.SetRotation( _mover.Rotation );
		}
	}
}