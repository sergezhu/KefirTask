using Game.Code.Unity.Input;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
	private IInputManager _inputManager;
	private MouseAndKeyboardControl _mouseAndKeyboardControl;

	private void Start()
	{
		_inputManager            = new InputManager();
		_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );
	}
}
