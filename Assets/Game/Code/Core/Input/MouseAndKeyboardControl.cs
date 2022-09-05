namespace Game.Code.Core.Input
{
	using System;
	using Game.Code.Unity.Input;
	using UnityEngine;
	using UnityEngine.InputSystem;

	public class MouseAndKeyboardControl
	{
		public event Action MoveStart;
		public event Action MoveEnd;
		public event Action RotateCWStart;
		public event Action RotateCWEnd;
		public event Action RotateCCWStart;
		public event Action RotateCCWEnd;
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
				KeyboardActions.MoveStart, () => { MoveStart?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCWStart, () => { RotateCWStart?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCCWStart, () => { RotateCCWStart?.Invoke(); }
			);
			
			Subscribe(
				KeyboardActions.MoveEnd, () => { MoveEnd?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCWEnd, () => { RotateCWEnd?.Invoke(); }
			);

			Subscribe(
				KeyboardActions.RotateCCWEnd, () => { RotateCCWEnd?.Invoke(); }
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