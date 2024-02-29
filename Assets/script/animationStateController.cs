using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animationStateController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //increase performance
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = Vector3.zero; // Initialize movement vector to zero

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
        transform.Translate(movement);

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
