using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputSystemKeyboard : MonoBehaviour
{

    public Vector2 movment { get; private set; }
    public float pitch { get; private set; }
    public float yaw { get; private set; }
    public bool jump { get; private set; }
    public bool run { get; private set; }
    public bool stealthdown { get; private set; }

    public bool stealthup { get; private set; }
    public bool stealth { get; private set; }
    public bool shoot { get; private set; }
    public bool stopShoot { get; private set; }
    public bool point { get; private set; }
    public bool recharge { get; private set; }
    public bool crouch { get; private set; }
    public PlayerInput pl;

    public void Start()
    {
        pl = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        movment = pl.actions["Move"].ReadValue<Vector2>();
        jump = pl.actions["Jump"].WasPressedThisFrame();
        run = pl.actions["Run"].WasPressedThisFrame(); 
        stealthdown = pl.actions["Crunch"].WasPressedThisFrame();
        stealthup = pl.actions["Crunch"].WasPressedThisFrame();
        //pitch = pl.actions["Look"].ReadValue<Vector2>();
        //yaw = pl.actions["Look"].ReadValue<Vector2>();






    }
    public bool returnRecharge()
    {
        return recharge;
    }
    public bool returnShootup()
    {
        return stopShoot;
    }
    public bool retrurnShoot()
    {
        return shoot;
    }
    public float returnyaw()
    {
        return yaw;
    }
    public float returnpitch()
    {
        return pitch;
    }
    public float returnver()
    {
        return movment.x;
    }
    public float returnhor()
    {
        return movment.y;
    }
    public bool returnJump()
    {
        return jump;
    }
    public bool returnrun()
    {
        return run;
    }
    public bool returnstealthdown()
    {
        return stealthdown;
    }
    public bool returnstealthup()
    {
        return stealthup;
    }
    public bool returnstealth()
    {
        return stealth;
    }

}
