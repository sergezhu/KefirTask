﻿namespace Game.Code.Unity.Enemies
{
	using Game.Code.Unity.Common;
	using Game.Code.Unity.Enums;

	public class EnemyView : BaseView
	{
		public override ECollisionLayer Layer => ECollisionLayer.Asteroid;
	}
}