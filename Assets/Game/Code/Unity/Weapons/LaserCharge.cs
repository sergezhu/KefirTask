namespace Game.Code.Unity.Weapons
{
	using Game.Code.Unity.RX;

	public class LaserCharge
	{
		private readonly float _recallDelay;
		private readonly RecallTimer _recallTimer;

		public ReactiveProperty<float> RecallProgress { get; } = new();
		public ReactiveProperty<bool> IsReady { get; } = new();

		public LaserCharge( float recallDelay )
		{
			_recallDelay = recallDelay;
			_recallTimer = new RecallTimer( recallDelay );
		}

		public void Tick( float deltaTime )
		{
			_recallTimer.Tick( deltaTime );

			RecallProgress.Value = _recallTimer.Progress;
			IsReady.Value = _recallTimer.IsReady;
		}

		public void Recall() => _recallTimer.Recall();
	}
}