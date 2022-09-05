namespace Game.Code.View.Common
{
	using System;
	using Game.Code.Common;
	using Game.Code.Common.Enums;
	using UnityEngine;

	public interface ICollidedView
	{
		event Action<CollisionInfo> Collided;
		
		EEntityType Type { get; }
		Collider[] Colliders { get; }
	}
}