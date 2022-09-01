namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Enums;

	public interface ICollidedView
	{
		event Action<CollisionInfo> Collided;
		
		ECollisionLayer Layer { get; }
	}
}