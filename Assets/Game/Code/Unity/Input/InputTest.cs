namespace Game.Code.Unity.Input
{
	using UnityEngine;

	public class InputTest : MonoBehaviour
	{
		private MouseAndKeyboardControl _mouseAndKeyboardControl;

		public void Init( MouseAndKeyboardControl mouseAndKeyboardControl )
		{
			_mouseAndKeyboardControl = mouseAndKeyboardControl;

			Subscribe();
		}

		private void Subscribe()
		{
			_mouseAndKeyboardControl.Move += OnMove;
			_mouseAndKeyboardControl.RotateCW += OnRotateCW;
			_mouseAndKeyboardControl.RotateCCW += OnRotateCCW;
		}

		private void OnMove() => Debug.Log( "Move" );
		private void OnRotateCW() => Debug.Log( "RotateCW" );
		private void OnRotateCCW() => Debug.Log( "RotateCCW" );
	}
}