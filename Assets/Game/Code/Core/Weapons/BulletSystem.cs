namespace Game.Code.Core.Weapons
{
	using System.Collections.Generic;
	using Game.Code.Core.Common;

	public class BulletSystem : ITickable
	{
		private List<BulletModel> _bullets;

		public BulletSystem()
		{
			_bullets = new List<BulletModel>();
		}

		public void Tick( float deltaTime )
		{
			_bullets.ForEach( b => b.Tick( deltaTime ) );
		}

		public void Add( BulletModel bullet )
		{
			if ( _bullets.Contains( bullet ) )
				return;
			
			_bullets.Add( bullet );
		}

		public void Remove( BulletModel bullet )
		{
			if ( _bullets.Contains( bullet ) == false)
				return;
			
			_bullets.Remove( bullet );
		}
	}
}