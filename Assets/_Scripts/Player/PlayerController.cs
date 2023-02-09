using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StatsManager
{
    #region Variables

    [Header("Character Properties")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float turnSmoothVelocity = 0.1f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private GameObject sliderGO;
    [SerializeField] private float healthBarOffsetY;

    private float horizontal, vertical;
    private float verticalSpeed;
    private float gravity = 9.81f;

    private bool grounded = false;

    private Vector3 direction = Vector3.zero;
    private Vector3 movement;

    private PlayerInputs inputs;
    private Camera cam;
    private CharacterController cc;
    private GameManager _gm;

    private int _maxHealth;
    private int _currentHealth;
    private int _attackDamage;
    private GameObject PlayerHealthBar;
    private UIManager _ui;
    private Vector2 _screenSize;
    private GameObject _healthBarSlider;

    #endregion


    #region Properties
    #endregion

    #region Built Methods

    void Start()
    {
        cam = Camera.main;
		_ui = UIManager.instance;
        inputs = GetComponent<PlayerInputs>();
        cc = GetComponent<CharacterController>();
        _gm = GameManager.instance;

        _maxHealth = MaxHealth;
        _currentHealth = _maxHealth;
        _attackDamage = AttackDamage;
        PlayerHealthBar = Instantiate(sliderGO, transform);
        PlayerHealthBar.transform.localPosition = new Vector3(0, healthBarOffsetY, 0);
        _healthBarSlider = PlayerHealthBar.transform.GetChild(0).GetChild(0).gameObject;
        _ui.SetHealthBar(_maxHealth, _healthBarSlider, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        VerticalMovement();
        PlayerRotation();
        _ui.UpdateHealthBarRotation(PlayerHealthBar);
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Juggernaut"){
            Destroy(other.gameObject);
            JuggernautBonus();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Batiment")){
            _gm.ChangeOpacity();
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Batiment")){
            _gm.ChangeOpacity();
        }
    }

    #endregion
    void JuggernautBonus(){
        _maxHealth += 100;
        _ui.SetHealthBar(_maxHealth, _healthBarSlider, _currentHealth);
    }
    
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
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void PlayerRotation(){
        _screenSize = new Vector2(Screen.width, Screen.height);
        Vector3 lookPosition = new Vector3(Input.mousePosition.x - _screenSize.x/2, 0, Input.mousePosition.y - _screenSize.y/2);
        transform.LookAt(transform.position + new Vector3(0, 1, 0));
        transform.LookAt(-lookPosition);
    }

    public void Hurt(int damage)
    {
        _currentHealth -= damage;
        _ui.UpdateHealthBar(_currentHealth, _healthBarSlider);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Death(){
        Destroy(gameObject);
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
