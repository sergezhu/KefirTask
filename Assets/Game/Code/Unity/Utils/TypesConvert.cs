namespace Game.Code.Unity.Utils
{
	using SNVector3 = System.Numerics.Vector3;
	using UNIVector3 = UnityEngine.Vector3;

	public static class TypesConvert
	{
		public static UNIVector3 ToUnityVector3( this SNVector3 v ) => new ( v.X, v.Y, v.Z );
		public static SNVector3 ToNumericsVector3( this UNIVector3 v ) => new ( v.x, v.y, v.z );
	}
}