namespace Game.Code.Core.Configs
{
	using Game.Code.Common.Enums;
	using UnityEngine;

	[CreateAssetMenu( fileName = "Reward", menuName = "Configs/Reward", order = (int) EConfig.Reward )]
	public class RewardConfig : ScriptableObject
	{
		public int AsteroidPartReward = 5;
		public int AsteroidReward = 25;
		public int EnemyReward = 50;
	}
}