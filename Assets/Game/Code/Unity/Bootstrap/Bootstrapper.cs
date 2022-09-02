namespace Game.Code.Unity.Bootstrap
{
	using System.Collections.Generic;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Ship;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private RootConfig _rootConfig;
	
		private List<ITickable> _tickables;
	
		private InputTest _inputTest;

		private IInputManager _inputManager;
		private MouseAndKeyboardControl _mouseAndKeyboardControl;
		private ViewFactory _viewFactory;

		private Mover _shipMover;
		private ShipPresenter _shipPresenter;
		private ShipView _shipView;
		private Rotator _shipRotator;

		private void Awake()
		{
			_tickables = new List<ITickable>();
		
			_inputManager            = new InputManager();
			_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );
			_viewFactory             = new ViewFactory( _rootConfig.ViewPrefabs );

			//_inputTest = FindObjectOfType<InputTest>();
			//_inputTest.Init( _mouseAndKeyboardControl );


			SetupShip();
			SetupAsteroids();
		}

		private void SetupShip()
		{
			var shipConfig = _rootConfig.Ship;

			_shipView      = _viewFactory.Create( EEntityType.Ship ) as ShipView;
			_shipMover     = new Mover( shipConfig.StartPosition.ToNumericsVector3(), 0, shipConfig.SmoothDirection );
			_shipPresenter = new ShipPresenter( _shipView, _mouseAndKeyboardControl, _shipMover, shipConfig );

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
}
