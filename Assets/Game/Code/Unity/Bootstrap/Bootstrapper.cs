namespace Game.Code.Unity.Bootstrap
{
	using Game.Code.Unity.Asteroids;
	using Game.Code.Unity.Camera;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.UI;
	using UnityEngine;

	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private RootConfig _rootConfig;
		[SerializeField] private UIHudView _uiHudView;

		private InputTest _inputTest;

		private IInputManager _inputManager;
		private MouseAndKeyboardControl _mouseAndKeyboardControl;
		private ViewFactory _viewFactory;
		private BulletViewFactory _bulletViewFactory;
		private AsteroidPartsFactory _asteroidPartsFactory;
		private CameraController _cameraController;
		private GameSystem _gameSystem;
		
		private UIViewFactory _uiViewFactory;
		private LaserChargeBlocksViewFactory _laserChargeViewFactory;
		private UISystem _uiSystem;

		private void Awake()
		{
			_inputManager				= new InputManager();
			_mouseAndKeyboardControl	= new MouseAndKeyboardControl( _inputManager );
			_viewFactory				= new ViewFactory( _rootConfig.ViewPrefabs );
			_bulletViewFactory			= new BulletViewFactory( _viewFactory );
			_asteroidPartsFactory		= new AsteroidPartsFactory( _viewFactory, _rootConfig.Asteroids );

			_uiViewFactory				= new UIViewFactory( _rootConfig.ViewPrefabs );
			_laserChargeViewFactory		= new LaserChargeBlocksViewFactory( _uiViewFactory );

			//_inputTest = FindObjectOfType<InputTest>();
			//_inputTest.Init( _mouseAndKeyboardControl );

			_cameraController = FindObjectOfType<CameraController>();

			_gameSystem = new GameSystem( _rootConfig, _viewFactory, _bulletViewFactory, _asteroidPartsFactory, _cameraController, _mouseAndKeyboardControl );
			_uiSystem = new UISystem( _rootConfig, _uiViewFactory, _laserChargeViewFactory, _gameSystem.HeroFacade, _uiHudView );
		}

		private void Update()
		{
			_gameSystem.Tick( Time.deltaTime );
			_uiSystem.Tick( Time.deltaTime );
		}
	}
}
