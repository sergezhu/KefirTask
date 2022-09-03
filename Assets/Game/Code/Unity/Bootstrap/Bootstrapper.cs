namespace Game.Code.Unity.Bootstrap
{
	using System.Collections.Generic;
	using System.Linq;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Asteroids;
	using Game.Code.Unity.Camera;
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

		private InputTest _inputTest;

		private IInputManager _inputManager;
		private MouseAndKeyboardControl _mouseAndKeyboardControl;
		private ViewFactory _viewFactory;
		private BulletViewFactory _bulletViewFactory;
		private AsteroidPartsFactory _asteroidPartsFactory;
		private CameraController _cameraController;
		private GameSystem _gameSystem;

		private void Awake()
		{
			_inputManager            = new InputManager();
			_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );
			_viewFactory             = new ViewFactory( _rootConfig.ViewPrefabs );
			_bulletViewFactory       = new BulletViewFactory( _viewFactory );
			_asteroidPartsFactory    = new AsteroidPartsFactory( _viewFactory, _rootConfig.Asteroids );

			//_inputTest = FindObjectOfType<InputTest>();
			//_inputTest.Init( _mouseAndKeyboardControl );

			_cameraController = FindObjectOfType<CameraController>();

			_gameSystem = new GameSystem( _rootConfig, _viewFactory, _bulletViewFactory, _asteroidPartsFactory, _cameraController, _mouseAndKeyboardControl );
		}

		private void Update()
		{
			_gameSystem.Tick( Time.deltaTime );
		}
	}
}
