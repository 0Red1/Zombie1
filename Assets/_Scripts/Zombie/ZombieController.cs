using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieController : StatsManager
{
    #region Variables
	[Header("ZombieController")]
	[SerializeField] private float attackRange;
	[SerializeField] private float detectionRange;
	[SerializeField] private float chaseRange;
	[SerializeField] private int offsetRandomDestination;
	[SerializeField] private float timeBetweenAttacks;
	[SerializeField] private GameObject sliderGO;
	[SerializeField] private float healthBarOffsetY;
	[SerializeField] private GameObject juggernautGO;
	[SerializeField] private GameObject bloodParticles;

	private NavMeshAgent _navAgent;
	private GameObject _player;
	private GameObject _attackDetect;
	private GameObject _ground;
	private GameObject _canvas;
	private Animator _animator;
	private GameManager _gm;
	
	private UIManager _ui;

	private GameObject _target;
	private bool _attack = false;
	private Vector2 _groundSize;
	private bool _hasRandomDestination = false;
	private bool _canAttack = false;

	private int _maxHealth;
	private int _currentHealth;
	private int _attackDamage;
	private GameObject _healthBar;
	private GameObject _healthBarSlider;
	private bool _trigger = false;
	private bool _hurt = false;
	private State _animState;

	private enum State{
		Walk,
		Hit,
		Detect,
		Attack
	}

	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Start()
    {
		_navAgent = GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player");
		_attackDetect = transform.GetChild(1).gameObject;
		_ground = GameObject.Find("BasePlane");
		_canvas = GameObject.Find("Canvas");
		_groundSize = new Vector2(_ground.GetComponent<Renderer>().bounds.size.x, _ground.GetComponent<Renderer>().bounds.size.z);
		_animator = GetComponent<Animator>();

		_ui = UIManager.instance;
		_gm = GameManager.instance;

		_maxHealth = MaxHealth;
		_currentHealth = _maxHealth;
		_attackDamage = AttackDamage;
		_healthBar = Instantiate(sliderGO, transform);;
		_healthBarSlider = _healthBar.transform.GetChild(0).GetChild(0).gameObject;
		_healthBar.transform.localPosition = new Vector3(0, healthBarOffsetY, 0);
		_ui.SetHealthBar(_maxHealth, _healthBarSlider, 0, false);
		_animState = State.Walk;

    }

    void Update(){
		switch(_animState){
			case State.Walk:
				break;
			case State.Hit:
				_animator.SetTrigger("Hit");
				break;
			case State.Detect:
				_animator.SetTrigger("Detect");
				break;
			case State.Attack:
				_animator.SetTrigger("Attack");
				break;
		}
		if (_canAttack){
			if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange){
				_player.GetComponent<PlayerController>().Hurt(_attackDamage);
				_canAttack = false;
			}
		}
	}
	
	void FixedUpdate()
    {
		if (_gm.IsPlaying){
			if (!_attack && !_hurt){
				CheckTarget();;
				if (!_target){
					if (!_hasRandomDestination){
						_hasRandomDestination = true;
						SetRandomDestination();
					}
					else{
						CheckRandomDestination();
					}
				}
				else{
					CheckTargetOnRange();
				}
			}
		}
		else if (_navAgent.destination != transform.position){
			_navAgent.destination = transform.position;
		}
		_ui.UpdateHealthBarRotation(_healthBar);
    }
	#endregion
	
	#region Custom Methods
	private void CheckTarget(){
		float distance = Vector3.Distance(transform.position, _player.transform.position);
		if (distance <= detectionRange && !_target){
			_hasRandomDestination = false;
			_target = _player;
			_trigger = true;
			_animState = State.Detect;
			StartCoroutine(SetTarget());
		}
		else if (distance < chaseRange && _target && !_trigger){
			UpdateTargetPosition();
		}
		else if (distance >= chaseRange && _target){
			_target = null;
		}
	}

	private void CheckTargetOnRange(){
		if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange){
			_navAgent.destination = transform.position;
			_attack = true;
			_animState = State.Attack;
		}
	}

	private void SetRandomDestination(){
		Vector3 randomDestination = Vector3.zero;
		int groundLimit = (int)_groundSize.x / 2;
		int randomX = Random.Range(-groundLimit + offsetRandomDestination, groundLimit - offsetRandomDestination);
		int randomZ = Random.Range(-groundLimit + offsetRandomDestination, groundLimit - offsetRandomDestination);
		randomDestination = new Vector3(randomX, 0, randomZ);
		_navAgent.destination = randomDestination;
	}

	private void CheckRandomDestination(){
		if (_navAgent.remainingDistance <= 1){
			_hasRandomDestination = false;
		}
	}

	private void UpdateTargetPosition(){
		_navAgent.destination = _target.transform.position;
	}

	public void LaunchAttack(){
		StartCoroutine(Attack());
	}

	IEnumerator Attack(){
		_attackDetect.SetActive(true);
		_canAttack = true;
		yield return new WaitForSeconds(timeBetweenAttacks);
		_attack = false;
		_canAttack = false;
		_target = null;
		_attackDetect.SetActive(false);
	}

	IEnumerator SetTarget(){
		_navAgent.destination = transform.position;
		yield return new WaitForSeconds(1f);
		_trigger = false;
		if (_target){
			_navAgent.destination = _target.transform.position;
		}
	}

	public void Hurt(int damage){
		_animState = State.Hit;
		SetHurt();
		_navAgent.destination = transform.position;
		_currentHealth -= damage;
        _ui.UpdateHealthBar(_currentHealth, _healthBarSlider, false);
        if (_currentHealth <= 0)
        {
            _gm.ZombieDeath();
			Death();
        }
	}

	public void SetWalk(){
		_animState = State.Walk;
	}

	public void SetHurt(){
		if (!_hurt){
			_hurt = true;
		}
		else{
			_hurt = false;
		}
	}

	private void Death(){
		float drop = Random.Range(0.1f, 1f);
		if (drop < 0.06f){
			Instantiate(juggernautGO, transform.position, Quaternion.identity);
		}
		Instantiate(bloodParticles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	#endregion
}
