namespace Game.Code.Core.Weapons
{
	using System;
	using UnityEngine;

	public class RecallTimer
	{
		private readonly float _recallDelay;

		private float _recallTimer;

		public float Progress => _recallTimer / _recallDelay;
		public bool IsReady => Mathf.Approximately( Progress, 0 );

		public RecallTimer( float recallDelay )
		{
			_recallDelay = recallDelay;

			if ( _recallDelay < float.Epsilon )
				throw new InvalidOperationException( "RecallTimer must have delay greater than 0" );
		}

		public void Tick( float deltaTime )
		{
			_recallTimer = Mathf.Clamp( _recallTimer - deltaTime, 0, _recallDelay );
		}

		public void Recall() => _recallTimer = _recallDelay;
	}
}