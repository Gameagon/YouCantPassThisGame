using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class FirstPersonCameraController : MonoBehaviour
{
    [SerializeField] Vector2 lookAngle;
    [SerializeField] Transform head;
    [SerializeField] private float sensivilitygamepad = 30; 

    [SerializeField] float sensitivity = 2.0f;
    // Start is called before the first frame update.0
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(InputAction.CallbackContext context)
    {
        Debug.Log(context.control.device);
        if(context.control.device is Mouse)
        {
            lookAngle += context.ReadValue<Vector2>() * sensitivity;

        }
        else
        {
            lookAngle += context.ReadValue<Vector2>() * sensitivity * sensivilitygamepad;



        }
        lookAngle.y = Mathf.Clamp(lookAngle.y, -90, 90);
        lookAngle.x = lookAngle.x % 360;
    }

    private void FixedUpdate()
    {
        head.localEulerAngles = new Vector3(-lookAngle.y, head.localEulerAngles.y, head.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, lookAngle.x, transform.localEulerAngles.z);
    }
}
