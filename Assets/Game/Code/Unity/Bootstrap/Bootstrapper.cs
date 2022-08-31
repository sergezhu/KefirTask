using System.Collections.Generic;
using Game.Code.Unity;
using Game.Code.Unity.Input;
using Game.Code.Unity.Ship;
using Game.Code.Unity.Utils;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
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

		_inputTest = FindObjectOfType<InputTest>();
		_inputTest.Init( _mouseAndKeyboardControl );

		_shipView      = FindObjectOfType<DefaultShipView>();
		_shipMover     = new Mover( Vector3.zero.ToInternalVector3(), 0 );
		_shipPresenter = new ShipPresenter( _shipView, _mouseAndKeyboardControl, _shipMover );
	}

	private void Update()
	{
		_tickables.ForEach( t => t.Tick( Time.deltaTime ) );
	}
}
