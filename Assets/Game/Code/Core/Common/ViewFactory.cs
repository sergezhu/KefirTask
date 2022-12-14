namespace Game.Code.Core.Common
{
	using System;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Configs;
	using Game.Code.View.Common;
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
				case EEntityType.Bullet:
					return Object.Instantiate( _prefabsConfig.BulletPrefab );
				default:
					throw new ArgumentOutOfRangeException( nameof(type), type, null );
			}
		}
	}
}