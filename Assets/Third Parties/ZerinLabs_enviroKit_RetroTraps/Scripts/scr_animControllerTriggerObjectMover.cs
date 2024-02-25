/*
 DESCRIPTION:
 ---------------------------------------------------------------------------------------------
 Mini-script used to move one game object from one point to another (according to 2 object transforms)
 and swap a animation (bool) parameter once reaches it's destination (true/false)
 Quite usefull for the "spike roll" assets for instance
 
 - speed: defines the speed to the object in order to move from START position to END position
 - param_name: name of a parameter that would be set-up at the initial state of the object
 - param_ini_value: initial state of the parameter
 - paramSwap_name: name of the "swap-able" parameter that will change each time the gameobject reaches its destination
 - paramSwap_ini_value : initial "swapable parameter" value

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_animControllerTriggerObjectMover : MonoBehaviour
{
    Animator anim;

    public Transform objStart;
    public Transform objEnd;

    public float speed = 0.1f;

    public string param_name = "enabled";   //name of the animator bool parameter to swap
    public bool param_ini_value = true;     //value of the bool parameter by default

    public string paramSwap_name = "backwards";   //name of the animator bool parameter to swap
    public bool paramSwap_ini_value = false;     //value of the bool parameter by default

    private float lerpPos = 0f;
    private Vector3 lerpIni;
    private Vector3 lerpEnd;
    private float lerpDir = 1;

    // ---------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();

        lerpIni = objStart.transform.position;
        lerpEnd = objEnd.transform.position;

        updateAnimator(param_name, param_ini_value);
        updateAnimator(paramSwap_name, paramSwap_ini_value);
    }

    // ---------------------------------------------------------------------------------------------------
    void Update()
    {
        lerpPos += speed*lerpDir*Time.deltaTime;

        if (lerpPos >= 1f)
        {
            lerpPos = 1f;
            lerpDir = lerpDir * -1f;

            paramSwap_ini_value = swapBool(paramSwap_ini_value);
            updateAnimator(paramSwap_name, paramSwap_ini_value);
        }

        if (lerpPos <= 0f)
        {
            lerpPos = 0;
            lerpDir = lerpDir * -1f;

            paramSwap_ini_value = swapBool(paramSwap_ini_value);
            updateAnimator(paramSwap_name, paramSwap_ini_value);
        }

        this.transform.position = Vector3.Lerp(lerpIni, lerpEnd, lerpPos);
    }

    // ---------------------------------------------------------------------------------------------------
    //triggers the new param state and update de time counter
    void updateAnimator(string param_name, bool param_value)
    {
        anim.SetBool(param_name, param_value);
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
