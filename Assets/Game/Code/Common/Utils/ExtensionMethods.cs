namespace Game.Code.Common.Utils
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public static class ExtensionMethods
	{
#region Random

		public static int GetRandomWeightedIndex( this List<float> weights )
		{
			// https://forum.unity.com/threads/random-numbers-with-a-weighted-chance.442190/#post-5173340

			if (weights == null || weights.Count == 0)
				return -1;

			float total			= 0;
			for (int i = 0; i < weights.Count; i ++)
			{
				float w			= weights[ i ];

				if (float.IsPositiveInfinity( w ))
					return i;

				if (w > 0 && !float.IsNaN( w ))
					total		+= w;
			}

			float random		= UnityEngine.Random.value;

			float sum			= 0;
			for (int i = 0; i < weights.Count; i ++)
			{
				float w			= weights[ i ];

				if (float.IsNaN( w ) || w <= 0)
					continue;

				sum				+= w / total;

				if (sum >= random)
					return i;
			}

			return -1;
		}


		public static T Random<T>( this List<T> list )				=> list[ list.RandomIndex() ];
		public static int RandomIndex<T>( this List<T> list )		=> UnityEngine.Random.Range( 0, list.Count );
		public static T PopRandom<T>( this List<T> list )			=> list.Pop( list.RandomIndex() );
		public static T Pop<T>( this List<T> list, int index )
		{
			T item				= list[ index ];
			list[ index ]		= list[ list.Count - 1 ];
			list.RemoveAt( list.Count - 1 );
			return item;
		}

#endregion
#region Rect

		public static Vector2 RandomPoint( this Rect rect )
		{
			float x				= UnityEngine.Random.Range( rect.min.x, rect.max.x );
			float y				= UnityEngine.Random.Range( rect.min.y, rect.max.y );

			return new Vector2( x, y );
		}
#endregion
#region RectInt

		public static RectInt Intersection( this RectInt rect, RectInt other )
		{
			Vector2Int min		= Vector2Int.Max( rect.min, other.min );
			Vector2Int max		= Vector2Int.Min( rect.max, other.max );

			return new RectInt( min, max - min );
		}

		public static RectInt Shrink( this RectInt r, int shrink = 1 )					=> r.Grow( shrink * (-1) );
		public static RectInt Grow( this RectInt r, int extent = 1 )					=> r.Grow( Vector2Int.one * extent );
		public static RectInt Grow( this RectInt r, int extentX, int extentY )			=> r.Grow( new Vector2Int( extentX, extentY ) );
		public static RectInt Grow( this RectInt r, Vector2Int extent )					=> r.Grow( extent, extent );
		public static RectInt Grow( this RectInt r, Vector2Int extentMin, Vector2Int extentMax )
		=>
			new RectInt(
				r.min	- extentMin,
				r.size	+ extentMin + extentMax
			);

#endregion
#region Vector2,3

		public static Vector2 MapCellCenter( this Vector2Int v )	=> v + Vector2.one / 2;


		public static Vector2Int RoundToInt( this Vector2 v )		=> new Vector2Int( Mathf.RoundToInt( v.x ), Mathf.RoundToInt( v.y ) );
		public static Vector2Int FloorToInt( this Vector2 v )		=> new Vector2Int( Mathf.FloorToInt( v.x ), Mathf.FloorToInt( v.y ) );
		public static Vector2Int CeilToInt( this Vector2 v )		=> new Vector2Int( Mathf.CeilToInt( v.x ), Mathf.CeilToInt( v.y ) );
		public static Vector2Int Mod( this Vector2Int v, int mod )			=> new Vector2Int( v.x % mod, v.y % mod );
		public static int Sum( this Vector2Int v )					=> v.x + v.y;
		public static float Sum( this Vector2 v )					=> v.x + v.y;
		public static float Min( this Vector2 v )					=> Mathf.Min( v.x, v.y );
		public static float Max( this Vector2 v )					=> Mathf.Max( v.x, v.y );
		public static int Area( this Vector2Int v )					=> v.x * v.y;
		public static Vector2Int Abs( this Vector2Int v )			=> new Vector2Int( Mathf.Abs( v.x ), Mathf.Abs( v.y ) );
		public static Vector2 Abs( this Vector2 v )					=> new Vector2( Mathf.Abs( v.x ), Mathf.Abs( v.y ) );
		// public static Vector3 Abs( this Vector3 v )					=> new Vector3( Mathf.Abs( v.x ), Mathf.Abs( v.y ), Mathf.Abs( v.z ) );
		public static Vector2 Sign( this Vector2 v )				=> new Vector2( Mathf.Sign( v.x ), Mathf.Sign( v.y ) );
		public static Vector2 Sign( this Vector2Int v )				=> new Vector2( Mathf.Sign( v.x ), Mathf.Sign( v.y ) );
		public static Vector2 Clamp01( this Vector2 v )				=> new Vector2( Mathf.Clamp01( v.x ), Mathf.Clamp01( v.y ) );
		public static Vector2Int Swap( this Vector2Int v )			=> new Vector2Int( v.y, v.x );

		public static Vector2Int Rotated_cw_90	( this Vector2Int v )						=> Rotated_90( v, true );
		public static Vector2Int Rotated_ccw_90	( this Vector2Int v )						=> Rotated_90( v, false );
		public static Vector2Int Rotated_90	( this Vector2Int v, bool clockWise )			=> new Vector2Int( v.y, -v.x ) * (clockWise ? 1 : -1);
		public static Vector2Int Rotate_90	( ref this Vector2Int v, bool clockWise )		=> v = v.Rotated_90( clockWise );
		public static Vector2 Rotated_90	( this Vector2 v, bool clockWise = true )		=> new Vector2( v.y, -v.x ) * (clockWise ? 1 : -1);


		// Swizzle
		public static Vector3Int x1y( this Vector2Int v )		=> new Vector3Int( v.x, 1, v.y );
		public static Vector3Int x0y( this Vector2Int v )		=> new Vector3Int( v.x, 0, v.y );
		public static Vector3 x0y( this Vector2 v )				=> new Vector3( v.x, 0, v.y );
		public static Vector2 xz( this Vector3 v )				=> new Vector2( v.x, v.z );
		public static Vector4 xyz1( this Vector3 v )			=> new Vector4( v.x, v.y, v.z, 1 );

		// With
		public static Vector3Int WithY( this Vector3Int v, int y )		=> new Vector3Int( v.x, y, v.z );


		public static Vector3 WithXY( this Vector3 v, float x, float y )
		{
			v.x		= x;
			v.y		= y;

			return v;
		}

		public static Vector3 WithXY( this Vector3 v, Vector2 xy )
		{
			return v.WithXY( xy.x, xy.y );
		}

		public static Vector3 WithXZ( this Vector3 v, Vector3 xz )
		{
			v.x		= xz.x;
			v.z		= xz.z;

			return v;
		}

		public static Vector3Int WithXZ( this Vector3Int v, Vector2Int xz )
		{
			v.x		= xz.x;
			v.z		= xz.y;

			return v;
		}

		public static Vector3 WithX( this Vector3 v, float x )			{ v.x = x; return v; }
		public static Vector3 WithY( this Vector3 v, float y )			{ v.y = y; return v; }
		public static Vector3 WithZ( this Vector3 v, float z )			{ v.z = z; return v; }

		public static Vector2 WithX( this Vector2 v, float x )			{ v.x = x; return v; }
		public static Vector2 WithY( this Vector2 v, float y )			{ v.y = y; return v; }

		public static Vector2Int WithX( this Vector2Int v, int x )		{ v.x = x; return v; }
		public static Vector2Int WithY( this Vector2Int v, int y )		{ v.y = y; return v; }

#endregion
#region Transform

		public static void SetXY	( this Transform transform, Vector2 xy )		=> transform.position		= transform.position.WithXY( xy );

		public static void SetZ		( this Transform transform, float z )			=> transform.position		= transform.position.WithZ( z );

#endregion
#region Other

		public static string Reverse( this string s )
		{
			// https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string

			char[] charArray		= s.ToCharArray();
			Array.Reverse( charArray );
			return new string( charArray );
		}


		public static void DestroyChildren( this Transform transform )
		{
			for (int i = transform.childCount - 1; i >= 0; i --)
				GameObject.Destroy( transform.GetChild( i ).gameObject );
		}

		public static void DestroyChildrenEditor(this Transform transform)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
				GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

#endregion
}

