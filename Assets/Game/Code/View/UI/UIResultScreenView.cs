namespace Game.Code.View.UI
{
	using System;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public class UIResultScreenView : MonoBehaviour
	{
		public event Action RestartClick;
		
		[SerializeField] private TMP_Text _scoresText;
		[SerializeField] private Button _restartButton;

		public void SetScoresText( int scores ) => _scoresText.text = $"{scores}";
		public void Show() => gameObject.SetActive( true );
		public void Hide() => gameObject.SetActive( false );

		
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