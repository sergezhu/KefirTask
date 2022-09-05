namespace Game.Code.Core.UI
{
	using System.Linq;
	using Game.Code.Core.Scores;
	using Game.Code.Core.Ship;
	using Game.Code.Core.Weapons;
	using Game.Code.View.UI;

	public class UIHudPresenter
	{
		private readonly UIHudView _view;
		private readonly LaserChargeBlocksViewFactory _laserChargeBlocksViewFactory;
		private readonly HeroFacade _heroFacade;
		private readonly ScoresSystem _scoresSystem;

		private readonly LaserCharge[] _laserCharges;
		private readonly LaserChargeBlockView[] _laserChargesViews;

		public UIHudPresenter( UIHudView view, LaserChargeBlocksViewFactory laserChargeBlocksViewFactory, HeroFacade heroFacade, ScoresSystem scoresSystem )
		{
			_view = view;
			_laserChargeBlocksViewFactory = laserChargeBlocksViewFactory;
			_heroFacade = heroFacade;
			_scoresSystem = scoresSystem;

			_laserCharges = _heroFacade.LaserCharges.ToArray();
			_laserChargesViews = new LaserChargeBlockView[_laserCharges.Length];
			
			CreateAndBindLaserCharges();
			BindMoveParameters();
			BindScores();
		}

		private void CreateAndBindLaserCharges()
		{
			for ( var i = 0; i < _laserCharges.Length; i++ )
			{
				var laserChargeView = _laserChargeBlocksViewFactory.Create();
				_laserChargesViews[i] = laserChargeView;
				_view.AttachLaserChargeBlock( _laserChargesViews[i], i + 1 );

				LaserChargeBind( _laserCharges[i], _laserChargesViews[i] );

				_laserChargesViews[i].SetReadyState( true );
				_laserChargesViews[i].SetRecallProgress( 0 );
			}
		}

		private void LaserChargeBind( LaserCharge laserCharge, LaserChargeBlockView laserChargesView )
		{
			laserCharge.IsReady.Changed += laserChargesView.SetReadyState;
			laserCharge.RecallProgress.Changed += laserChargesView.SetRecallProgress;
		}

		private void BindMoveParameters()
		{
			_heroFacade.Position.Changed += _view.SetPositionText;
			_heroFacade.CurrentSpeed.Changed += _view.SetCurrentSpeedText;
			_heroFacade.CurrentDirectionAngle.Changed += _view.SetCurrentAngleText;

			_view.SetPositionText( _heroFacade.Position.Value );
			_view.SetCurrentSpeedText( _heroFacade.CurrentSpeed.Value );
			_view.SetCurrentAngleText( _heroFacade.CurrentDirectionAngle.Value );
		}

		private void BindScores()
		{
			_scoresSystem.CurrentScores.Changed += _view.SetScoresText;
			
			_view.SetScoresText( _scoresSystem.CurrentScores.Value );
		}
	}
}