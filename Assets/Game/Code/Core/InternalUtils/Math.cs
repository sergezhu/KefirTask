namespace Game.Code.Core.InternalUtils
{
	using UnityEngine;

	public static class Math
	{
		// Here may be funcs implementations not from UnityEngine
		
		public static float Clamp( float f, float a, float b ) => Mathf.Clamp( f, a, b );
		public static float Abs( float f ) => Mathf.Abs( f );
		public static float Sin( float radians ) => Mathf.Sin( radians );
		public static float Cos( float radians ) => Mathf.Cos( radians );
		public static float Lerp( float a, float b, float t ) => Mathf.Lerp( a, b, t );
	}
}