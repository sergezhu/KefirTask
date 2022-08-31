using Game.Code.Unity.Input;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
	private InputTest _inputTest;
	
	private IInputManager _inputManager;
	private MouseAndKeyboardControl _mouseAndKeyboardControl;

	private void Awake()
	{
		_inputManager            = new InputManager();
		_mouseAndKeyboardControl = new MouseAndKeyboardControl( _inputManager );

		_inputTest = FindObjectOfType<InputTest>();
		_inputTest.Init( _mouseAndKeyboardControl );
	}
}
