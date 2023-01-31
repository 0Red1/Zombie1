using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Character Properties")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float turnSmoothVelocity = 0.1f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float horizontal, vertical;
    private float verticalSpeed;
    private float gravity = 9.81f;

    private bool grounded = false;

    private Vector3 direction = Vector3.zero;
    private Vector3 movement;

    private PlayerInputs inputs;
    private Camera cam;
    private CharacterController cc;

    #endregion


    #region Properties
    #endregion

    #region Built Methods

    void Start()
    {
        cam = Camera.main;
        inputs = GetComponent<PlayerInputs>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        VerticalMovement();
    }

    #endregion

    void Movement()
    {
        if (!inputs) return;

        horizontal = inputs.Movement.x;
        vertical = inputs.Movement.y;

        direction.Set(horizontal, 0, vertical);

        Vector3 moveDirection = new Vector3();

        if (direction.normalized.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        movement = moveDirection.normalized * (moveSpeed * Time.deltaTime);
    }

    void VerticalMovement()
    {
        if (cc.isGrounded)
        {
            if (!grounded)
            {
                movement = Vector3.zero;
            }

            grounded = true;
            verticalSpeed = -gravity * 0.3f;
        }
        else
        {
            grounded = false;
            verticalSpeed -= gravity * Time.deltaTime;
        }

        movement += verticalSpeed * Vector3.up * Time.deltaTime;
        cc.Move(movement);
    }
}
