using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private GameObject zombieGO;
	[SerializeField] private GameObject spawnList;

	public static WaveManager instance;
	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Awake(){
        if (instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
	}
	#endregion
	
	#region Custom Methods
	public void StartWave(int numberZombie){
		StartCoroutine(SpawnZombie(numberZombie));
	}

	IEnumerator SpawnZombie(int numberZombie){
		for (int i = 0; i < numberZombie; i++){
			GameObject spawn = spawnList.transform.GetChild(Random.Range(0, spawnList.transform.childCount)).gameObject;
			Instantiate(zombieGO, spawn.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1f);
		}
	}
	#endregion
}
