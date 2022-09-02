﻿namespace Game.Configs
{
	using Game.Code.Unity.Asteroids;
	using Game.Code.Unity.Enemies;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.Ship;
	using UnityEngine;

	[CreateAssetMenu( fileName = "ViewPrefabs", menuName = "Configs/ViewPrefabs", order = (int) EConfig.ViewPrefabs )]
	public class ViewPrefabsConfig : ScriptableObject
	{
		public ShipView HeroShipPrefab;
		public EnemyView EnemyPrefab;
		public AsteroidView AsteroidPrefab;
		public AsteroidPartView AsteroidPartPrefab;
	}
}