namespace Game.Code.Core.Spawn
{
	using System;
	using Game.Code.Core.Camera;
	using Game.Code.Core.Ship;
	using Game.Code.Core.Utils;
	using UnityEngine;
	using Random = UnityEngine.Random;

	public class SpawnTimer
	{
		public event Action<SpawnData> SpawnRequest;

		private const float SpawnZoneWidth = 2;
		private const float DirectionSector = 90;
		
		private readonly CameraController _cameraController;
		private readonly HeroFacade _hero;
		private readonly float _delay;

		private Rect[] _areas;
		private float _startTime;
		private float _endTime;
		private bool _isHeroDead;

		public SpawnTimer(CameraController cameraController, float delay, HeroFacade hero)
		{
			_cameraController = cameraController;
			_delay            = delay;
			_hero             = hero;

			ResetTimer();
			SetupAreas();
			
			Subscribe();
		}

		public void Tick()
		{
			if(_isHeroDead)
				return;
			
			var time = Time.time;

			if ( time < _endTime )
				return;

			var pos  = GetRandomPoint();
			var dir  = GetRandomDirection( pos );
			var data = new SpawnData() {Position = pos, Direction = dir};

			SpawnRequest?.Invoke( data );
			
			ResetTimer();
		}

		private void Subscribe()
		{
			_hero.Dead     += OnHeroDead;
		}

		private void OnHeroDead()
		{
			_isHeroDead = true;
		}

		private void ResetTimer()
		{
			_startTime = Time.time;
			_endTime   = _startTime + _delay;
		}

		private void SetupAreas()
		{
			var frustumPoints = _cameraController.GetFrustumPoints();
			var min           = new Vector2( frustumPoints.LeftBottom.x, frustumPoints.LeftBottom.z );
			var max           = new Vector2( frustumPoints.RightTop.x, frustumPoints.RightTop.z );

			_areas = new Rect[]
			{
				new Rect( min.x - SpawnZoneWidth, max.y - SpawnZoneWidth, SpawnZoneWidth, Math.Abs(max.y - min.y) + SpawnZoneWidth * 2),
				new Rect( max.x, max.y - SpawnZoneWidth, SpawnZoneWidth, Math.Abs(max.y - min.y) + SpawnZoneWidth * 2),
				new Rect( min.x - SpawnZoneWidth, min.y, Math.Abs( max.x - min.x ) + SpawnZoneWidth * 2, SpawnZoneWidth ),
				new Rect( min.x - SpawnZoneWidth, max.y - SpawnZoneWidth, Math.Abs( max.x - min.x ) + SpawnZoneWidth * 2, SpawnZoneWidth ),
			};
		}

		private Vector3 GetRandomPoint()
		{
			var rndArea = Random.Range( 0, _areas.Length );
			var area    = _areas[rndArea];

			return area.RandomPoint().x0y();
		}

		private Vector3 GetRandomDirection(Vector3 origin)
		{
			var dir  = _hero.Position.Value - origin;
			var q1   = Quaternion.Euler( 0, 0.5f * DirectionSector, 0 );
			var q2   = Quaternion.Euler( 0, -0.5f * DirectionSector, 0 );
			var dir1 = q1 * dir;
			var dir2 = q2 * dir;
			var rnd  = Random.Range( 0, 1f );

			return Vector3.Slerp( dir1, dir2, rnd );
		}
	}
}