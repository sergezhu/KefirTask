namespace Game.Code.Core.Common
{
	using Game.Code.Common.Enums;
	using Game.Code.Core.Weapons;
	using Game.Code.View.Weapons;

	public class BulletViewFactory
	{
		private readonly ViewFactory _viewFactory;

		public BulletViewFactory(ViewFactory viewFactory)
		{
			_viewFactory = viewFactory;
		}

		public BulletView Create() => _viewFactory.Create( EEntityType.Bullet ) as BulletView;
	}
}