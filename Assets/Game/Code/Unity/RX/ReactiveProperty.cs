namespace Game.Code.Unity.RX
{
	using System;

	public class ReactiveProperty<T> where T : IEquatable<T>
	{
		public event Action<T> Changed;

		private T _value;
		
		public T Value
		{
			get => _value;
			set 
			{
				if ( _value.Equals(value) )
				{
					_value = value;
					Changed?.Invoke( _value );
				}
			}
		}
	}
}