namespace Game.Code.Unity.Ship
{
	using UnityEngine;

	public class ShipView : MonoBehaviour, ITransformableView
	{
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
	}
}