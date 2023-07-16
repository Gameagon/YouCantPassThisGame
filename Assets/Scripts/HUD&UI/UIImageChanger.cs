using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIImageChanger : MonoBehaviour
{
    public UnityEngine.UI.Image imageComponent;

    public bool setNativeSize;

    public void ChangeSprite(Sprite image)
    {
        imageComponent.sprite = image;
        if (setNativeSize) imageComponent.SetNativeSize();
    }
}
