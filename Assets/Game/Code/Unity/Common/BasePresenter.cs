namespace Game.Code.Unity.Common
{
	using System;

	public abstract class BasePresenter : ITickable
	{
		public event Action<DestroyInfo> DestroyRequest;
		
		protected BaseView View;
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