using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);//detruire la balle

        }
        if (collision.gameObject.tag == "Foe")
        {
            Destroy(collision.gameObject);// detruit l'ennemie
            Destroy(this.gameObject);//detruire la balle
        }
        if (collision.gameObject.tag == "Props")
        {
            Destroy(collision.gameObject);// detruit l'ennemie
            Destroy(this.gameObject);//detruire la balle
        }
    }
}
