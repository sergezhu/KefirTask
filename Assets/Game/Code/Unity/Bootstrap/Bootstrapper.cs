using System.Collections.Generic;
using Game.Code.Unity;
using Game.Code.Unity.Input;
using Game.Code.Unity.Ship;
using Game.Code.Unity.Utils;
using Game.Configs;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
	[SerializeField] private RootConfig _rootConfig;
	
	private List<ITickable> _tickables;
	
	private InputTest _inputTest;

	private IInputManager _inputManager;
	private MouseAndKeyboardControl _mouseAndKeyboardControl;

	private Mover _shipMover;
	private ShipPresenter _shipPresenter;
	private DefaultShipView _shipView;

	private void Awake()
	{
		_tickables = new List<ITickable>();
		
		_inputManager            = new InputManager();
		_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );

		//_inputTest = FindObjectOfType<InputTest>();
		//_inputTest.Init( _mouseAndKeyboardControl );

		_shipView      = FindObjectOfType<DefaultShipView>();
		_shipMover     = new Mover( _rootConfig.Ship.StartPosition.ToInternalVector3(), 0 );
		_shipPresenter = new ShipPresenter( _shipView, _mouseAndKeyboardControl, _shipMover, _rootConfig.Ship );
		
		_shipPresenter.Subscribe();
		_tickables.Add( _shipPresenter );
	}

	private void Update()
	{
		_tickables.ForEach( t => t.Tick( Time.deltaTime ) );
	}
}
