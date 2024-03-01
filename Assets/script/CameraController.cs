using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offset;
        //this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z); 
    }
}