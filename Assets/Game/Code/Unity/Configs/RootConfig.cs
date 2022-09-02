namespace Game.Configs
{
	using Game.Code.Unity.Enums;
	using UnityEngine;


	[CreateAssetMenu( fileName = "Root", menuName = "Configs/Root", order = (int)EConfig.Root )]
	public class RootConfig : ScriptableObject
	{
		public ShipConfig Ship;
		public AsteroidsConfig Asteroids;
		public EnemiesConfig Enemies;
		public RewardConfig Reward;
		public ViewPrefabsConfig ViewPrefabs;
	}
}

