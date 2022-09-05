namespace Game.Code.Unity.Common
{
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Weapons;

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