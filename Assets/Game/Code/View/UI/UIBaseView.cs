namespace Game.Code.View.UI
{
	using UnityEngine;

	public abstract class UIBaseView : MonoBehaviour
	{
		public void Show() => gameObject.SetActive( true );
		public void Hide() => gameObject.SetActive( false );
	}
}