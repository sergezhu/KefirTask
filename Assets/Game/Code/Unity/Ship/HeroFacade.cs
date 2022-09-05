namespace Game.Code.Unity.Ship
{
	using System;
	using System.Collections.Generic;
	using Game.Code.Unity.RX;
	using Game.Code.Unity.Weapons;
	using UnityEngine;

	public class HeroFacade
	{
		public event Action Dead;
		
		private readonly ShipModel _heroModel;

		public ReactiveProperty<Vector3> Position => _heroModel.Position;
		public ReactiveProperty<float> CurrentSpeed => _heroModel.CurrentSpeed;
		public ReactiveProperty<float> CurrentDirectionAngle => _heroModel.CurrentDirectionAngle;
		public IEnumerable<LaserCharge> LaserCharges => _heroModel.LaserCharges;
		

		public HeroFacade( ShipModel heroModel )
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