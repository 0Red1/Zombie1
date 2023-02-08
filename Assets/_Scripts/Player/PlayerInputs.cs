using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Variables

    private Vector2 movement = Vector2.zero;

    #endregion


    #region Properties

    public Vector2 Movement => movement;

    #endregion

    #region Built in Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }
    #endregion

    #region Custom Methods

    void GetInputs()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    #endregion
}
