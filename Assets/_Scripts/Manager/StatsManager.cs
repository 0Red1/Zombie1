using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    #region Variables

    [Header("StatsManager")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackDamage;
    #endregion

    #region Properties
    public int MaxHealth => maxHealth;
    public int AttackDamage => attackDamage;
    #endregion

    #region Built in Methods
    #endregion

    #region Custom Methods
    #endregion
}
