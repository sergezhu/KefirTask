namespace Game.Code.Core
{
	using System.Collections.Generic;
	using System.Linq;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Asteroids;
	using Game.Code.Core.Camera;
	using Game.Code.Core.Common;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Enemies;
	using Game.Code.Core.Input;
	using Game.Code.Core.Move;
	using Game.Code.Core.Scores;
	using Game.Code.Core.Ship;
	using Game.Code.Core.Spawn;
	using Game.Code.Core.Weapons;
	using Game.Code.View.Asteroids;
	using Game.Code.View.Enemy;
	using Game.Code.View.Ship;
	using UnityEngine;

	public class GameSystem
	{
		private readonly RootConfig _rootConfig;
		private readonly ViewFactory _viewFactory;
		private readonly BulletViewFactory _bulletViewFactory;
		private readonly AsteroidPartsFactory _asteroidPartsFactory;
		private readonly CameraController _cameraController;
		private readonly MouseAndKeyboardControl _mouseAndKeyboardControl;
		private readonly ScoresSystem _scoresSystem;

		private Mover _shipMover;
		private ShipModel _shipModel;
		private ShipView _shipView;
		private Rotator _shipRotator;
		private BulletSystem _shipBulletSystem;
		private HeroFacade _heroFacade;

		private List<BaseModel> _tickableModels;

		private readonly SpawnService _asteroidsSpawnService;
		private readonly SpawnService _enemiesSpawnService;
		private readonly ScreenPortal _screenPortal;

		public HeroFacade HeroFacade => _heroFacade;

		public GameSystem( RootConfig rootConfig, ViewFactory viewFactory, BulletViewFactory bulletViewFactory, AsteroidPartsFactory asteroidPartsFactory,
						   CameraController cameraController, MouseAndKeyboardControl mouseAndKeyboardControl, ScoresSystem scoresSystem )
		{
			_rootConfig              = rootConfig;
			_viewFactory             = viewFactory;
			_bulletViewFactory       = bulletViewFactory;
			_asteroidPartsFactory    = asteroidPartsFactory;
			_cameraController        = cameraController;
			_mouseAndKeyboardControl = mouseAndKeyboardControl;
			_scoresSystem			 = scoresSystem;

			_tickableModels = new List<BaseModel>();

			_screenPortal = new ScreenPortal( _cameraController );

			SetupShip();
			
			_asteroidsSpawnService = new SpawnService( _cameraController, _rootConfig.Asteroids.SpawnDelay, _heroFacade );
			_enemiesSpawnService = new SpawnService( _cameraController, _rootConfig.Enemies.SpawnDelay, _heroFacade );

			SetupAsteroids();
			SetupEnemies();
		}

		public void Tick(float deltaTime)
		{
			_tickableModels.ForEach( t => t.Tick( deltaTime ) );
			_asteroidsSpawnService.Tick();
			_enemiesSpawnService.Tick();
			_shipBulletSystem.Tick( deltaTime );
		}

		private void SetupShip()
		{
			SpawnHeroShip();
			_tickableModels.Add( _shipModel );
		}

		private void SetupAsteroids()
		{
			_asteroidsSpawnService.SpawnRequest += data =>
			{
				var model = SpawnAsteroid( data );
				_tickableModels.Add( model );
			};
		}

		private void SetupEnemies()
		{
			_enemiesSpawnService.SpawnRequest += data =>
			{
				var model = SpawnEnemy( data );
				_tickableModels.Add( model );
			};
		}

		private void SpawnHeroShip()
		{
			var shipConfig = _rootConfig.Ship;

			_shipView         = _viewFactory.Create( EEntityType.Ship ) as ShipView;
			_shipMover        = new Mover( shipConfig.StartPosition, 0, shipConfig.SmoothDirection );
			_shipBulletSystem = new BulletSystem();
			_shipModel        = new ShipModel( _shipView, _mouseAndKeyboardControl, _shipMover, shipConfig, _bulletViewFactory, _shipBulletSystem, _screenPortal );
			_heroFacade       = new HeroFacade( _shipModel );

			_shipModel.DestroyRequest += OnDestroyRequest;
		}

		private AsteroidModel SpawnAsteroid( SpawnData data )
		{
			var asteroidsConfig = _rootConfig.Asteroids;
			
			var asteroidView = _viewFactory.Create( EEntityType.Asteroid ) as AsteroidView;
			var mover = new Mover( data.Position, data.Direction, 1 );
			var rotator = new Rotator( Random.rotation, Random.rotation, asteroidsConfig.RandomRotationSpeed );
			var model = new AsteroidModel( asteroidView, mover, rotator, asteroidsConfig );

			mover.StartMove();

			model.CreatePartsRequest += OnCreatePartsRequest;
			model.DestroyRequest += OnDestroyRequest;
			
			return model;
		}

		private AsteroidPartModel SpawnAsteroidPart( AsteroidPartView sourceAsteroidView )
		{
			var asteroidsConfig = _rootConfig.Asteroids;
			var mover = new Mover( sourceAsteroidView.Position, sourceAsteroidView.Velocity, 1 );
			var rotator = new Rotator( Random.rotation, Random.rotation, asteroidsConfig.RandomRotationSpeed );
			var model = new AsteroidPartModel( sourceAsteroidView, mover, rotator, asteroidsConfig );

			mover.StartMove();

			model.DestroyRequest += OnDestroyRequest;
			
			return model;
		}

		private EnemyModel SpawnEnemy( SpawnData data )
		{
			var enemiesConfig = _rootConfig.Enemies;
			
			var enemyView = _viewFactory.Create( EEntityType.Enemy ) as EnemyView;
			var mover = new Mover( data.Position, data.Direction, enemiesConfig.SmoothDirection );
			var model = new EnemyModel( enemyView, mover, _heroFacade, enemiesConfig );

			mover.StartMove();

			model.DestroyRequest += OnDestroyRequest;

			return model;
		}

		private void OnCreatePartsRequest( SourceAsteroidData data )
		{
			var partViews = _asteroidPartsFactory.Create( data );
			
			partViews
				.ToList()
				.ForEach( view =>
				{
					var model = SpawnAsteroidPart( view );
					_tickableModels.Add( model );
				} );
		}

		private void OnDestroyRequest( DestroyInfo info )
		{
			Debug.Log( $"Destroy {info.EntityType}" );

			var p = info.Model;

			p.DestroyRequest -= OnDestroyRequest;
			_tickableModels.Remove( p );

			if ( info.HasBeenDestroyedByPlayerWeapon )
				_scoresSystem.AddScoresByType( info.EntityType );
		}
	}
}