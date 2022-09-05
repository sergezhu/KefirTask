namespace Game.Code.Unity.Common
{
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.UI;

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