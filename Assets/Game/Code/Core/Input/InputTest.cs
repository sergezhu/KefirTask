namespace Game.Code.Core.Input
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
			_mouseAndKeyboardControl.MoveStart += OnMoveStart;
			_mouseAndKeyboardControl.MoveEnd += OnMoveEnd;
			_mouseAndKeyboardControl.RotateCWStart += OnRotateCWStart;
			_mouseAndKeyboardControl.RotateCWEnd += OnRotateCWEnd;
			_mouseAndKeyboardControl.RotateCCWStart += OnRotateCCWStart;
			_mouseAndKeyboardControl.RotateCCWEnd += OnRotateCCWEnd;
			_mouseAndKeyboardControl.Fire1 += OnFire1;
			_mouseAndKeyboardControl.Fire2 += OnFire2;
		}

		private void OnMoveStart() => Debug.Log( "Move Start" );
		private void OnMoveEnd() => Debug.Log( "Move End" );
		private void OnRotateCWStart() => Debug.Log( "RotateCW Start" );
		private void OnRotateCWEnd() => Debug.Log( "RotateCW End" );
		private void OnRotateCCWStart() => Debug.Log( "RotateCCW Start" );
		private void OnRotateCCWEnd() => Debug.Log( "RotateCCW End" );
		private void OnFire1() => Debug.Log( "Fire1" );
		private void OnFire2() => Debug.Log( "Fire2" );
	}
}