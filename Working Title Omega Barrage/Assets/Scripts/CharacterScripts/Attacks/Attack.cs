using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    [System.Serializable]
    public class AttackData
    {
        /*--On-Hit Variables--*/
        public float comboPower;
        public float damage;
        private float currentDamage;
        public int armorDamage;
        public int shieldDamage;
        public bool damAfterResistRemove;

        public string attackID;
        private string enemyHitName;
        public bool attackIDReset = false;

        /*--knockback variables--*/
        public float knockBackDirection;
        public float knockBackPower;
        public float knockBackDuration;
        public float attackHitStunDuration;
        private int myFacing;
        private Vector3 knockBackVector;
    }


    public List<AttackData> possibleEffects;




	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
