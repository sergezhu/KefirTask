namespace Game.Code.Unity.Input
{
	public interface IInputManager
	{
		Controls.KeyboardActions Keyboard { get; }
		Controls.MouseActions Mouse { get; }
	}


	public class InputManager : IInputManager
	{
		private readonly Controls _controls;

		public Controls.KeyboardActions Keyboard { get; }
		public Controls.MouseActions Mouse { get; }
		

		public InputManager()
		{
			_controls = new Controls();
			
			Keyboard  = _controls.Keyboard;
			Keyboard.Enable();

			Mouse = _controls.Mouse;
			Mouse.Enable();
		}
	}
}

