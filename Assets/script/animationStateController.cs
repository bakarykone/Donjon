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
        bool isWalking = animator.GetBool(isWalkingForwardHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");

    //FORWARD
        //if player presses w key
        if (!isWalking && forwardPressed) 
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingForwardHash, true);
        }
        // if player is not pressing w key
        if (isWalking && !forwardPressed)
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
        //if player presses a key
        if (!isWalking && leftPressed)
        {
            //then set the walking boolean to be true
            animator.SetBool(isWalkingLeftHash, true);
        }
        // if player is not pressing a key
        if (isWalking && !forwardPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingLeftHash, false);
        }
    }
}
