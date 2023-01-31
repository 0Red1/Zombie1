using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggernaut : MonoBehaviour
{
    public HealthManager hm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hm.maxHealth = 200f;
            hm.currentHealth = hm.maxHealth;
            Destroy(gameObject);
        } 
    }
}
