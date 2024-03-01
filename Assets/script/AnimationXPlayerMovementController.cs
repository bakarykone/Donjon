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

    public float currentSpeed;
    public float runSpeed;
    public float walkSpeed;

    public InputActionReference fireAction;
    public InputActionReference horizontalAction;
    public InputActionReference verticalAction;
    public InputActionReference SprintAction;

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
        bool runPressed = SprintAction.action.IsInProgress();
        bool leftPressed = horizontalAction.action.ReadValue<float>() < 0;
        bool rightPressed = horizontalAction.action.ReadValue<float>() > 0;
        bool backwardPressed = verticalAction.action.ReadValue<float>() < 0;

        // Update movement vector based on input
        if (forwardPressed)
        {
            movement += Vector3.forward * (runPressed ? runSpeed : walkSpeed) * Time.deltaTime;
        }
        if (backwardPressed)
        {
            movement += Vector3.back * walkSpeed * Time.deltaTime;
        }
        if (leftPressed)
        {
            movement += Vector3.left * walkSpeed * Time.deltaTime;
        }
        if (rightPressed)
        {
            movement += Vector3.right * walkSpeed * Time.deltaTime;
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

        // Fire action
        if (fireAction.action.triggered)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
        }
    }
}
