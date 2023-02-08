using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : HealthManager
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

    private int _maxHealth;
    private int _currentHealth;
    private int _attackDamage;
    private Slider _healthBar;

    #endregion


    #region Properties
    #endregion

    #region Built Methods

    void Start()
    {
        cam = Camera.main;
        inputs = GetComponent<PlayerInputs>();
        cc = GetComponent<CharacterController>();

        _maxHealth = MaxHealth;
        _currentHealth = _maxHealth;
        _attackDamage = AttackDamage;
        _healthBar = HealthBar;
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

    void UpdateAnimation()
    {
        /*if (!animator) return;

        animator.SetFloat("Velocity", direction.magnitude);

        if (cc.isGrounded)
        {
            animator.SetBool("isGrounded", true);

            animator.SetFloat("ComboTimer", Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1));
            if (inputs.Attack)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.ResetTrigger("Attack");
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isBigLanding", bigLanding);
            animator.SetFloat("VerticalSpeed", verticalSpeed);
        }*/
    }

    public void MeleeAttackStart()
    {
        //staff.SetActive(true);
    }

    public void MeleeAttackEnd()
    {
        //staff.SetActive(false);
    }

    public void DistanceAttackStart()
    {
        //pistol.SetActive(true);
    }

    public void DistanceAttackEnd()
    {
        //pistol.SetActive(false);
    }

    public void CanMove()
    {
        //canMove = true;
    }

    public void Spawned()
    {
        //spawned = true;
    }
}
