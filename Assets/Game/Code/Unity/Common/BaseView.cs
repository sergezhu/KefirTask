namespace Game.Code.Unity.Common
{
	using System;
	using System.Collections;
	using Game.Code.Unity.Collisions;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	[RequireComponent(typeof(Collider))]
	public abstract class BaseView : MonoBehaviour, ITransformableView, ICollidedView, ILaserDamageableView
	{
		public event Action<CollisionInfo> Collided;
		public event Action LaserHit;

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

		public Vector3 Velocity { get; set; }

		public Collider[] Colliders { get; private set; }

		public abstract EEntityType Type { get; }

		private void Awake()
		{
			Colliders = GetComponents<Collider>();
		}

		private void OnTriggerEnter( Collider other )
		{
			OnTriggerEnterInternal( other );
		}

		public void ApplyLaserDamage()
		{
			LaserHit?.Invoke();
		}

		public void Destroy()
		{
			StartCoroutine( DestroyAnNextFrame() );
		}

		IEnumerator DestroyAnNextFrame()
		{
			yield return null;
			Destroy( gameObject );
		}

		private void OnTriggerEnterInternal( Collider other )
		{
			if ( other.TryGetComponent<BaseView>( out var view ) )
			{
				var info = new CollisionInfo()
				{
					OtherEntityType		= view.Type,
					OtherVelocity		= view.Velocity
				};

				Collided?.Invoke( info );

				//Debug.Log( $"{this.name} -- collided with -- {view.name}" );
			}
		}
	}
}