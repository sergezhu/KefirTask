namespace Game.Code.Unity.Spawn
{
	using System;
	using System.Linq;
	using Game.Code.Unity.Camera;
	using Game.Code.Unity.Utils;
	using UnityEngine;

	public class SpawnTimer
	{
		public event Action<SpawnData> SpawnRequest;

		private const float SpawnZoneWidth = 5;
		
		private readonly CameraController _cameraController;
		private readonly float _delay;
		
		private Rect[] _areas;

		public SpawnTimer(CameraController cameraController, float delay)
		{
			_cameraController = cameraController;
			_delay = delay;

			SetupAreas();
		}

		public void Tick( float deltaTime )
		{
			
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
	}
}