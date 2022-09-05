namespace Game.Code.Unity.UI
{
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public class LaserChargeBlockView : IUIBaseView
	{
		[SerializeField] private Image _progressImage;
		[SerializeField] private TMP_Text _infoText;
		
		private bool _isReady;
		private float _recallProgress;

		public void SetReadyState( bool value )
		{
			_isReady = value;
			UpdateView();
		}

		public void SetRecallProgress( float value )
		{
			_recallProgress = 1f - value;
			UpdateView();
		}

		private void UpdateView()
		{
			_progressImage.fillAmount = _recallProgress;

			_infoText.text = _isReady
				? "READY"
				: $"{Mathf.Floor( _recallProgress * 100f )}%";
		}
	}
}