using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour{

    /*--Player Inputs--*/

    /*---NON-REBINDABLE INPUTS--*/
    public KeyCode menu;
    /*--ALL REBINDABLE INPUTS--*/
    public KeyCode jump;
    public KeyCode attackLight;
    public KeyCode attackMedium;
    public KeyCode attackHeavy;
    public KeyCode combo;
    public KeyCode dash;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode weaponActive1;
    public KeyCode weaponActive2;
    public KeyCode consumable;
    public KeyCode activate;

    public List<KeyCode> possibleInputs;
    public List<KeyCode> mInputs;
    public List<KeyCode> mPrevInputs1;
    public List<KeyCode> mPrevInputs2;
    public List<KeyCode> mPrevInputs3;
    public List<KeyCode> mPrevInputs4;
    public List<KeyCode> mPrevInputs5;

    public bool Released(KeyCode key)
    {
        //Debug.Log("Released being called for key: " + key + myInputs.Contains(key) + mPrevInputs.Contains(key));
        return (!mInputs.Contains(key) && mPrevInputs1.Contains(key));
    }
    public bool KeyState(KeyCode key)
    {
        //Debug.Log("Keystate being called for key: " + key + myInputs.Contains(key) + mPrevInputs.Contains(key));
        return (mInputs.Contains(key));
    }
    public bool Pressed(KeyCode key)
    {
        //Debug.Log("Pressed being called for key: " + key);
        return (mInputs.Contains(key) && !mPrevInputs1.Contains(key));
    }
    /*--End of Player Inputs--*/
    public void GenerateCurrentInputs()
    {
        foreach(KeyCode input in possibleInputs)
        {
            if (Input.GetKey(input))
            {
                mInputs.Add(input);
                //Debug.Log(input);
            }
        }
    }

    public void UpdatePrevInputs()
    {
        mPrevInputs5 = mPrevInputs4;
        mPrevInputs4 = mPrevInputs3;
        mPrevInputs3 = mPrevInputs2;
        mPrevInputs2 = mPrevInputs1;
        mPrevInputs1 = mInputs;
        mInputs = new List<KeyCode>();
    }


    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {
        //UpdatePrevInputs();
        //GenerateCurrentInputs();
    }
}
