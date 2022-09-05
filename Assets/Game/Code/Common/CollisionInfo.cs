namespace Game.Code.Common
{
	using Game.Code.Common.Enums;
	using UnityEngine;

	public struct CollisionInfo
	{
		public EEntityType OtherEntityType;
		public Vector3 OtherVelocity;
	}
}