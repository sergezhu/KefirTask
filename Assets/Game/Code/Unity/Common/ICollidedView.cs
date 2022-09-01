namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Core.Physics;

	public interface ICollidedView
	{
		event Action<CollisionInfo> Collided;
	}
}