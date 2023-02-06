using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
