﻿namespace Game.Code.Unity.UI
{
	using System.Linq;
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Ship;
	using Game.Code.Unity.Utils;
	using Game.Code.Unity.Weapons;

	public class UIHudPresenter
	{
		private readonly UIHudView _view;
		private readonly LaserChargeBlocksViewFactory _laserChargeBlocksViewFactory;
		private readonly HeroFacade _heroFacade;
		
		private readonly LaserCharge[] _laserCharges;
		private readonly LaserChargeBlockView[] _laserChargesViews;

		public UIHudPresenter(UIHudView view, LaserChargeBlocksViewFactory laserChargeBlocksViewFactory, HeroFacade heroFacade)
		{
			_view = view;
			_laserChargeBlocksViewFactory = laserChargeBlocksViewFactory;
			_heroFacade = heroFacade;

			_laserCharges = _heroFacade.LaserCharges.ToArray();
			_laserChargesViews = new LaserChargeBlockView[_laserCharges.Length];
			
			CreateAndBindLaserCharges();
			BindMoveParameters();
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
	}
}