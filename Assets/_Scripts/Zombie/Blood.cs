using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    #region Variables
	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Start()
    {
		GetComponent<ParticleSystem>().collision.SetPlane(0, GameObject.Find("PlaneBlood").transform);
		StartCoroutine(DestroyTime());
    }
	#endregion
	
	#region Custom Methods
	IEnumerator DestroyTime(){
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
	#endregion
}
