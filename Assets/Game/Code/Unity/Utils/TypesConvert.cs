namespace Game.Code.Unity.Utils
{
	using Game.Code.Core.InternalTypes;
	using UnityEngine;

	public static class TypesConvert
	{
		public static Vector3 ToUnityVector3( this Vector3Internal v ) => new Vector3( v.X, v.Y, v.Z );
		public static Vector3Internal ToInternalVector3( this Vector3 v ) => new Vector3Internal( v.x, v.y, v.z );
	}
}