﻿namespace Game.Code.Unity.Asteroids
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;

	public class AsteroidPartView : BaseView
	{
		public override ECollisionLayer Layer => ECollisionLayer.AsteroidPart;
	}
}