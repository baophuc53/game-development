using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterInputHandler : MonoBehaviour
{
	public Character Character;

	[Header("Controls")]
    public string XAxis = "Horizontal";
    public string YAxis = "Vertical";
    public string JumpButton = "Jump";
    public string ShootButton = "Fire1";

	private Vector2 input = default(Vector2);
    private Vector3 velocity = default(Vector3);
    private bool wasGrounded = false;

	// Update is called once per frame
	private void Update()
	{
		GetInput();
		HandleMove();
		HandleShoot();
	}

	private void GetInput()
    {
		input.x = Input.GetAxis(XAxis);
		input.y = Input.GetAxis(YAxis);
	}

	private void HandleShoot()
    {
		bool inputShootStart = Input.GetButtonDown(ShootButton);
		bool inputShootEnd = Input.GetButtonUp(ShootButton);

		if (inputShootStart)
        {
			Character.OnShootBegin();
        }

		if (inputShootEnd)
        {
			Character.OnShootFinish();
        }
    }

	private void HandleMove()
    {
		float dt = Time.deltaTime;
		bool isGrounded = Character.Controller.isGrounded;
		bool doJump = false;
		bool inputJumpStart = Input.GetButtonDown(JumpButton);
		bool doCrouch = (isGrounded && input.y < -0.5f);

		if (!doCrouch)
		{
			if (isGrounded)
			{
				if (inputJumpStart)
				{
					doJump = true;
				}
			}
		}

		if (doJump)
		{
			velocity.y = Character.JumpSpeed;
		}

		velocity.x = 0;
		if (!doCrouch)
		{
			if (input.x != 0)
			{
				velocity.x = Character.MoveSpeed;
				velocity.x *= Mathf.Sign(input.x);
			}
		}


		Vector3 gravityDeltaVelocity = Physics.gravity * Character.gravityScale * dt;
		if (!isGrounded)
		{
			if (wasGrounded)
			{
				if (velocity.y < 0)
					velocity.y = 0;
			}
			else
			{
				velocity += gravityDeltaVelocity;
			}
		}

		Character.Controller.Move(velocity * dt);

		wasGrounded = isGrounded;

		// Determine and store character state

		if (isGrounded)
		{
			if (doCrouch)
			{
				Character.SetState(CharacterState.CROUCH);
			}
			else
			{
				if (input.x == 0)
				{
					Character.SetState(CharacterState.IDLE);
				}
				else
				{
					Character.SetState(CharacterState.MOVING);
				}
			}
		}
		else
		{
			Character.SetState(CharacterState.JUMP);
		}
	}
}
