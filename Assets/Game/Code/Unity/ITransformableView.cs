namespace Game.Code.Unity
{
	using UnityEngine;

	public interface ITransformableView
	{
		Vector3 Position { get; set; }
		Quaternion Rotation { get; set; }
	}
}