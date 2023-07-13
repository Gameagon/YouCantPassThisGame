using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonCameraController : MonoBehaviour
{
    [SerializeField] Vector2 lookAngle;
    [SerializeField] Transform head;


    [SerializeField] float sensitivity = 2.0f;
    // Start is called before the first frame update.0
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookAngle += context.ReadValue<Vector2>() * sensitivity;
        lookAngle.y = Mathf.Clamp(lookAngle.y, -90, 90);
        lookAngle.x = lookAngle.x % 360;
    }

    private void FixedUpdate()
    {
        head.localEulerAngles = new Vector3(-lookAngle.y, head.localEulerAngles.y, head.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, lookAngle.x, transform.localEulerAngles.z);
    }
}
