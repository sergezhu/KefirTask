namespace Game.Code.Unity.Weapons
{
	using System.Collections;
	using UnityEngine;

	public class LaserCannonView : MonoBehaviour
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private LineRenderer _ray;

		private Coroutine _coroutine;

		public Transform ShootPoint => _shootPoint;

		private void Awake()
		{
			HideLaser();
		}

		public void ShowLaser( float duration )
		{
			if(_coroutine != null)
				StopCoroutine( _coroutine );

			_coroutine = StartCoroutine( ShowLaserRoutine( duration ) );
		}

		public void SetLaserSize( Vector3 size )
		{
			_ray.transform.localScale = size;
		}

		private IEnumerator ShowLaserRoutine( float duration )
		{
			ShowLaser();

			var waiter = duration > 0.01f
				? new WaitForSeconds( duration )
				: null;
			
			yield return waiter;
			
			HideLaser();
		}

		private void ShowLaser()
		{
			_ray.enabled = true;
		}

		private void HideLaser()
		{
			_ray.enabled = false;
		}
	}
}