namespace Game.Code.Unity.Collisions
{
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public struct CollisionInfo
	{
		public ECollisionLayer OtherLayer;
		public Vector3 OtherVelocity;
	}
}