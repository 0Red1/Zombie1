using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private Material opacityMaterial;
	[SerializeField] private List<GameObject> objectToChangeOpacity = new List<GameObject>();

	private Material _originMaterial;

	public static GameManager instance;
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
	public void ChangeOpacity(){
		Material matToSet;
		if (!_originMaterial){
			_originMaterial = objectToChangeOpacity[0].GetComponent<Renderer>().material;
			matToSet = opacityMaterial;
		}
		else{
			matToSet = _originMaterial;
			_originMaterial = null;
		}
		foreach (GameObject batGO in objectToChangeOpacity){
			batGO.GetComponent<Renderer>().material = matToSet;
		}
	}
	#endregion
}
