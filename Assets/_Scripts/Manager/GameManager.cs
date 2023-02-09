using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private Material opacityMaterial;

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
	public void ChangeOpacity(GameObject roof){
		Material matToSet;
		if (!_originMaterial){
			_originMaterial = roof.GetComponent<Renderer>().material;
			matToSet = opacityMaterial;
		}
		else{
			matToSet = _originMaterial;
			_originMaterial = null;
		}
		roof.GetComponent<Renderer>().material = matToSet;
	}
	#endregion
}
