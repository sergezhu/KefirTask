namespace Game.Code.Unity.Utils
{
	using SNVector3 = System.Numerics.Vector3;
	using UNIVector3 = UnityEngine.Vector3;
	using SNQuaternion = System.Numerics.Quaternion;
	using UNIQuaternion = UnityEngine.Quaternion;

	public static class TypesConvert
	{
		public static UNIVector3 ToUnityVector3( this SNVector3 v ) => new ( v.X, v.Y, v.Z );
		public static SNVector3 ToNumericsVector3( this UNIVector3 v ) => new ( v.x, v.y, v.z );
		public static UNIQuaternion ToUnityQuaternion( this SNQuaternion q ) => new UNIQuaternion( q.X, q.Y, q.Z, q.W );
		public static SNQuaternion ToNumericsQuaternion( this UNIQuaternion q ) => new SNQuaternion( q.x, q.y, q.z, q.w );
	}
}