using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public float speed;

    public InputActionReference fireAction;
    public InputActionReference horizontalAction;
    public InputActionReference verticalAction;
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
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

        Vector3 movement = this.transform.position;
        //Ajout du mouvement horizontal 
        movement += Vector3.right * speed * horizontalAction.action.ReadValue<float>() * Time.deltaTime;

        //Ajout du mouvement vertical 
        movement += Vector3.forward * speed * verticalAction.action.ReadValue<float>() * Time.deltaTime;

        rb.MovePosition(movement);
        //this.transform.position += Vector3.right * speed * horizontalAction.action.ReadValue<float>() * Time.deltaTime;


        if (fireAction.action.triggered)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, this.transform.rotation);
        }
    }
}
