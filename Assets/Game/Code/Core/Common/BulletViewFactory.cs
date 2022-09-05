namespace Game.Code.Core.Common
{
	using Game.Code.Core.Enums;
	using Game.Code.Core.Weapons;

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