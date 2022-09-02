namespace Game.Code.Unity.Weapons
{
	using System.Collections;
	using UnityEngine;

	public class LaserCannonView : MonoBehaviour
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private TrailRenderer _trail;

		private Coroutine _coroutine;

		public Transform ShootPoint => _shootPoint;

		private void Awake()
		{
			HideLaser();
		}

		public void ShowLaser( Vector3 position, Vector3 direction, float duration )
		{
			if(_coroutine != null)
				StopCoroutine( _coroutine );

			_coroutine = StartCoroutine( ShowLaserRoutine( position, direction, duration ) );
		}

		private IEnumerator ShowLaserRoutine( Vector3 position, Vector3 direction, float duration )
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
			_trail.enabled = true;
		}

		private void HideLaser()
		{
			_trail.enabled = false;
		}
	}
}