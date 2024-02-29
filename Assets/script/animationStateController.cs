using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    int isRunningHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    int isWalkingBackwardHash;

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
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkingForward = animator.GetBool(isWalkingForwardHash);
        bool isWalkingLeft = animator.GetBool(isWalkingLeftHash);
        bool isWalkingRight = animator.GetBool(isWalkingRightHash);
        bool isWalkingBackward = animator.GetBool(isWalkingBackwardHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");

    //FORWARD
        //if player presses W key
        if (!isWalkingForward && forwardPressed) 
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingForwardHash, true);
        }
        // if player is not pressing W key
        if (isWalkingForward && !forwardPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingForwardHash, false);
        }

        //if player is walking and not running and presses left shift
        if (!isRunning && (forwardPressed && runPressed))
        {
            //then set the isRunning bool to be true
            animator.SetBool(isRunningHash, true);
        }
        //if player is running and stops running or stops walking
        if (isRunning && (!forwardPressed && runPressed))
        {
            //then set the isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
        }

    //LEFT
        //if player presses A key
        if (!isWalkingLeft && leftPressed)
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingLeftHash, true);
        }
        // if player is not pressing A key
        if (isWalkingLeft && !leftPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingLeftHash, false);
        }

    //RIGHT
        //if player presses D key
        if (!isWalkingRight && rightPressed)
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingRightHash, true);
        }
        // if player is not pressing D key
        if (isWalkingRight && !rightPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingRightHash, false);
        }

    //BACKWARD
        if (!isWalkingBackward && backwardPressed)
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingBackwardHash, true);
        }
        // if player is not pressing W key
        if (isWalkingBackward && !forwardPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingBackwardHash, false);
        }

    }
}
