namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public abstract class BaseView : MonoBehaviour, ITransformableView, ICollidedView
	{
		public event Action<CollisionInfo> Collided;
		
		public Vector3 Position
		{
			get => transform.position; 
			set => transform.position = value;
		}

		public Quaternion Rotation
		{
			get => transform.rotation; 
			set => transform.rotation = value;
		}

		public abstract ECollisionLayer Layer { get; }
	}
}