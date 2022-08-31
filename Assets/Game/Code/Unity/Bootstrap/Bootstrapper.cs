using System.Collections.Generic;
using Game.Code.Core.Move;
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
	private ShipView _shipView;
	private Rotator _shipRotator;

	private void Awake()
	{
		_tickables = new List<ITickable>();
		
		_inputManager            = new InputManager();
		_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );

		//_inputTest = FindObjectOfType<InputTest>();
		//_inputTest.Init( _mouseAndKeyboardControl );

		SetupShip();
		SetupAsteroids();
	}

	private void SetupShip()
	{
		var shipConfig = _rootConfig.Ship;

		_shipView      = FindObjectOfType<ShipView>();
		_shipMover     = new Mover( shipConfig.StartPosition.ToNumericsVector3(), 0, shipConfig.SmoothDirection );
		_shipPresenter = new ShipPresenter( _shipView, _mouseAndKeyboardControl, _shipMover, shipConfig );

		_shipPresenter.Subscribe();
		_tickables.Add( _shipPresenter );
	}

	private void SetupAsteroids()
	{
	}

	private void Update()
	{
		_tickables.ForEach( t => t.Tick( Time.deltaTime ) );
	}
}
