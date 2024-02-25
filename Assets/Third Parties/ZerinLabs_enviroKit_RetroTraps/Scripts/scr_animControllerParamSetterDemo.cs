/*
 DESCRIPTION:
 ---------------------------------------------------------------------------------------------
 Mini-script used to setup up to 3 different (boolean) animation parameters from a gameobject
 If the any of the parameter names are empty ("") that parameter would not ne used
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_animControllerParamSetterDemo : MonoBehaviour
{
    Animator anim;

    public string param_A_name = "enabled";
    public bool param_A_ini_value = true;

    public string param_B_name = "";
    public bool param_B_ini_value = true;

    public string param_C_name = "";
    public bool param_C_ini_value = true;

    // ---------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();

        if (param_A_name != "")
        {
            anim.SetBool(param_A_name, param_A_ini_value);
        }

        if (param_B_name != "")
        {
            anim.SetBool(param_B_name, param_B_ini_value);
        }

        if (param_C_name != "")
        {
            anim.SetBool(param_C_name, param_C_ini_value);
        }

    }

    // ---------------------------------------------------------------------------------------------------
    void Update()
    {

    }
}
