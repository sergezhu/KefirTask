namespace Game.Code.Core.UI
{
	using System;
	using Game.Code.Core.Scores;
	using Game.Code.Core.Ship;
	using Game.Code.View.UI;

	public class UIResultScreenPresenter
	{
		public event Action RestartRequest;
		
		private readonly UIResultScreenView _view;
		private readonly HeroFacade _hero;
		private readonly ScoresSystem _scoresSystem;

		public UIResultScreenPresenter( UIResultScreenView view, HeroFacade hero, ScoresSystem scoresSystem )
		{
			_view = view;
			_hero = hero;
			_scoresSystem = scoresSystem;

			Subscribe();
		}

		private void Subscribe()
		{
			_hero.Dead += OnHeroDead;
			_view.RestartClick += OnRestartClick;
		}

		private void OnRestartClick()
		{
			RestartRequest?.Invoke();
		}

		private void OnHeroDead()
		{
			_view.SetScoresText( _scoresSystem.CurrentScores.Value );
			_view.Show();
		}
	}
}