using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


    /*--State Machine for the player contains every possible state--*/
    public enum PlayerState
    {
        Standing,
        Walking,
        RunStart,
        Running,
        GroundJumping,
        InAir,
        AirJumping,
        Landing,
        Dashing,
        Attacking,
        AerialAttacking,
        Comboing,
        AerialComboing,
        LandingLag,
        GroundedHitstun,
        AerialHitstun,
        ImpactedWall,
        ImpactedGround,
        ReboundingWall,
        ReboundingGround,
        Prone,
        Pivoting,
        AerialPivoting,
        FallingThroughPlatform
    }
    public PlayerState mCurrentState = PlayerState.Standing;

    /*--MAKE SURE TO MAKE A WEAPON SCRIPT--*/
    //public Weapon myWeapon;

    public Vector2 groundDrag;
    public Vector2 airDrag;
    public float techTimer;
    public float maxTechTimer;
    public bool canPivot;
    public KeyInput myInputs;

    /*--SPEED VARIABLES--*/
    public Vector2 runForce;
    public float runSpeed;
    public Vector2 airStrafeForce;
    public float airStrafeSpeed;

    public Vector2 fallSpeed;
    public Vector2 fastFallSpeed;
    public Vector2 currentMaxFallSpeed;
    public Vector2 maxRiseSpeed;
    private bool isFastFalling;

    /*--JUMP VARIABLES--*/
    public float minJumpTime;
    public float maxJumpTime;
    public float jumpTimer = 0;
    public Vector2 shortJumpSpeed;
    public Vector2 shortJumpAccel;
    public Vector2 fullJumpSpeed;
    public Vector2 fullJumpAccel;
    public float jumpCheckTime;
    public bool jumpCheck = false;
    public float jumpNumber;
    public bool isJumping;


    public enum Direction
    {
        Left,
        Right
    }

    /*--Update Current Player State--*/
    public void PlayerStateUpdate()
    {
        //Debug.Log(jumpTimer);

        /*--Make sure all speeds are positive--*/
        if ((grounded || platformed) && !hitStunned)
        {
            mSpeed.x = Mathf.Min(mSpeed.x, runSpeed);
            mSpeed.x = Mathf.Max(mSpeed.x, -runSpeed);
        }
        if((!grounded && !platformed) && !hitStunned)
        {
            mSpeed.x = Mathf.Min(mSpeed.x, airStrafeSpeed);
            mSpeed.x = Mathf.Max(mSpeed.x, -airStrafeSpeed);
            /*--Make sure currentMaxFallSpeed is always negative-*/
            mSpeed.y = Mathf.Max(mSpeed.x, currentMaxFallSpeed.y);
            mSpeed.y = Mathf.Min(mSpeed.x, maxRiseSpeed.y);
        }
        //Debug.Log("PlayerStateUpdateActive");
        switch (mCurrentState)
        {



            case PlayerState.Standing:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT RUN CHECK HERE--*/
                /*--PUT JUMP CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                /*--PUT ATTACK CHECK HERE--*/
                /*--PUT COMBO CHECK HERE--*/
                /*--PUT FALLINGTHROUGH CHECK HERE--*/
                /*--PUT PIVOT CHECK HERE--*/

                if (!grounded && !platformed)
                {
                    mCurrentState = PlayerState.InAir;
                    break;
                }
                else if (grounded || platformed)
                {
                    if (mSpeed != Vector2.zero)
                    {
                        if (mFacing == 1)
                        {
                            //Move(MoveType.AccelerateTo, Vector2.zero, 0, -groundDrag);
                        }
                        if (mFacing == -1)
                        {
                            //Move(MoveType.AccelerateTo, Vector2.zero, 0, groundDrag);
                        }
                    }



                    if (myInputs.Pressed(myInputs.jump))
                    {
                        mCurrentState = PlayerState.GroundJumping;
                        break;
                    }

                    if (myInputs.KeyState(myInputs.right))
                    {
                        if (mFacing == 1)
                        {
                            if (myInputs.Pressed(myInputs.dash) || myInputs.KeyState(myInputs.dash))
                            {
                                //DashStart("DashForward");
                            }
                            else
                            {
                                //Debug.Log("RunStart");
                                mCurrentState = PlayerState.RunStart;
                                break;
                            }
                        }
                        if (mFacing != 1)
                        {
                            Debug.Log("Traditional Pivot");
                            mCurrentState = PlayerState.Pivoting;
                        }
                    }
                    if (myInputs.KeyState(myInputs.left))
                    {
                        if (mFacing == -1)
                        {
                            if (myInputs.Pressed(myInputs.dash) || myInputs.KeyState(myInputs.dash))
                            {
                                //DashStart("DashForward");
                            }
                            else
                            {
                                //Debug.Log("RunStart");
                                mCurrentState = PlayerState.RunStart;
                                break;
                            }

                        }
                        if (mFacing != -1)
                        {
                            Debug.Log("Traditional Pivot");
                            mCurrentState = PlayerState.Pivoting;
                        }
                    }
                }
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                break;




            case PlayerState.RunStart:
                if (!grounded && !platformed)
                {
                    mCurrentState = PlayerState.InAir;
                    break;
                }

                if (myInputs.Pressed(myInputs.jump))
                {
                    mCurrentState = PlayerState.GroundJumping;
                    break;
                }
                if (mFacing == 1)
                {
                    if (myInputs.Released(myInputs.right))
                    {
                        mCurrentState = PlayerState.Standing;
                        break;
                    }
                    if (myInputs.KeyState(myInputs.right))
                    {
                        if (mSpeed == runForce)
                        {
                            mCurrentState = PlayerState.Running;
                            break;
                        }
                        if (mSpeed != runForce)
                        {
                            Move(MoveType.AddForce, runForce, mFacing);
                        }
                        mCurrentState = PlayerState.RunStart;
                        if (myInputs.Pressed(myInputs.dash))
                        {
                            //DashStart("DashForward");
                        }
                    }
                }
                else if(mFacing == -1)
                {
                    if (myInputs.Released(myInputs.left))
                    {
                        mCurrentState = PlayerState.Standing;
                        break;
                    }
                    if (myInputs.KeyState(myInputs.left))
                    {
                        if (mSpeed == -runForce)
                        {
                            mCurrentState = PlayerState.Running;
                            break;
                        }
                        if (mSpeed != -runForce)
                        {
                            Move(MoveType.AddForce, -runForce, mFacing);
                        }
                        mCurrentState = PlayerState.RunStart;
                        if (myInputs.Pressed(myInputs.dash))
                        {
                            //DashStart("DashForward");
                        }
                    }
                }
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                /*--PUT RUN CHECK HERE--*/
                /*--PUT JUMP CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                /*--PUT COMBO CHECK HERE--*/
                /*--PUT FALLINGTHROUGH CHECK HERE--*/
                break;




            case PlayerState.Running:

                if (myInputs.Pressed(myInputs.jump))
                {
                    mCurrentState = PlayerState.GroundJumping;
                    break;
                }
                if (mFacing == 1)
                {
                    if (myInputs.Released(myInputs.right))
                    {
                        mCurrentState = PlayerState.Standing;
                        break;
                    }
                    if (myInputs.Pressed(myInputs.left))
                    {
                        mCurrentState = PlayerState.Pivoting;
                        break;
                    }
                }
                else if (mFacing == -1)
                {
                    if (myInputs.Released(myInputs.left))
                    {
                        mCurrentState = PlayerState.Standing;
                        break;
                    }
                    if (myInputs.Pressed(myInputs.right))
                    {
                        mCurrentState = PlayerState.Pivoting;
                        break;
                    }
                }

                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                /*--PUT JUMP CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                /*--PUT COMBO CHECK HERE--*/
                /*--PUT FALLINGTHROUGH CHECK HERE--*/
                break;




            case PlayerState.GroundJumping:
                isJumping = true;
                jumpTimer += Time.deltaTime;
                Debug.Log(jumpTimer);

                if (myInputs.Released(myInputs.jump))
                {
                    if(jumpTimer >= minJumpTime && jumpTimer < maxJumpTime)
                    {
                        Debug.Log("FullHop");
                        jumpTimer = 0f;
                        ShortJump();
                        mCurrentState = PlayerState.InAir;
                        break;
                    }
                    else if (jumpTimer < minJumpTime)
                    {
                        Debug.Log("ShortHop");
                        jumpTimer = 0f;
                        FullJump();
                        mCurrentState = PlayerState.InAir;
                        break;
                    }
                }

                if (jumpTimer >= maxJumpTime)
                {
                    Debug.Log("Max Jump Time Exceeded");
                    jumpTimer = 0f;
                    FullJump();
                    mCurrentState = PlayerState.InAir;
                    break;
                }
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                break;




            case PlayerState.InAir:
                if (isJumping)
                {
                    if(mSpeed.y <= 0)
                    {
                        Debug.Log("No Longer Jumping");
                        isJumping = false;
                    }
                }
                if(!isJumping && jumpNumber >= 1)
                {
                    if (myInputs.Pressed(myInputs.jump))
                    {
                        jumpNumber -= 1;
                        mCurrentState = PlayerState.AirJumping;
                        break;
                    }
                }
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                /*--PUT JUMP CHECK HERE--*/
                /*--PUT ATTACK CHECK HERE--*/
                /*--PUT AERIALCOMBO CHECK HERE--*/
                break;



            case PlayerState.AirJumping:
                isJumping = true;
                Debug.Log(jumpTimer);
                FullJump();
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                /*--PUT DASH CHECK HERE--*/
                break;




            case PlayerState.Landing:

                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                break;




            case PlayerState.Dashing:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                break;




            case PlayerState.Attacking:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                break;




            case PlayerState.AerialAttacking:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                break;




            case PlayerState.Comboing:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                break;



            case PlayerState.AerialComboing:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                break;


            case PlayerState.LandingLag:
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                break;




            case PlayerState.GroundedHitstun:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                break;




            case PlayerState.AerialHitstun:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                break;




            case PlayerState.ImpactedWall:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                /*--PUT REBOUNDING CHECK HERE--*/
                break;



            case PlayerState.ImpactedGround:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                /*--PUT REBOUNDING CHECK HERE--*/
                /*--PUT PRONE CHECK HERE--*/
                break;




            case PlayerState.ReboundingWall:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT REBOUND FUNCTION HERE--*/
                break;





            case PlayerState.ReboundingGround:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT REBOUND FUNCTION HERE--*/
                break;




            case PlayerState.Prone:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT GETUP FUNCTION HERE--*/
                break;




            case PlayerState.Pivoting:
                Debug.Log("Is pivoting");
                Pivot(mFacing);
                if (mFacing == 1)
                {
                    Debug.Log(mFacing + "Decelerate");
                    //Move(MoveType.AccelerateTo, Vector2.zero, 0, -groundDrag);
                }
                if (mFacing == -1)
                {
                    Debug.Log(mFacing + "Decelerate");
                    //Move(MoveType.AccelerateTo, Vector2.zero, 0, groundDrag);
                }
                if (myInputs.KeyState(myInputs.left))
                {
                    if (mFacing == -1)
                    {
                        if (myInputs.Pressed(myInputs.dash) || myInputs.KeyState(myInputs.dash))
                        {
                            //DashStart("DashForward");
                        }
                        else
                        {
                            Debug.Log("RunStart");
                            mCurrentState = PlayerState.RunStart;
                            break;
                        }

                    }
                }
                if (myInputs.KeyState(myInputs.right))
                {
                    if (mFacing == 1)
                    {
                        if (myInputs.Pressed(myInputs.dash) || myInputs.KeyState(myInputs.dash))
                        {
                            //DashStart("DashForward");
                        }
                        else
                        {
                            Debug.Log("RunStart");
                            mCurrentState = PlayerState.RunStart;
                            break;
                        }

                    }
                }
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT STANDING CHECK HERE--*/
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                Debug.Log("Go back to standing");
                mCurrentState = PlayerState.Standing;
                break;




            case PlayerState.AerialPivoting:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT HITSTUN CHECK HERE--*/
                /*--PUT IN-AIR CHECK HERE--*/
                /*--PUT LANDING CHECK HERE--*/
                break;




            case PlayerState.FallingThroughPlatform:
                //mAnimator.Play(/*--PUT ANIMATION STATE HERE--*/"");
                /*--PUT IN-AIR CHECK HERE--*/
                /*--PUT FALLTHROUGH FUNCTION HERE--*/
                break;
        }
    }



    protected override void Gravity()
    {
        if (affectedByGravity)
        {
            //Debug.Log("Gravy bish");
            //Move(MoveType.AccelerateTo, currentMaxFallSpeed, 1f, gravityAccel);
        }
    }







    private void FullJump()
    {
        jumpNumber -= 1;
        //Move(MoveType.AccelerateTo, fullJumpSpeed, 1f, fullJumpAccel);
    }

    private void ShortJump()
    {
        jumpNumber -= 1;
        //Move(MoveType.AccelerateTo, shortJumpSpeed, 1f, shortJumpAccel);
        if (!grounded && !platformed && !isJumping)
        {

        }
    //jumpCheck = true;
    //jumpCheckTime = 0.3f;
    //if (jumpNumber >= 1f)
    //{
    //    if ((grounded || platformed) && !isJumping && !Input.GetKey(myInputs.dash))
    //    {
    //        Debug.Log("Jump Check");
    //        jumpNumber -= 1f;
    //        if (jumpTimer <= minJumpTime)
    //        {
    //            isJumping = true;
    //            Debug.Log("shorthop" + isJumping);
    //        }
    //        if (jumpTimer > minJumpTime)
    //        {
    //            isJumping = true;
    //            Debug.Log("fullhop" + isJumping);
    //            Move(MoveType.AccelerateTo, fullJumpSpeed, 1f, fullJumpAccel);
    //        }
    //    }
    //    if (!grounded && !platformed && !isJumping)
    //    {
    //        Debug.Log("jump analysis");
    //        jumpNumber -= 1f;
    //        StopAllMovement();
    //        if (jumpTimer <= minJumpTime)
    //        {
    //            isJumping = true;
    //            Debug.Log("shorthop");
    //            Move(MoveType.AccelerateTo, shortJumpSpeed, 1f, shortJumpAccel);
    //        }
    //        if (jumpTimer > minJumpTime)
    //        {
    //            isJumping = true;
    //            Debug.Log("fullhop");
    //            Move(MoveType.AccelerateTo, fullJumpSpeed, 1f, fullJumpAccel);
    //        }
    //    }
    //}
    //Debug.Log(jumpNumber);
    //Debug.Log("jump ended early");
}


    public void PlayerFullUpdate()
    {
        PlayerStateUpdate();
        Debug.Log(mCurrentState);
        Debug.Log(mFacing);
        UpdatePhysics();
        myInputs.UpdatePrevInputs();
        myInputs.GenerateCurrentInputs();
    }

    public void PlayerInit()
    {
        affectedByGravity = true;
        mPosition = transform.position;
        physicsBoxOffset.y = physicsBox.halfSize.y;

        myInputs.mPrevInputs5 = new List<KeyCode>();
        myInputs.mPrevInputs4 = new List<KeyCode>();
        myInputs.mPrevInputs3 = new List<KeyCode>();
        myInputs.mPrevInputs2 = new List<KeyCode>();
        myInputs.mPrevInputs1 = new List<KeyCode>();
        myInputs.mInputs = new List<KeyCode>();
        myInputs.menu = KeyCode.Escape;
        myInputs.possibleInputs.Add(myInputs.menu);
        myInputs.jump = KeyCode.Space;
        myInputs.possibleInputs.Add(myInputs.jump);
        myInputs.attackLight = KeyCode.P;
        myInputs.possibleInputs.Add(myInputs.attackLight);
        myInputs.attackMedium = KeyCode.O;
        myInputs.possibleInputs.Add(myInputs.attackMedium);
        myInputs.attackHeavy = KeyCode.I;
        myInputs.possibleInputs.Add(myInputs.attackHeavy);
        myInputs.combo = KeyCode.U;
        myInputs.possibleInputs.Add(myInputs.combo);
        myInputs.dash = KeyCode.LeftShift;
        myInputs.possibleInputs.Add(myInputs.dash);
        myInputs.up = KeyCode.W;
        myInputs.possibleInputs.Add(myInputs.up);
        myInputs.down = KeyCode.S;
        myInputs.possibleInputs.Add(myInputs.down);
        myInputs.left = KeyCode.A;
        myInputs.possibleInputs.Add(myInputs.left);
        myInputs.right = KeyCode.D;
        myInputs.possibleInputs.Add(myInputs.right);
        myInputs.weaponActive1 = KeyCode.Escape;
        myInputs.possibleInputs.Add(myInputs.weaponActive1);
        myInputs.weaponActive2 = KeyCode.Escape;
        myInputs.possibleInputs.Add(myInputs.weaponActive2);
        myInputs.consumable = KeyCode.Escape;
        myInputs.possibleInputs.Add(myInputs.consumable);
        myInputs.activate = KeyCode.E;
        myInputs.possibleInputs.Add(myInputs.activate);







    }

    // Use this for initialization
    protected override void Start () {
        PlayerInit();
        base.Start();
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () {
        base.FixedUpdate();
        PlayerFullUpdate();

    }
}
