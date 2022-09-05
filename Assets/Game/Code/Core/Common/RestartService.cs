namespace Game.Code.Core.Common
{
	using UnityEngine.SceneManagement;

	public class RestartService
	{
		public void Restart()
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}
	}
}