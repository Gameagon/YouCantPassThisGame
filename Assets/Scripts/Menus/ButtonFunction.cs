using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonFunction : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsPauseButton;
    public GameObject MenuToGo;

    public static event Action<GameObject, bool> ChangeMenu = delegate { };

    public void SendMenu()
    {
        ChangeMenu(MenuToGo, IsPauseButton);
    }

}


