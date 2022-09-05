namespace Game.Code.Unity
{
	using System.Collections.Generic;
	using System.Linq;
	using Game.Code.Unity.Asteroids;
	using Game.Code.Unity.Camera;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enemies;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Input;
	using Game.Code.Unity.Move;
	using Game.Code.Unity.Scores;
	using Game.Code.Unity.Ship;
	using Game.Code.Unity.Spawn;
	using Game.Code.Unity.Utils;
	using Game.Code.Unity.Weapons;
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

		private readonly SpawnTimer _asteroidsSpawnTimer;
		private readonly SpawnTimer _enemiesSpawnTimer;
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
			
			_asteroidsSpawnTimer = new SpawnTimer( _cameraController, _rootConfig.Asteroids.SpawnDelay, _heroFacade );
			_enemiesSpawnTimer = new SpawnTimer( _cameraController, _rootConfig.Enemies.SpawnDelay, _heroFacade );

			SetupAsteroids();
			SetupEnemies();
		}

		public void Tick(float deltaTime)
		{
			_tickableModels.ForEach( t => t.Tick( deltaTime ) );
			_asteroidsSpawnTimer.Tick();
			_enemiesSpawnTimer.Tick();
			_shipBulletSystem.Tick( deltaTime );
		}

		private void SetupShip()
		{
			SpawnHeroShip();
			_tickableModels.Add( _shipModel );
		}

		private void SetupAsteroids()
		{
			_asteroidsSpawnTimer.SpawnRequest += data =>
			{
				var model = SpawnAsteroid( data );
				_tickableModels.Add( model );
			};
		}

		private void SetupEnemies()
		{
			_enemiesSpawnTimer.SpawnRequest += data =>
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