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
	
		private List<BasePresenter> _tickablePresenters;
	
		private InputTest _inputTest;

		private IInputManager _inputManager;
		private MouseAndKeyboardControl _mouseAndKeyboardControl;
		private ViewFactory _viewFactory;

		private Mover _shipMover;
		private ShipPresenter _shipPresenter;
		private ShipView _shipView;
		private Rotator _shipRotator;
		private CameraController _cameraController;

		private void Awake()
		{
			_tickablePresenters = new List<BasePresenter>();
		
			_inputManager            = new InputManager();
			_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );
			_viewFactory             = new ViewFactory( _rootConfig.ViewPrefabs );

			//_inputTest = FindObjectOfType<InputTest>();
			//_inputTest.Init( _mouseAndKeyboardControl );

			_cameraController = FindObjectOfType<CameraController>();

			SetupShip();
			SetupAsteroids();

			SubscribeOnDestroy();
		}

		private void Update()
		{
			_tickablePresenters.ForEach( t => t.Tick( Time.deltaTime ) );
		}

		private void SetupShip()
		{
			var shipConfig = _rootConfig.Ship;

			_shipView      = _viewFactory.Create( EEntityType.Ship ) as ShipView;
			_shipMover     = new Mover( shipConfig.StartPosition.ToNumericsVector3(), 0, shipConfig.SmoothDirection );
			_shipPresenter = new ShipPresenter( _shipView, _mouseAndKeyboardControl, _shipMover, shipConfig );

			_tickablePresenters.Add( _shipPresenter );
		}

		private void SetupAsteroids()
		{
			var asteroidsConfig = _rootConfig.Asteroids;

			var frustumPoints = _cameraController.GetFrustumPoints();
			var points        = new[] {frustumPoints.LeftBottom, frustumPoints.LeftTop, frustumPoints.RightTop, frustumPoints.RightBottom};
			
			points.ToList().ForEach( p =>
			{
				var asteroidView = _viewFactory.Create( EEntityType.Asteroid ) as AsteroidView;
				var mover        = new Mover( p.ToNumericsVector3(), 0, 1 );
				var rotator      = new Rotator( Random.rotation.ToNumericsQuaternion(), Random.rotation.ToNumericsQuaternion(), 
				                                asteroidsConfig.RandomRotationSpeed );
				var presenter = new AsteroidPresenter( asteroidView, mover, rotator, asteroidsConfig );
				
				_tickablePresenters.Add( presenter );
			} );
		}

		private void SubscribeOnDestroy()
		{
			_tickablePresenters.ForEach( p =>
			{
				p.DestroyRequest += OnDestroyRequest;
			} );
		}

		private void OnDestroyRequest( DestroyInfo info )
		{
			Debug.Log( $"Destroy {info.EntityType}" );

			var p = info.Presenter;
			
			p.DestroyRequest -= OnDestroyRequest;
			_tickablePresenters.Remove( p );
		}
	}
}
