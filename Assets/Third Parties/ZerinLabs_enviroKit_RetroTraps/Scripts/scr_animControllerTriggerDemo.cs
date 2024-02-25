/*
 DESCRIPTION:
 ---------------------------------------------------------------------------------------------
 Mini-script used to swap a animation (bool) parameter from one state to another (true/false)
 periodically.
 
 - param_period: determines the time between each state swap
 - param_offset: set ups an offset (delay) before triggering the first swap

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_animControllerTriggerDemo : MonoBehaviour
{
    Animator anim;

    public string param_name = "enabled";   //name of the animator bool parameter to swap
    public bool param_ini_value = true;     //value of the bool parameter by default
    public float param_period = 0f;         //time period between each bool parameter swap
    public float param_offset = 0f;         //time offset

    private float periodCounter = 0f;       //counter variable to keep track of time

    // ---------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();

        if (param_period == 0f) //if the period is not defined we set a random (small) number
        {
            param_period = Random.Range(1, 3);
        }

        updateAnimator(param_offset);
    }

    // ---------------------------------------------------------------------------------------------------
    void Update()
    {
        if (Time.time >= periodCounter)
        {
            param_ini_value = swapBool(param_ini_value);

            updateAnimator(0f);
        }
    }

    // ---------------------------------------------------------------------------------------------------
    //triggers the new param state and update de time counter
    void updateAnimator(float timeOffset)
    {
        anim.SetBool(param_name, param_ini_value);

        periodCounter = Time.time + param_period + timeOffset;
    }

    // ---------------------------------------------------------------------------------------------------
    //triggers the new param state and update de time counter
    bool swapBool(bool boolVal)
    {
        if (boolVal == true)
        {
            boolVal = false;
        }
        else
        {
            boolVal = true;
        }

        return boolVal;
    }
}
