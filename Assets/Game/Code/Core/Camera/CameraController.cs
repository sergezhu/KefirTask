namespace Game.Code.Core.Camera
{
	using UnityEngine;

	public class CameraController : MonoBehaviour
	{
		private Camera _camera;

		void Awake()
		{
			_camera = Camera.main;
		}

		public Vector3 GetPointAtHeight(float screenX, float screenY, float height)
		{
			Ray ray = _camera.ViewportPointToRay(new Vector3(screenX, screenY, 0));
			Vector3 atHeight = ray.origin + (ray.origin.y - height) / -ray.direction.y * ray.direction;
			
			return atHeight;
		}

		public FrustumData GetFrustumPoints()
		{
			Vector3 leftTop     = GetPointAtHeight( 0, 0, 0 );
			Vector3 leftBottom  = GetPointAtHeight( 0, 1, 0 ); 
			Vector3 rightTop    = GetPointAtHeight( 1, 0, 0 );
			Vector3 rightBottom = GetPointAtHeight( 1, 1, 0 );

			return new FrustumData() {LeftTop = leftTop, LeftBottom = leftBottom, RightTop = rightTop, RightBottom = rightBottom};
		}
	}
}

