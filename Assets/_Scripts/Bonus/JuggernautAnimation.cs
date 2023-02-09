using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggernautAnimation : MonoBehaviour
{
    #region Variables
	[SerializeField] private float offsetY;

	private float _originPositionY;
	#endregion
	
	#region Properties
	#endregion
	
	#region Built in Methods
	void Start()
    {
        _originPositionY = transform.position.y;
		StartCoroutine(RotationAnim());
		StartCoroutine(SpawnAnim());
    }
	#endregion
	
	#region Custom Methods
	IEnumerator RotationAnim(){
		transform.eulerAngles += new Vector3(0, 3, 0);
		yield return new WaitForSeconds(0.02f);
		StartCoroutine(RotationAnim());
	}

	IEnumerator SpawnAnim(){
		while (transform.position.y < _originPositionY + (offsetY - offsetY / 4)){
			transform.position += new Vector3(0, 0.1f, 0);
			yield return new WaitForSeconds(0.01f);
		}
		while (transform.position.y < _originPositionY + offsetY){
			transform.position += new Vector3(0, 0.05f, 0);
			yield return new WaitForSeconds(0.01f);
		}
		while (transform.position.y > _originPositionY + (offsetY - offsetY / 4)){
			transform.position -= new Vector3(0, 0.05f, 0);
			yield return new WaitForSeconds(0.01f);
		}
		while(transform.position.y > _originPositionY){
			transform.position -= new Vector3(0, 0.1f, 0);
			yield return new WaitForSeconds(0.01f);
		}
	}
	#endregion
}
