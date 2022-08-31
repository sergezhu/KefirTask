namespace Game.Code.Unity.Ship
{
	using UnityEngine;

	public interface IShipView
	{
		void SetPosition( Vector3 position );
		void SetRotation( float rotation );
	}
	
	public class DefaultShipView : MonoBehaviour, IShipView
	{
		public void SetPosition( Vector3 position )
		{
			transform.position = position;
		}

		public void SetRotation( float rotation )
		{
			transform.rotation = Quaternion.Euler( 0, rotation * Mathf.Rad2Deg, 0 );
		}
	}
}