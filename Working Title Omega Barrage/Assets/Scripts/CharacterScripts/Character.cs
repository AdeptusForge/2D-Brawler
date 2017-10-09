using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MovingObject {

    public Animator mAnimator;
    public AudioSource mAudioSource;
    public Factions myFactionManager;
    public Factions.AllGameFactions myFaction;
    public List<Factions.AllGameFactions> myEnemies;
    public List<Factions.AllGameFactions> myAllies;
    protected SpriteRenderer mySprite;
    //protected Rigidbody2D myBody;
    //public List<StatusEffect> activeStatusEffects = new List<StatusEffect>();
    public string affectedByAttack;
    public float hitStunDuration;
    public bool hitStunned;
    public float knockBackDuration;
    public float mFacing;
    public bool grounded;
    public bool platformed;


    // Use this for initialization
    protected override void Start () {
        base.Start();
        GravityReset();
    }
	
	// Update is called once per frame
	protected override void FixedUpdate () {
        base.FixedUpdate();
	}


    public void Pivot(float facing)
    {
        facing = mFacing * -1f;
        mFacing = -mFacing;
        transform.localScale = new Vector3(facing, mTransform.localScale.y, mTransform.localScale.z);
        Debug.Log(mFacing);
    }
}