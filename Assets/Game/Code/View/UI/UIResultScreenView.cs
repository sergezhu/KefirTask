namespace Game.Code.View.UI
{
	using System;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public class UIResultScreenView : UIBaseView
	{
		public event Action RestartClick;
		
		[SerializeField] private TMP_Text _scoresText;
		[SerializeField] private Button _restartButton;

		public void SetScoresText( int scores ) => _scoresText.text = $"{scores}";

		
		private void OnEnable()
		{
			_restartButton.onClick.AddListener( OnRestartClick );
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener( OnRestartClick );
		}

		private void OnRestartClick() => RestartClick?.Invoke();
	}
}