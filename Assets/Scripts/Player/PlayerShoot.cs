using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{
    private InputSystemKeyboard kb;
    public static Action shootInput;
    public static Action reloadInput;
    public static Action recoilUP;
    public static Action recoilDown;


    private void Start()
    {
        kb = gameObject.GetComponent<InputSystemKeyboard>();
    }

    private void Update()
    {
        if(kb.retrurnShoot())
        {
            //recoilUP?.Invoke();
            shootInput?.Invoke();
        }
        else if(kb.returnShootup())
        {
           // recoilDown?.Invoke();
            Debug.Log("prag");
        }

        if(kb.returnRecharge())
        {
            reloadInput?.Invoke();
        }
    }
    
    

}
