using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieVision : MonoBehaviour
{
    public EnnemyController ennemyController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ennemyController.SetEnemyAI(EnnemyAI.isChasing);
        }
    }
}