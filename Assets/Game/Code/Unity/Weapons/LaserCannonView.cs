namespace Game.Code.Unity.Weapons
{
	using System.Collections;
	using UnityEngine;

	public class LaserCannonView : MonoBehaviour
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private LineRenderer _ray;

		private Coroutine _coroutine;
		private float _lightTimerStart;
		private float _lightTimerEnd;
		private Vector4 _emissionDefaultColor;
		private Material _newMaterial;

		public Transform ShootPoint => _shootPoint;

		private void Awake()
		{
			_newMaterial  = new Material( _ray.material );
			_ray.material = _newMaterial;
			
			_emissionDefaultColor = _newMaterial.GetVector( "_EmissionColor" );

			HideLaserInternal();
		}

		private void Update()
		{
			UpdateLightIntensity();
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
			ShowLaserInternal( duration );

			var waiter = duration > 0.01f
				? new WaitForSeconds( duration )
				: null;
			
			yield return waiter;
			
			HideLaserInternal();
		}

		private void ShowLaserInternal( float duration )
		{
			_ray.enabled = true;

			_lightTimerStart = Time.time;
			_lightTimerEnd   = _lightTimerStart + duration;
		}

		private void HideLaserInternal()
		{
			_ray.enabled = false;
		}

		private void UpdateLightIntensity()
		{
			var t = Time.time;
			var canUpdate = t >= _lightTimerStart && t <= _lightTimerEnd;

			if ( !canUpdate )
				return;

			var relativeIntensity = (_lightTimerEnd - t) / (_lightTimerEnd - _lightTimerStart);

			_newMaterial.SetVector( "_EmissionColor", _emissionDefaultColor * relativeIntensity );
		}
	}
}