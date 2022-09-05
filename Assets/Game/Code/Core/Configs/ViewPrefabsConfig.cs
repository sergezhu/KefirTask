namespace Game.Code.Core.Configs
{
	using Game.Code.Common.Enums;
	using Game.Code.Core.Asteroids;
	using Game.Code.Core.Enemies;
	using Game.Code.Core.Ship;
	using Game.Code.Core.UI;
	using Game.Code.Core.Weapons;
	using Game.Code.View.Asteroids;
	using Game.Code.View.Enemy;
	using Game.Code.View.Ship;
	using Game.Code.View.UI;
	using Game.Code.View.Weapons;
	using UnityEngine;

	[CreateAssetMenu( fileName = "ViewPrefabs", menuName = "Configs/ViewPrefabs", order = (int) EConfig.ViewPrefabs )]
	public class ViewPrefabsConfig : ScriptableObject
	{
		public ShipView HeroShipPrefab;
		public EnemyView EnemyPrefab;
		public AsteroidView AsteroidPrefab;
		public AsteroidPartView AsteroidPartPrefab;
		public BulletView BulletPrefab;

		[Space]
		public LaserChargeBlockView LaserChargeBlockPrefab;
	}
}