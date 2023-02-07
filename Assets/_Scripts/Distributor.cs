using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour
{
    #region Variables

    [SerializeField] private int damageBeforeDestruction = 3;
    [SerializeField] private GameObject juggernaut;

    #endregion

    public void Break(int damage)
    {
        damageBeforeDestruction -= damage;

        if (damageBeforeDestruction <= 0)
        {
            Destroy(gameObject);
            Instantiate(juggernaut);
        }
    }
}
