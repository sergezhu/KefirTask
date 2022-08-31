namespace Game.Configs
{
	using UnityEngine;


	[CreateAssetMenu( fileName = "Root", menuName = "Configs/Root", order = (int)EConfig.Root )]
	public class RootConfig : ScriptableObject
	{
		public ShipConfig Ship;
	}
}

