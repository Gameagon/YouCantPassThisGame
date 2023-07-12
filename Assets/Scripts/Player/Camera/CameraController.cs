using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Rigidbody rb;
    public Transform follow;

    private InputSystemKeyboard kb;

    [SerializeField] float sensivility = 2.0f;
    // Start is called before the first frame update.0
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
        kb = gameObject.GetComponent<InputSystemKeyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = transform.position;
    }

    public void Look(InputAction.CallbackContext context)
    {
        pitch -= context.ReadValue<Vector2>().y * sensivility;
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
        yaw += context.ReadValue<Vector2>().x * sensivility;
        Camera.main.transform.localRotation = Quaternion.Euler(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0);


    }
}
