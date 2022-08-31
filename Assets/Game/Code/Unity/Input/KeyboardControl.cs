namespace Game.Code.Unity.Input
{
	using System;
	using UnityEngine;
	using UnityEngine.InputSystem;

	public class KeyboardControl
	{
		public event Action Move;
		public event Action RotateCW;
		public event Action RotateCCW;
		
		private readonly IInputManager _inputManager;

		private bool _isTouching;
		private bool _waitTouch;

		private Controls.KeyboardActions KeyboardActions	=> _inputManager.Keyboard;

		public KeyboardControl( IInputManager inputManager)
		{
			Debug.Log( $"KeyboardControl Init" );

			_inputManager = inputManager;

			Subscribe(
				_inputManager.Keyboard.Move, () => { Move?.Invoke(); }
			);

			Subscribe(
				_inputManager.Keyboard.RotateCW, () => { RotateCW?.Invoke(); }
			);

			Subscribe(
				_inputManager.Keyboard.RotateCCW, () => { RotateCCW?.Invoke(); }
			);
		}

		void Subscribe( InputAction inputAction, Action action ) 
			=> 
				inputAction.performed += _ => { action.Invoke(); };
	}
}