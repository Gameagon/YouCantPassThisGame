using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Moviment")]
    private Vector2 movement;
    public float moveSpeed = 0.0f;
    [SerializeField] float walkSpeed = 0.0f;
    [SerializeField] float jumpForce = 0.0f;
    [SerializeField] float runsped = 0.0f;
    [SerializeField] float stealthspeed = 0.0f;
    Vector3 moveDirection;
    public Transform orientation;
    public float groundDrag;
    
    [Header("groundcheck")]
    bool grounded;
    public float playerHeight;
    public LayerMask whatIsGround;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    float startYScale;

    private InputSystemKeyboard kb;
    private Rigidbody rb;


    [SerializeField] private float smoothcrouch = 100.0f;
    CapsuleCollider playerCapsule;
    public MovimentState state;
    public enum MovimentState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    public void Start()
    {
        kb = gameObject.GetComponent<InputSystemKeyboard>();
        rb = gameObject.GetComponent<Rigidbody>();

        playerCapsule = gameObject.GetComponent<CapsuleCollider>();
        startYScale = transform.localScale.y;
    }
    private void Update()
    {
        //SpeedControl();
        crouch();
        grounded = Physics.Raycast(transform.position, Vector3.down,playerHeight * 0.5f + 0.2f, whatIsGround);

        if(grounded)
        {
            rb.drag = groundDrag;
        }
    }

    private void FixedUpdate()
    {
        Vector3 tempvector = SkullAGMaths.CalculateVelocity(new Vector2(rb.velocity.x, rb.velocity.z), movement, 20, moveSpeed, 1);
        rb.velocity = new Vector3(tempvector.x, rb.velocity.y, tempvector.y);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

        /*Vector2 axis = context.ReadValue<Vector2>() * moveSpeed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z,0.0f, Camera.main.transform.right.x);
        Vector3 wishDirection =(forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);*/

        

    }

    public void jump(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton() && Physics.Raycast(rb.transform.position, Vector3.down, 1))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void crouch()
    {
        if(kb.returnstealthdown())
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if(kb.returnstealthup())
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }   
}
