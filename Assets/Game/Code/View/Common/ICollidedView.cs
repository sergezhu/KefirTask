namespace Game.Code.Core.Common
{
	using System;
	using Game.Code.Core.Collisions;
	using Game.Code.Core.Enums;
	using UnityEngine;

	public interface ICollidedView
	{
		event Action<CollisionInfo> Collided;
		
		EEntityType Type { get; }
		Collider[] Colliders { get; }
	}
}