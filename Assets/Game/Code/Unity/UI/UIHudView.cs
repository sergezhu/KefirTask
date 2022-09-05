namespace Game.Code.Unity.UI
{
	using System.Collections.Generic;
	using TMPro;
	using UnityEngine;

	public class UIHudView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _coordXText;
		[SerializeField] private TMP_Text _coordYText;
		[SerializeField] private TMP_Text _speedText;
		[SerializeField] private TMP_Text _angleText;

		[Space]
		[SerializeField] private Transform _laserChargeBlocksParent;

		private List<LaserChargeBlockView> _laserChargeBlocks;

		public void AttachLaserChargeBlock( LaserChargeBlockView laserChargeView, int siblingIndex )
		{
			laserChargeView.transform.parent = _laserChargeBlocksParent;
			laserChargeView.transform.SetSiblingIndex( siblingIndex );
		}
	}
}