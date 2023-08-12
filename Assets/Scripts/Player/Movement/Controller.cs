using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    [SerializeField] float walkSpeed = 0.0f;
    [SerializeField] float runSpeed = 0.0f;
    [SerializeField] float crouchSpeed = 0.0f;
    [SerializeField] float jumpForce = 0.0f;
    [SerializeField] float acceleration = 0.0f;
    float actualMoveSpeed;
    public bool canmove = false;
    public  Vector2 movement { get; private set; }

    [Header("Groundcheck")]
    public Vector3 groundDetectorOffset;
    public float groundDetectorSize = 0.9f;
    public LayerMask groundLayer;
    public LayerMask ceilLayer;
    private bool hasGround{get { return ground; } } 
    private float friction { get { return ground ? ground.material.dynamicFriction : airFriction; } }
    public float airFriction = 0.1f;
    Collider ground;
    Vector3 ColPoint2Center;

    [Header("Crouching")]
    public float crouchHeight;
    private float basicHeight;
    private Vector3 basicCenter;
    private Vector3 crouchCenter;

    private Rigidbody rb;

    [SerializeField] private Animator animator;

    [SerializeField] CapsuleCollider mainCollider;

    public bool running;
    public bool crouchOrder;
    public bool crouching { get; private set;}
    public static Controller current;
    private void Awake()
    {
        current = this;
    }
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();



        if(!mainCollider) mainCollider = gameObject.GetComponent<CapsuleCollider>();
        basicHeight = mainCollider.height;
        basicCenter = mainCollider.center;
        crouchCenter = new Vector3(mainCollider.center.x, mainCollider.center.y - ((basicHeight - crouchHeight) / 2), mainCollider.center.z);

        ColPoint2Center = Vector3.up * (basicHeight / 2 - mainCollider.radius);

        if(!animator) { animator = GetComponentInChildren<Animator>(); }
    }

    private void FixedUpdate()
    {
        
        if (crouching && !crouchOrder && !Physics.CheckCapsule(transform.position + basicCenter + ColPoint2Center, transform.position + basicCenter - ColPoint2Center, mainCollider.radius * groundDetectorSize, ceilLayer))
        {
            crouching = false;

            animator.SetBool("Crouching", crouching);

            mainCollider.height = basicHeight;
            mainCollider.center = basicCenter;
        }

        GroundCheck();
        
        if (crouching) { actualMoveSpeed = crouchSpeed; }
        else if(running) { actualMoveSpeed = runSpeed; }
        else { actualMoveSpeed = walkSpeed; }
        //Debug.Log(movement);
        if (canmove)
        {
            Vector2 finalDir = SkullAGMaths.RotateVector(movement, new Vector2(transform.forward.x, transform.forward.z).normalized);
            Vector2 tempvector = SkullAGMaths.CalculateVelocity(new Vector2(rb.velocity.x, rb.velocity.z), finalDir, acceleration, actualMoveSpeed, friction);
            rb.velocity = new Vector3(tempvector.x, rb.velocity.y, tempvector.y);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

    }

    private void GroundCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + groundDetectorOffset, mainCollider.radius * groundDetectorSize, groundLayer);

        if(colliders.Length == 0)
        {
            ground = null;
        }
        else
        {
            ground = colliders[0];//quiza necesitemos algo más sofisticado en el futuro
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context)
    {
        if(context.started && hasGround) //lo de context started es para cuando pulsas un boton, se activa nada m�s pulsarlo, luego esta canceled que es al soltarlo;
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    public void crouch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            crouchOrder = true;

            crouching = true;

            animator.SetBool("Crouching", crouching);

            mainCollider.height = crouchHeight;
            mainCollider.center = crouchCenter;
        }
        else if(context.canceled) // aqui se ve lo del cancel, no se levanta enseguida por si tiene un techo encima;
        {
            crouchOrder = false;
        }
    }   

    public void run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            running = true;
        }
        else if (context.canceled)
        {
            running = false;
        }
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        if(crouching)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + basicCenter + ColPoint2Center, mainCollider.radius * groundDetectorSize);
            Gizmos.DrawWireSphere(transform.position + basicCenter - ColPoint2Center, mainCollider.radius * groundDetectorSize);
        }

        if(mainCollider)
        {
            Gizmos.color = hasGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position + groundDetectorOffset, mainCollider.radius * groundDetectorSize);
        }
    }
#endif
}
