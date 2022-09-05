namespace Game.Code.Core.Scores
{
	using System.Collections.Generic;
	using Game.Code.Core.Configs;
	using Game.Code.Core.Enums;
	using Game.Code.Core.RX;

	public class ScoresSystem
	{
		private readonly RewardConfig _rewardConfig;
		private readonly Dictionary<EEntityType, int> _rewardDictionary;
		
		public ReactiveProperty<int> CurrentScores { get; } = new();

		public ScoresSystem(RewardConfig rewardConfig)
		{
			_rewardConfig = rewardConfig;

			_rewardDictionary = new Dictionary<EEntityType, int>
			{
				{EEntityType.Asteroid, _rewardConfig.AsteroidReward},
				{EEntityType.AsteroidPart, _rewardConfig.AsteroidPartReward},
				{EEntityType.Enemy, _rewardConfig.EnemyReward},
			};
		}

		public void AddScoresByType( EEntityType type )
		{
			CurrentScores.Value += _rewardDictionary[type];
		}
	}
}