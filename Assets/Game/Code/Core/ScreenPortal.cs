namespace Game.Code.Core
{
	using Game.Code.Core.Camera;
	using Game.Code.Core.Utils;
	using UnityEngine;

	public class ScreenPortal
	{
		private Rect _bounds;

		public ScreenPortal(CameraController cameraController)
		{
			var frustumPoints = cameraController.GetFrustumPoints();
			var min = new Vector2( frustumPoints.LeftBottom.x, frustumPoints.LeftBottom.z );
			var max = new Vector2( frustumPoints.RightTop.x, frustumPoints.RightTop.z );

			_bounds = new Rect( min, max - min );
		}

		public Vector3 RecalculatePosition(Vector3 sourcePosition)
		{
			var screenPos = sourcePosition.xz();

			if ( _bounds.Contains( screenPos, true ) )
				return sourcePosition;

			if ( screenPos.x < _bounds.xMin )
				screenPos.x += _bounds.width;

			if ( screenPos.x > _bounds.xMax )
				screenPos.x -= _bounds.width;

			if ( screenPos.y > _bounds.yMin )
				screenPos.y += _bounds.height;

			if ( screenPos.y < _bounds.yMax )
				screenPos.y -= _bounds.height;

			return screenPos.x0y();
		}
	}
}