namespace Game.Code.Core.InternalUtils
{
	using System;

	public static class MathExt
	{
		public static float Clamp( float f, float a, float b ) => Math.Clamp( f, a, b );
		public static float Abs( float f ) => Math.Abs( f );
		public static float Sin( float radians ) => (float) Math.Sin( radians );
		public static float Cos( float radians ) => (float) Math.Cos( radians );
		public static float Lerp( float a, float b, float t ) => a + (b - a) * t;
	}
}