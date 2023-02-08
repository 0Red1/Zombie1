using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    #region Variables

    private HealthManager hm;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        hm = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hm = GetComponent<HealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Juggernaut"))
        {
            hm.maxHealth += 100f;
            hm.currentHealth = hm.maxHealth;
            hm.healthBar.maxValue = hm.maxHealth;
            hm.healthBar.value = hm.currentHealth;
            Destroy(other.gameObject);
        } 
    }
}
