using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform player;
    [SerializeField] private float offset;

    private GameObject _bat;
    #endregion

    #region Properties
    #endregion

    #region Built in Methods
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z + offset);
    }
    #endregion

    #region Custom Methods
    #endregion
}
