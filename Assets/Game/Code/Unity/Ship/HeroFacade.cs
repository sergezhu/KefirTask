namespace Game.Code.Unity.Ship
{
	using System;
	using UnityEngine;

	public class HeroFacade
	{
		public event Action Dead;
		
		private readonly ShipModel _heroModel;

		public Vector3 Position => _heroModel.Position;
		public Vector3 Velocity => _heroModel.Velocity;

		public HeroFacade( ShipModel heroModel)
		{
			_heroModel = heroModel;
			
			Subscribe();
		}

		private void Subscribe()
		{
			_heroModel.DestroyRequest += _ => Dead?.Invoke();
		}
	}
}