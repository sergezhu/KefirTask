namespace Game.Code.Core.InternalTypes
{
	using System;

	[Serializable]
	public class Vector3Internal
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3Internal( float x, float y, float z )
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3Internal operator +( Vector3Internal a, Vector3Internal b )
			=> new Vector3Internal( a.X + b.X, a.Y + b.Y, a.Z + b.Z );

		public static Vector3Internal operator -( Vector3Internal a, Vector3Internal b )
			=> new Vector3Internal( a.X - b.X, a.Y - b.Y, a.Z - b.Z );
	}
}