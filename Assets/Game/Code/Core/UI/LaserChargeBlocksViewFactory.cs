namespace Game.Code.Core.UI
{
	using Game.Code.Core.Enums;

	public class LaserChargeBlocksViewFactory
	{
		private readonly UIViewFactory _viewFactory;

		public LaserChargeBlocksViewFactory( UIViewFactory viewFactory )
		{
			_viewFactory = viewFactory;
		}

		public LaserChargeBlockView Create() => _viewFactory.Create( EUIEntityType.LaserChargeBlock ) as LaserChargeBlockView;
	}
}