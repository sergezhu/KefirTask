﻿namespace Game.Code.Unity.Common
{
	using UnityEngine;

	public interface ITransformableView
	{
		Vector3 Position { get; set; }
		Quaternion Rotation { get; set; }
		Vector3 Velocity { get; set; }
	}
}