namespace Game.Code.Unity.Asteroids
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using UnityEngine;

	public class AsteroidPartsFactory
	{
		private readonly ViewFactory _viewFactory;
		private readonly AsteroidsConfig _asteroidsConfig;

		public AsteroidPartsFactory(ViewFactory viewFactory, AsteroidsConfig asteroidsConfig)
		{
			_viewFactory = viewFactory;
			_asteroidsConfig  = asteroidsConfig;
		}

		public AsteroidPartView[] Create( SourceAsteroidData data )
		{
			var views = new AsteroidPartView[_asteroidsConfig.RandomDestroyParts];

			for ( int i = 0; i < views.Length; i++ )
			{
				var view = _viewFactory.Create( EEntityType.AsteroidPart ) as AsteroidPartView;
				view.Position = data.Position;
				
				var partVelocity = data.Velocity + _asteroidsConfig.RandomPartSpeed * Random.onUnitSphere;
				partVelocity.y = 0;
				view.Velocity  = partVelocity;

				views[i] = view;
			}

			return views;
		}
	}
}