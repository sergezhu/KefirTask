namespace Game.Code.Core
{
	using Game.Code.Core.Configs;
	using Game.Code.Core.Scores;
	using Game.Code.Core.Ship;
	using Game.Code.Core.UI;
	using Game.Code.View.UI;

	public class UISystem
	{
		private readonly RootConfig _rootConfig;
		private readonly UIViewFactory _uiViewFactory;
		private readonly LaserChargeBlocksViewFactory _laserChargeBlocksViewFactory;
		private readonly HeroFacade _heroFacade;
		private readonly UIHudView _uiHudView;
		private readonly ScoresSystem _scoresSystem;
		private readonly UIHudPresenter _uiHudPresenter;


		public UISystem( RootConfig rootConfig, UIViewFactory uiViewFactory, LaserChargeBlocksViewFactory laserChargeBlocksViewFactory, HeroFacade heroFacade,
						 UIHudView uiHudView, ScoresSystem scoresSystem )
		{
			_rootConfig = rootConfig;
			_uiViewFactory = uiViewFactory;
			_laserChargeBlocksViewFactory = laserChargeBlocksViewFactory;
			_heroFacade = heroFacade;
			_uiHudView = uiHudView;
			_scoresSystem = scoresSystem;

			_uiHudPresenter = new UIHudPresenter( _uiHudView, _laserChargeBlocksViewFactory, _heroFacade, _scoresSystem );
		}
	}
}