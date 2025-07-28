using UnityEngine;
using System.Collections;

public class BulletMg : MonoBehaviour {

 	public float speed;
	public Rigidbody rb;

    public GameObject b;

    public float bulletLf = 1.0f;

    void Start ()
    {
        rb.velocity = transform.forward * speed;
    }

    void Update(){

        bulletLf -= Time.deltaTime;

        if(bulletLf <= 0){
             Destroy(b);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(b);
        }
    }
}
