namespace Game.Code.Unity.Spawn
{
	using System;
	using Game.Code.Core.Move;
	using Game.Code.Unity.Camera;
	using Game.Code.Unity.Utils;
	using UnityEngine;
	using Random = UnityEngine.Random;

	public class SpawnTimer
	{
		public event Action<SpawnData> SpawnRequest;

		private const float SpawnZoneWidth = 5;
		private const float DirectionSector = 90;
		
		private readonly CameraController _cameraController;
		private readonly Mover _heroMover;
		private readonly float _delay;

		private Rect[] _areas;
		private float _startTime;
		private float _endTime;

		public SpawnTimer(CameraController cameraController, float delay, Mover heroMover)
		{
			_cameraController = cameraController;
			_delay            = delay;
			_heroMover        = heroMover;

			ResetTimer();
			SetupAreas();
		}

		public void Tick( float deltaTime )
		{
			var time = Time.time;

			if ( time < _endTime )
				return;

			var pos  = GetRandomPoint();
			var dir  = GetRandomDirection( pos );
			var data = new SpawnData() {Position = pos, Direction = dir};

			SpawnRequest?.Invoke( data );
			
			ResetTimer();
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
			var dir  = _heroMover.Position.ToUnityVector3() - origin;
			var q1   = Quaternion.Euler( 0, 0.5f * DirectionSector, 0 );
			var q2   = Quaternion.Euler( 0, -0.5f * DirectionSector, 0 );
			var dir1 = q1 * dir;
			var dir2 = q2 * dir;
			var rnd  = Random.Range( 0, 1f );

			return Vector3.Slerp( dir1, dir2, rnd );
		}
	}
}