namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using UnityEngine;
	using Object = UnityEngine.Object;

	public class ViewFactory
	{
		private readonly ViewPrefabsConfig _prefabsConfig;

		public ViewFactory( ViewPrefabsConfig prefabsConfig )
		{
			_prefabsConfig = prefabsConfig;
		}
		
		public BaseView Create( EEntityType type )
		{
			switch ( type )
			{
				case EEntityType.Ship:
					return Object.Instantiate( _prefabsConfig.HeroShipPrefab );
				case EEntityType.Enemy:
					return Object.Instantiate( _prefabsConfig.EnemyPrefab );
				case EEntityType.Asteroid:
					return Object.Instantiate( _prefabsConfig.AsteroidPrefab );
				case EEntityType.AsteroidPart:
					return Object.Instantiate( _prefabsConfig.AsteroidPartPrefab );
				default:
					throw new ArgumentOutOfRangeException( nameof(type), type, null );
			}
		}
	}
}