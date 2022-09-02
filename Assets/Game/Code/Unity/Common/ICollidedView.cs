namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public interface ICollidedView
	{
		event Action<CollisionInfo> Collided;
		
		ECollisionLayer Layer { get; }
		Collider[] Colliders { get; }
	}
}