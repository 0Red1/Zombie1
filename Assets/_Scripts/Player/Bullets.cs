using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public int damage;

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
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            other.transform.parent.GetComponent<ZombieController>().Hurt(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Distributor")
        {
            other.GetComponent<Distributor>().Break(1);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Decor")
        {
            Destroy(gameObject);
        }
    }
}
