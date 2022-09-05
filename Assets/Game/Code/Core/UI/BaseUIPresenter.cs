namespace Game.Code.Core.UI
{
	using Game.Code.View.UI;

	public abstract class BaseUIPresenter
	{
		protected abstract UIBaseView View { get; }
		
		public void Show()
		{
			View.Show();
		}

		public void Hide()
		{
			View.Hide();
		}
	}
}