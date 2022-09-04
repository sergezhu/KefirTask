namespace Game.Code.Unity.Weapons
{
	public class LaserCharge
	{
		private readonly float _recallDelay;
		
		private RecallTimer _recallTimer;

		public float RecallProgress => _recallTimer.Progress;
		public bool IsReady => _recallTimer.IsReady;

		public LaserCharge( float recallDelay )
		{
			_recallDelay = recallDelay;
		}

		public void Tick( float deltaTime )
		{
			_recallTimer.Tick( deltaTime );
		}

		public void Recall() => _recallTimer.Recall();
	}
}