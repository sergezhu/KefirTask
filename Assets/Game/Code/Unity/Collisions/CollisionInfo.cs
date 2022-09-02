namespace Game.Code.Unity.Collisions
{
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public struct CollisionInfo
	{
		public EEntityType OtherEntityType;
		public Vector3 OtherVelocity;
	}
}