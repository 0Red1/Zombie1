using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float damage;

    public float speed;
    public float destroyerTime;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyerTime);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //other.GetComponent<HealthManager>().Hurt(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Decor")
        {
            //other.GetComponent<HealthManager>().Hurt(damage);
            Destroy(gameObject);
        }
    }
}
