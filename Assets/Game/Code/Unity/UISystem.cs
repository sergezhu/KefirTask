namespace Game.Code.Unity
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Scores;
	using Game.Code.Unity.Ship;
	using Game.Code.Unity.UI;

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