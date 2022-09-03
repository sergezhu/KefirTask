namespace Game.Code.Unity
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

	public class GameSystem
	{
		private readonly RootConfig _rootConfig;
		private readonly ViewFactory _viewFactory;
		private readonly BulletViewFactory _bulletViewFactory;
		private readonly AsteroidPartsFactory _asteroidPartsFactory;
		private readonly CameraController _cameraController;
		private readonly MouseAndKeyboardControl _mouseAndKeyboardControl;

		private Mover _shipMover;
		private ShipModel _shipModel;
		private ShipView _shipView;
		private Rotator _shipRotator;

		private List<BaseModel> _tickableModels;


		public GameSystem( RootConfig rootConfig, ViewFactory viewFactory, BulletViewFactory bulletViewFactory, AsteroidPartsFactory asteroidPartsFactory,
		                   CameraController cameraController, MouseAndKeyboardControl mouseAndKeyboardControl )
		{
			_rootConfig              = rootConfig;
			_viewFactory             = viewFactory;
			_bulletViewFactory       = bulletViewFactory;
			_asteroidPartsFactory    = asteroidPartsFactory;
			_cameraController        = cameraController;
			_mouseAndKeyboardControl = mouseAndKeyboardControl;

			_tickableModels    = new List<BaseModel>();
			
			SetupShip(); 
			SetupAsteroids();
			
			SubscribeOnDestroy();
			SubscribeOnCreateParts();
		}

		public void Tick(float deltaTime)
		{
			_tickableModels.ForEach( t => t.Tick( deltaTime ) );
		}

		private void SetupShip()
		{
			var shipConfig = _rootConfig.Ship;

			_shipView  = _viewFactory.Create( EEntityType.Ship ) as ShipView;
			_shipMover = new Mover( shipConfig.StartPosition.ToNumericsVector3(), 0, shipConfig.SmoothDirection );
			_shipModel = new ShipModel( _shipView, _mouseAndKeyboardControl, _shipMover, shipConfig, _bulletViewFactory );

			_tickableModels.Add( _shipModel );
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
				var rotator = new Rotator( Random.rotation.ToNumericsQuaternion(), Random.rotation.ToNumericsQuaternion(),
				                           asteroidsConfig.RandomRotationSpeed );
				var presenter = new AsteroidModel( asteroidView, mover, rotator, asteroidsConfig );
				
				mover.StartMove();

				_tickableModels.Add( presenter );
			} );
		}

		private void SubscribeOnDestroy()
		{
			_tickableModels.ForEach( model => { model.DestroyRequest += OnDestroyRequest; } );
		}

		private void SubscribeOnCreateParts()
		{
			_tickableModels
				.OfType<AsteroidModel>()
				.ToList()
				.ForEach( model => { model.CreatePartsRequest += OnCreatePartsRequest; } );
		}

		private void OnDestroyRequest( DestroyInfo info )
		{
			Debug.Log( $"Destroy {info.EntityType}" );

			var p = info.Model;

			p.DestroyRequest -= OnDestroyRequest;
			_tickableModels.Remove( p );
		}

		private void OnCreatePartsRequest( SourceAsteroidData data )
		{
			var partViews       = _asteroidPartsFactory.Create( data );
			var asteroidsConfig = _rootConfig.Asteroids;
			
			partViews
				.ToList()
				.ForEach( view =>
				{
					var mover = new Mover( view.Position.ToNumericsVector3(), view.Velocity.ToNumericsVector3(), 1 );
					var rotator = new Rotator( Random.rotation.ToNumericsQuaternion(), Random.rotation.ToNumericsQuaternion(),
					                           asteroidsConfig.RandomRotationSpeed );
					var presenter = new AsteroidPartModel( view, mover, rotator, asteroidsConfig );
					
					mover.StartMove();

					_tickableModels.Add( presenter );
				} );
		}
	}
}