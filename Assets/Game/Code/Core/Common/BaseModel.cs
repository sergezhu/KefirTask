namespace Game.Code.Core.Common
{
	using System;

	public abstract class BaseModel : ITickable
	{
		public event Action<DestroyInfo> DestroyRequest;
		
		protected bool IsDestroyed { get; private set; }

		public abstract void Tick( float deltaTime );

		protected void InvokeDestroy(DestroyInfo info)
		{
			if ( IsDestroyed )
				return;

			IsDestroyed = true;

			DestroyRequest?.Invoke( info );
		}
	}
}