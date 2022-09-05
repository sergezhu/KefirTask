namespace Game.Code.Core.UI
{
	using System.Collections.Generic;
	using Game.Code.Core.Utils;
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

		[Space]
		[SerializeField] private TMP_Text _scoresText;

		private List<LaserChargeBlockView> _laserChargeBlocks;

		public void AttachLaserChargeBlock( LaserChargeBlockView laserChargeView, int siblingIndex )
		{
			laserChargeView.transform.SetParent( _laserChargeBlocksParent );
			laserChargeView.transform.SetSiblingIndex( siblingIndex );
		}

		public void SetPositionText( Vector3 worldPos )
		{
			var pos = worldPos.xz();
			
			_coordXText.text = $"{pos.x}";
			_coordYText.text = $"{pos.y}";
		}

		public void SetCurrentSpeedText( float speed )
		{
			_speedText.text = $"{speed:F1}";
		}

		public void SetCurrentAngleText( float angle )
		{
			var degs = Mathf.Floor( angle * Mathf.Rad2Deg );
			degs = Mathf.Repeat( degs, 360f );
			
			_angleText.text = $"{degs}";
		}

		public void SetScoresText( int scores )
		{
			_scoresText.text = $"{scores}";
		}
	}
}