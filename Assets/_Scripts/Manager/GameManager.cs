using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
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
	public void ChangeOpacity(GameObject target, float value){
		Color matColor = target.GetComponent<Renderer>().material.color;
		target.GetComponent<Renderer>().material.color = new Color(matColor.r, matColor.g, matColor.b, value);
	}
	#endregion
}
