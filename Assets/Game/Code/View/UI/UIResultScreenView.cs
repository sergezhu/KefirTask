namespace Game.Code.View.UI
{
	using TMPro;
	using UnityEngine;

	public class UIResultScreenView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoresText;

		public void SetScoresText( int scores ) => _scoresText.text = $"{scores}";
	}
}