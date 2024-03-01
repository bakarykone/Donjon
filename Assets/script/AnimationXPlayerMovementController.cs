using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationXPlayerController : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    int isRunningHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    int isWalkingBackwardHash;
    int isJumpingHash;

    public float runSpeed;
    public float walkSpeed;
    public float jumpForce;
    public float jumpCooldown = 0.5f; // Temps de recharge entre les sauts
    private bool canJump = true;
    private bool hasJumped = false;

    public InputActionReference fireAction;
    public InputActionReference horizontalAction;
    public InputActionReference verticalAction;
    public InputActionReference sprintAction;
    public InputActionReference jumpAction;

    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        //increase performance
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    void RotateToward(Vector3 pos)
    {
        pos = new Vector3(pos.x, this.transform.position.y, pos.z);
        this.transform.LookAt(pos);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if ((Physics.Raycast(ray, out hit, 100f, 1 << 3)))
        {
            print("Ray hit at : " + hit.point);
            RotateToward(hit.point);
        }

        Vector3 movement = this.transform.position; // Initialize movement vector to zero

        // Check input actions
        bool forwardPressed = verticalAction.action.ReadValue<float>() > 0;
        bool runPressed = sprintAction.action.IsInProgress();
        bool leftPressed = horizontalAction.action.ReadValue<float>() < 0;
        bool rightPressed = horizontalAction.action.ReadValue<float>() > 0;
        bool backwardPressed = verticalAction.action.ReadValue<float>() < 0;
        bool jumpPressed = jumpAction.action.triggered;

        // Update movement vector based on input
        if (forwardPressed)
        {
            movement += Vector3.forward * (runPressed ? runSpeed : walkSpeed) * Time.deltaTime;
        }
        if (backwardPressed)
        {
            movement += Vector3.back * (runPressed ? runSpeed : walkSpeed) * Time.deltaTime;
        }
        if (leftPressed)
        {
            movement += Vector3.left * (runPressed ? runSpeed : walkSpeed) * Time.deltaTime;
        }
        if (rightPressed)
        {
            movement += Vector3.right * (runPressed ? runSpeed : walkSpeed) * Time.deltaTime;
        }

        // Translate the object based on the calculated movement vector
        //transform.Translate(movement);

        rb.MovePosition(movement);

        // Update animator parameters
        animator.SetBool(isWalkingForwardHash, forwardPressed);
        animator.SetBool(isRunningHash, runPressed && forwardPressed);
        animator.SetBool(isWalkingLeftHash, leftPressed);
        animator.SetBool(isWalkingRightHash, rightPressed);
        animator.SetBool(isWalkingBackwardHash, backwardPressed);
        animator.SetBool(isJumpingHash, jumpPressed);

        // Fire action
        if (fireAction.action.triggered)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
        }

        // Jump action
        if (canJump && jumpPressed)
        {
            if (!hasJumped)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            canJump = true;
        }
    }

}
