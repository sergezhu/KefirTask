namespace Game.Code.Unity.Common
{
	using System;
	using Game.Code.Unity.Configs;
	using Game.Code.Unity.Enums;
	using Game.Code.Unity.UI;
	using Object = UnityEngine.Object;

	public class UIViewFactory
	{
		private readonly ViewPrefabsConfig _prefabsConfig;

		public UIViewFactory( ViewPrefabsConfig prefabsConfig )
		{
			_prefabsConfig = prefabsConfig;
		}
		
		public IUIBaseView Create( EUIEntityType type )
		{
			switch ( type )
			{
				case EUIEntityType.LaserChargeBlock:
					return Object.Instantiate( _prefabsConfig.LaserChargeBlockPrefab );
				default:
					throw new ArgumentOutOfRangeException( nameof(type), type, null );
			}
		}
	}
}