namespace Game.Code.Unity.Weapons
{
	using UnityEngine;

	public class BulletCannonView : MonoBehaviour
	{
		[SerializeField] private Transform _shootPoint;

		public Transform ShootPoint => _shootPoint;
	}
}