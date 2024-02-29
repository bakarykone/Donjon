using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player presses w key
        if(Input.GetKey("w")) 
        {
            print("w key has been pressed");
            //then set the walking boolean to be true
            animator.SetBool("isWalkingForward", true);
        }
    }
}
