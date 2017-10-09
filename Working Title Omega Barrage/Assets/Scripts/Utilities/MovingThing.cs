using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovingObject : MonoBehaviour
{
    public float mass;

    public bool affectedByFriction;
    public float currentDrag;
    public float aerialFriction;
    public float groundedFriction;
    public bool canMove;

    public Vector2 mOldPosition;
    public Vector2 mPosition;
    public Transform mTransform;
    public Vector2 mOldSpeed;
    public Vector2 mSpeed;
    public Vector2 mScale;

    [HideInInspector]
    public List<Force> allForces= new List<Force>();

    public AxisAlignedBoundingBox physicsBox;
    public Vector2 physicsBoxOffset;

    public bool mPushedRightWall;
    public bool mPushesRightWall;
    public bool mPushedLeftWall;
    public bool mPushesLeftWall;
    public bool mWasOnGround;
    public bool mOnGround;
    public bool mWasAtCeiling;
    public bool mAtCeiling;

    /*--Affected by Gravity--*/
    public bool affectedByGravity;
    public Vector2 gravityDirection;

    public class Force
    {
        public Vector2 direction;
        public ForceMode2D forceMode;

        public Force(Vector2 direction, ForceMode2D forceMode)
        {
            this.direction = direction;
            this.forceMode = forceMode;
        }
    }

    public void AddForce(Vector2 direction, ForceMode2D forceMode)
    {
        allForces.Add(new Force(direction, forceMode));
    }

    public void CalculateNetForces()
    {
        Vector2 netForce = Vector2.zero;
        foreach(Force force in allForces)
        {
            netForce += force.direction;
            Debug.Log(netForce);
        }
    }

    public void ClearAllForces()
    {
        allForces = new List<Force>();
    }


    protected virtual void Gravity()
    {
        if (affectedByGravity)
        {
            Move(MoveType.AddForce, gravityDirection, 1f);
        }
    }
    protected virtual void GravityReset()
    {
        gravityDirection = Vector2.down;
    }

    public void UpdatePhysics()
    {
        mOldPosition = mPosition;
        mOldSpeed = mSpeed;
        Debug.Log(mSpeed);


        mWasOnGround = mOnGround;
        mPushedRightWall = mPushesRightWall;
        mPushedLeftWall = mPushesLeftWall;
        mWasAtCeiling = mAtCeiling;

        mPosition += mSpeed * Time.deltaTime;
        Gravity();
        Drag();
        CalculateNetForces();
        ClearAllForces();
        if (mPosition.y < 0.0f)
        {
            mPosition.y = 0.0f;
            mOnGround = true;
        }
        else
            mOnGround = false;

        physicsBox.center = mPosition + physicsBoxOffset;

        mTransform.position = new Vector3(Mathf.Round(mPosition.x), Mathf.Round(mPosition.y), -1.0f);
        mTransform.localScale = new Vector3(mScale.x, mScale.y, 1.0f);
    }


    public virtual Vector2 AccelerateTo(Vector2 desiredSpeed, Vector2 accel)
    {
        Vector2 modSpeed = mOldSpeed;
        if(mSpeed != desiredSpeed)
        {
            if(accel == Vector2.zero)
            {
                Debug.Log("Cannot Accelerate with accel of 0");
            }
            else if (accel != Vector2.zero)
            {
                //Debug.Log(mOldSpeed);
                if (desiredSpeed.x == 0)
                {
                    if (accel.x > 0)
                    {
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x >= desiredSpeed.x)
                        {
                            //Debug.Log("ACCELERATING TO 0 X");
                            modSpeed.x = 0f;
                        }
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x < desiredSpeed.x)
                        {
                            modSpeed.x += accel.x;
                        }
                    }
                    if (accel.x < 0)
                    {
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x <= desiredSpeed.x)
                        {
                            modSpeed.x = 0f;
                        }
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x > desiredSpeed.x)
                        {
                            modSpeed.x += accel.x;
                        }
                    }
                }
                else if (desiredSpeed.x > 0)
                {
                    if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x >= desiredSpeed.x)
                    {
                        modSpeed.x = desiredSpeed.x;
                    }
                    else if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x < desiredSpeed.x)
                    {
                        modSpeed.x += accel.x;
                    }
                }
                else if(desiredSpeed.x < 0)
                    if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x <= desiredSpeed.x)
                    {
                        modSpeed.x = desiredSpeed.x;
                    }
                    else if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).x > desiredSpeed.x)
                    {
                        modSpeed.x += accel.x;
                    }
                if (desiredSpeed.y == 0)
                {
                    //Debug.Log("ACCELERATING TO 0 Y");
                    if (accel.y > 0)
                    {
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y >= desiredSpeed.y)
                        {
                            modSpeed.y = 0f;
                        }
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y < desiredSpeed.y)
                        {
                            modSpeed.y += accel.y;
                        }
                    }
                    if (accel.y < 0)
                    {
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y <= desiredSpeed.y)
                        {
                            modSpeed.y = 0f;
                        }
                        if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y > desiredSpeed.y)
                        {
                            modSpeed.y += accel.y;
                        }
                    }
                }
                else if (desiredSpeed.y > 0)
                {
                    if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y >= desiredSpeed.y)
                    {
                        modSpeed.y = desiredSpeed.y;
                    }
                    else if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y < desiredSpeed.y)
                    {
                        modSpeed.y += accel.y;
                    }
                }
                else if (desiredSpeed.y < 0)
                    if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y <= desiredSpeed.y)
                    {
                        modSpeed.y = desiredSpeed.y;
                    }
                    else if (new Vector2(modSpeed.x + accel.x, modSpeed.y + accel.y).y > desiredSpeed.y)
                    {
                        modSpeed.y += accel.y;
                    }
                
                //Debug.Log(modSpeed);
                return modSpeed;
            }
        }
        else if(mSpeed == desiredSpeed)
        {
            Debug.Log("Cannot Accelerate to same speed");
            return mSpeed;
        }

        Debug.Log("Fell through AccelerateTo");
        return Vector2.zero;
    }




    public enum MoveType
    {
        //SetVelocity,
        AddForce,
        TranslateDirectly,
        TransformEdit,
    }

    public virtual void Move(MoveType moveTypeEnum, Vector2 direction, float speed, ForceMode2D forceMode = ForceMode2D.Force)
    {
        switch (moveTypeEnum)
        {
            case MoveType.AddForce:
                AddForce(direction, forceMode);
                break;
            case MoveType.TranslateDirectly:
                mTransform.Translate(direction * speed);
                break;
            case MoveType.TransformEdit:
                mTransform.position = new Vector3(mTransform.position.x + direction.x, mTransform.position.y + direction.y, mTransform.position.z);
                break;
            default:
                Debug.Log("Unusable Movetype");
                break;
        }
    }

    public void StopAllMovement()
    {
        mSpeed = Vector3.zero;
    }


    protected virtual void Start()
    {
        //physicsBox.
    }
    protected virtual void FixedUpdate()
    {
        UpdatePhysics();
    }
}
