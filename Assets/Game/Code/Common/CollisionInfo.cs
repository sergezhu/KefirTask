namespace Game.Code.Core.Collisions
{
	using Game.Code.Core.Enums;
	using UnityEngine;

	public struct CollisionInfo
	{
		public EEntityType OtherEntityType;
		public Vector3 OtherVelocity;
	}
}