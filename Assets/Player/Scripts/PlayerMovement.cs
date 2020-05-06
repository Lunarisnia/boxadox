using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;
    public float mass = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float rayCastMaxDistance = 40f;
    public LayerMask rayCastTarget;


    bool isGrabbing = false;
    public float heldItemPosition = 10f;


    bool isGrounded;
    CharacterController controller;
    Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyInput();
    }

    void ApplyInput()
    {
        MovePlayer();
        JumpPlayer();
        RayCasted();
    }

    void RayCasted()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Grab"))
        {
            if (!isGrabbing)
            {
                if (Physics.Raycast(ray, out hit, rayCastMaxDistance, rayCastTarget))
                {
                    GrabItem(hit.collider.gameObject);
                }
            }
            else
            {
                DropItem();
            }

        }

    }

    Rigidbody boxRb;
    CharacterJoint charJoint;
    void GrabItem(GameObject item)
    {
        boxRb = item.GetComponent<Rigidbody>();
        charJoint = item.GetComponent<CharacterJoint>() != null
            ? item.GetComponent<CharacterJoint>()
            : item.AddComponent<CharacterJoint>();

        boxRb.useGravity = false;
        boxRb.constraints = RigidbodyConstraints.FreezeRotation;
        charJoint.connectedBody = Camera.main.gameObject.GetComponent<Rigidbody>();
        isGrabbing = true;
    }

    void DropItem()
    {
        boxRb.useGravity = true;
        boxRb.constraints = RigidbodyConstraints.None;
        charJoint.breakForce = 0;
        isGrabbing = false;
    }

    void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight) * -2f * gravity;
        }
    }

    void MovePlayer()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(move * movementSpeed * Time.deltaTime);

        Gravity();
    }

    void Gravity()
    {
        velocity.y += mass * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        GroundCheck();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
