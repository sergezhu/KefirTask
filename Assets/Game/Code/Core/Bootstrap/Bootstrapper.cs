namespace Game.Code.Core.Bootstrap
{
	using Game.Code.Core.Asteroids;
	using Game.Code.Core.Camera;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Input;
	using Game.Code.Core.Scores;
	using Game.Code.Core.UI;
	using Game.Code.View.UI;
	using UnityEngine;

	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private RootConfig _rootConfig;
		[SerializeField] private UIHudView _uiHudView;
		[SerializeField] private UIResultScreenView _uiResultScreenView;

		//private InputTest _inputTest;

		private IInputManager _inputManager;
		private MouseAndKeyboardControl _mouseAndKeyboardControl;
		private ViewFactory _viewFactory;
		private BulletViewFactory _bulletViewFactory;
		private AsteroidPartsFactory _asteroidPartsFactory;
		private CameraController _cameraController;
		private GameSystem _gameSystem;
		private ScoresSystem _scoresSystem;
		private RestartService _restartService;

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
			_scoresSystem				= new ScoresSystem(_rootConfig.Reward);
			_restartService				= new RestartService(); 

			_uiViewFactory				= new UIViewFactory( _rootConfig.ViewPrefabs );
			_laserChargeViewFactory		= new LaserChargeBlocksViewFactory( _uiViewFactory );

			//_inputTest = FindObjectOfType<InputTest>();
			//_inputTest.Init( _mouseAndKeyboardControl );

			_cameraController = FindObjectOfType<CameraController>();

			_gameSystem = new GameSystem( _rootConfig, _viewFactory, _bulletViewFactory, _asteroidPartsFactory, _cameraController, _mouseAndKeyboardControl, _scoresSystem );
			_uiSystem = new UISystem( _laserChargeViewFactory, _gameSystem.HeroFacade, _uiHudView, _uiResultScreenView, _scoresSystem, _restartService );
		}

		private void Update()
		{
			_gameSystem.Tick( Time.deltaTime );
		}
	}
}
