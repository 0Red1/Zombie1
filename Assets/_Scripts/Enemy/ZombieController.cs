using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    #region Variables
	[SerializeField] private float attackRange;
	[SerializeField] private float detectionRange;
	[SerializeField] private float chaseRange;
	[SerializeField] private int offsetRandomDestination;
	[SerializeField] private float timeBetweenAttacks;

	private NavMeshAgent _navAgent;
	private GameObject _player;
	private GameObject _attackDetect;
	private GameObject _ground;

	private GameObject _target;
	private bool _attack = false;
	private Vector2 _groundSize;
	private bool _hasRandomDestination = false;
	private bool _canAttack = false;
	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Start()
    {
        
		_navAgent = GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player");
		_attackDetect = transform.GetChild(1).gameObject;
		_ground = GameObject.Find("Ground");
		_groundSize = new Vector2(_ground.GetComponent<Renderer>().bounds.size.x, _ground.GetComponent<Renderer>().bounds.size.z);
    }

    void FixedUpdate()
    {
		if (!_attack){
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

	void OnTriggerStay(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _canAttack){
			print("BAM COUP DE POING DANS LE PLAYER");
			_canAttack = false;
		}
	}
	#endregion
	
	#region Custom Methods
	private void CheckTarget(){
		float distance = Vector3.Distance(transform.position, _player.transform.position);
		if (distance <= detectionRange && !_target){
			_hasRandomDestination = false;
			_target = _player;
			StartCoroutine(SetTarget());
		}
		else if (distance >= chaseRange && _target){
			_target = null;
		}
	}

	private void CheckTargetOnRange(){
		if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange){
			_navAgent.destination = transform.position;
			_attack = true;
			StartCoroutine(Attack());
		}
	}

	private void SetRandomDestination(){
		Vector3 randomDestination = Vector3.zero;
		int groundLimit = (int)_groundSize.x / 2;
		int randomX = Random.Range(-groundLimit + offsetRandomDestination, groundLimit - offsetRandomDestination);
		int randomZ = Random.Range(-groundLimit + offsetRandomDestination, groundLimit - offsetRandomDestination);
		randomDestination = new Vector3(randomX, 0, randomZ);
		_navAgent.destination = randomDestination;
		print(randomDestination);
	}

	private void CheckRandomDestination(){
		if (_navAgent.remainingDistance <= 1){
			_hasRandomDestination = false;
		}
	}

	IEnumerator Attack(){
		_attackDetect.SetActive(true);
		_canAttack = true;
		yield return new WaitForSeconds(timeBetweenAttacks);
		_attack = false;
		_canAttack = false;
		_attackDetect.SetActive(false);
	}

	IEnumerator SetTarget(){
		_navAgent.destination = transform.position;
		yield return new WaitForSeconds(1f);
		_navAgent.destination = _target.transform.position;
	}
	#endregion
}