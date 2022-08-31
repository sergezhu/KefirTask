namespace Game.Code.Unity.Input
{
	using System;
	using UnityEngine;
	using UnityEngine.InputSystem;

	public class MouseAndKeyboardControl
	{
		public event Action Move;
		public event Action RotateCW;
		public event Action RotateCCW;
		public event Action Fire1;
		public event Action Fire2;
		
		private readonly IInputManager _inputManager;

		private bool _isTouching;
		private bool _waitTouch;

		private Controls.KeyboardActions KeyboardActions	=> _inputManager.Keyboard;
		private Controls.MouseActions MouseActions	=> _inputManager.Mouse;

		public MouseAndKeyboardControl( IInputManager inputManager)
		{
			Debug.Log( $"KeyboardControl Init" );

			_inputManager = inputManager;

			Subscribe(
				KeyboardActions.Move, () => { Move?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCW, () => { RotateCW?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCCW, () => { RotateCCW?.Invoke(); }
			);

			Subscribe(
				MouseActions.Fire1, () => { Fire1?.Invoke(); }
			);

			Subscribe(
				MouseActions.Fire2, () => { Fire2?.Invoke(); }
			);
		}

		void Subscribe( InputAction inputAction, Action action ) 
			=> 
				inputAction.performed += _ => { action.Invoke(); };
	}
}