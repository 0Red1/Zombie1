using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Variables

    private Vector2 movement = Vector2.zero;
    private bool _attack;
    private bool _pause;

    #endregion


    #region Properties

    public Vector2 Movement => movement;
    public bool Attack => _attack;
    public bool Pause => _pause;

    #endregion

    #region Built in Methods
    void Update()
    {
        GetInputs();
    }
    #endregion

    #region Custom Methods

    void GetInputs()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _attack = Input.GetMouseButtonDown(0);
        _pause = Input.GetKeyDown(KeyCode.Escape);
    }

    #endregion
}
