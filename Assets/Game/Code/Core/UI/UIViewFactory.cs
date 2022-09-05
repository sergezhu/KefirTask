namespace Game.Code.Core.UI
{
	using System;
	using Game.Code.Common.Enums;
	using Game.Code.Core.Configs;
	using Game.Code.View.UI;
	using Object = UnityEngine.Object;

	public class UIViewFactory
	{
		private readonly ViewPrefabsConfig _prefabsConfig;

		public UIViewFactory( ViewPrefabsConfig prefabsConfig )
		{
			_prefabsConfig = prefabsConfig;
		}
		
		public UIBaseView Create( EUIEntityType type )
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