using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		public bool leftAttack;
		public bool rightAttack;

		public bool spell1;
		public bool spell2;

		public bool movementLocked { get; set; } = false;
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());	
			
		}

		public void OnLeftAttack(InputValue value)
		{
			LeftAttackInput(value.isPressed);
		}

		public void OnSpell1(InputValue value)
		{
			//Debug.Log("spell1 presed");
			Spell1Input(value.isPressed);
		}

		public void OnSpell2(InputValue value)
		{
			//Debug.Log("spell2 pressed");
			Spell2Input(value.isPressed);
		}

		public void OnRightAttack(InputValue value)
		{
			RightAttackInput(value.isPressed);
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void LeftAttackInput(bool newLeftAttackState)
		{
			//Debug.Log(newLeftAttackState);
			leftAttack = newLeftAttackState;
		}

		public void Spell1Input(bool newSpell1State)
		{
			spell1 = newSpell1State;
		}
		public void Spell2Input(bool newSpell2State)
		{
			spell2 = newSpell2State;
		}

		public void RightAttackInput(bool newRightAttackState)
		{
			rightAttack = newRightAttackState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}