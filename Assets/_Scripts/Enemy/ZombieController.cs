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

	private NavMeshAgent _navAgent;
	private GameObject _player;
	private float _originY; // Lock Y pour le déplacement car bug j'ai pas trouvé
	private GameObject _target;
	private GameObject _attackDetect;
	private bool _attack = false;
	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Start()
    {
        
		_originY = transform.position.y;
		_navAgent = GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player");
		_attackDetect = transform.GetChild(1).gameObject;
    }

    void Update()
    {
		if (!_attack){
			CheckTarget();
			if (_target){
				SetTarget();
				CheckTargetOnRange();
			}
		}
    }
	#endregion
	
	#region Custom Methods
	private void SetTarget(){
		_navAgent.destination = _target.transform.position;
		transform.position = new Vector3(transform.position.x, _originY, transform.position.z);
	}

	private void CheckTarget(){
		float distance = Vector3.Distance(transform.position, _player.transform.position);
		if (distance <= detectionRange && !_target){
			_target = _player;
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

	IEnumerator Attack(){
		print("Attack");
		yield return null; //A faire avec les anims
	}
	#endregion
}
