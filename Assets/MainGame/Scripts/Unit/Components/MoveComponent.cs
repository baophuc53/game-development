using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	Right = 1,
	Left = -1,
}

public class MoveComponent : BaseComponent
{ 
	public CharacterController Controller;

	[Header("Controls")]
	public string XAxis = "Horizontal";
	public string YAxis = "Vertical";
	public string JumpButton = "Jump";
	public string AttackButton = "Fire1";

	[Header("Moving")]
	public float walkSpeed = 1.5f;
	public float runSpeed = 7f;
	public float gravityScale = 6.6f;

	[Header("Jumping")]
	public float jumpSpeed = 25;
	public float minimumJumpDuration = 0.5f;
	public float jumpInterruptFactor = 0.5f;
	public float forceCrouchVelocity = 25;
	public float forceCrouchDuration = 0.5f;

	public Direction Direction { get; set; }

	private Vector2 input = default(Vector2);
	private Vector3 velocity = default(Vector3);
	private float minimumJumpEndTime = 0;
	private float forceCrouchEndTime;
	private bool wasGrounded = false;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    public override void Tick()
    {
		float dt = Time.deltaTime;
		bool isGrounded = Controller.isGrounded;
		
		// Dummy input.
		input.x = Input.GetAxis(XAxis);
		input.y = Input.GetAxis(YAxis);

		bool inputJumpStop = Input.GetButtonUp(JumpButton);
		bool inputJumpStart = Input.GetButtonDown(JumpButton);

		bool doCrouch = (isGrounded && input.y < -0.5f) || (forceCrouchEndTime > Time.time);
		bool doJumpInterrupt = false;
		bool doJump = false;

		if (!doCrouch)
		{
			if (isGrounded)
			{
				if (inputJumpStart)
				{
					doJump = true;
				}
			}
			else
			{
				doJumpInterrupt = inputJumpStop && Time.time < minimumJumpEndTime;
			}
		}

		// Dummy physics and controller using UnityEngine.CharacterController.
		Vector3 gravityDeltaVelocity = Physics.gravity * gravityScale * dt;

		if (doJump)
		{
			velocity.y = jumpSpeed;
			minimumJumpEndTime = Time.time + minimumJumpDuration;
		}
		else if (doJumpInterrupt)
		{
			if (velocity.y > 0)
				velocity.y *= jumpInterruptFactor;
		}

		velocity.x = 0;
		if (!doCrouch)
		{
			if (input.x != 0)
			{
				Direction = input.x > 0 ? Direction.Right : Direction.Left;
				velocity.x = Mathf.Abs(input.x) > 0.6f ? runSpeed : walkSpeed;
				velocity.x *= Mathf.Sign(input.x);
			}
		}


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
		Controller.Move(velocity * dt);
		wasGrounded = isGrounded;

		// Determine and store character state

		if (isGrounded)
		{
			if (doCrouch)
			{
				Owner.CurrentState = UnitState.Crouch;
			}
			else
			{
				if (input.x == 0)
                {
					Owner.CurrentState = UnitState.Idle;
                }
                else
                {
					Owner.CurrentState = UnitState.Run;
					//currentState = Mathf.Abs(input.x) > 0.6f ? CharacterState.Run : CharacterState.Walk;
                }
			}

		}
		else
		{
			Owner.CurrentState = UnitState.Jump;
			//currentState = velocity.y > 0 ? CharacterState.Jump : CharacterState.Fall;
		}
	}
}
