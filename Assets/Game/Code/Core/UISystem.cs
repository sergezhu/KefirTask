namespace Game.Code.Core
{
	using Game.Code.Core.Common;
	using Game.Code.Core.Scores;
	using Game.Code.Core.Ship;
	using Game.Code.Core.UI;
	using Game.Code.View.UI;

	public class UISystem
	{
		private readonly LaserChargeBlocksViewFactory _laserChargeBlocksViewFactory;
		private readonly HeroFacade _heroFacade;
		private readonly UIHudView _uiHudView;
		private readonly UIResultScreenView _uiResultScreenView;
		private readonly ScoresSystem _scoresSystem;
		private readonly RestartService _restartService;
		private readonly UIHudPresenter _uiHudPresenter;
		private readonly UIResultScreenPresenter _uiResultScreenPresenter;


		public UISystem( LaserChargeBlocksViewFactory laserChargeBlocksViewFactory, HeroFacade heroFacade, UIHudView uiHudView,
						 UIResultScreenView uiResultScreenView, ScoresSystem scoresSystem, RestartService restartService )
		{
			_laserChargeBlocksViewFactory = laserChargeBlocksViewFactory;
			_heroFacade = heroFacade;
			_uiHudView = uiHudView;
			_uiResultScreenView = uiResultScreenView;
			_scoresSystem = scoresSystem;
			_restartService = restartService;

			_uiHudPresenter = new UIHudPresenter( _uiHudView, _laserChargeBlocksViewFactory, _heroFacade, _scoresSystem );
			_uiResultScreenPresenter = new UIResultScreenPresenter( _uiResultScreenView, _heroFacade, _scoresSystem );
			
			_uiHudPresenter.Show();
			_uiResultScreenPresenter.Hide();

			Subscribe();
		}

		private void Subscribe()
		{
			_uiResultScreenPresenter.RestartRequest += OnRestartRequest;
		}

		private void Unsubscribe()
		{
			_uiResultScreenPresenter.RestartRequest -= OnRestartRequest;
		}

		private void OnRestartRequest()
		{
			Unsubscribe();
			
			_restartService.Restart(); 
		}
	}
}