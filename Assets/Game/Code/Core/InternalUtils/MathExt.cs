namespace Game.Code.Core.InternalUtils
{
	using System;
	using System.Numerics;

	public static class MathExt
	{
		public const float PI = (float) Math.PI;
		
		public static float Clamp( float f, float a, float b ) => Math.Clamp( f, a, b );
		public static float Abs( float f ) => Math.Abs( f );
		public static float Sign( float f ) => Math.Sign( f );
		public static float Sin( float radians ) => (float) Math.Sin( radians );
		public static float Asin( float radians ) => (float) Math.Asin( radians );
		public static float Cos( float radians ) => (float) Math.Cos( radians );
		public static float Acos( float radians ) => (float) Math.Acos( radians );
		public static float Lerp( float a, float b, float t ) => a + (b - a) * t;
		public static float LerpAngle( float a, float b, float t )
		{
			float num = Repeat( b - a, 360f );
			if ( num > 180.0 )
				num -= 360f;
			
			return a + num * Clamp( t, 0, 1f );
		}

		public static float Repeat( float t, float length ) => (float) Math.Clamp( t - Math.Floor( t / length ) * length, 0.0f, length );


		public static Vector3 Normalize( this Vector3 v )
		{
			var len = v.Length() + float.Epsilon;
			return new Vector3( v.X / len, v.Y / len, v.Z / len );
		}
	}
}