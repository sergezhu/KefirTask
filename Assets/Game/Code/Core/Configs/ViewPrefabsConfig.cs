namespace Game.Code.Core.Configs
{
	using Game.Code.Core.Asteroids;
	using Game.Code.Core.Enemies;
	using Game.Code.Core.Enums;
	using Game.Code.Core.Ship;
	using Game.Code.Core.UI;
	using Game.Code.Core.Weapons;
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