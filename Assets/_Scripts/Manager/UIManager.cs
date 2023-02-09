using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
	public static UIManager instance;
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
	public void SetHealthBar(int maxHealth, GameObject healthBar, int currentLife){
		healthBar.GetComponent<Slider>().maxValue = maxHealth;
		if (currentLife != 0){
			healthBar.GetComponent<Slider>().value = currentLife;

		}
		else{
			healthBar.GetComponent<Slider>().value = maxHealth;
		}
	}

	public void UpdateHealthBar(int currentLife, GameObject healthBar){
		healthBar.GetComponent<Slider>().value = currentLife;
	}

	public void UpdateHealthBarRotation(GameObject healthBar){
		healthBar.transform.LookAt(Camera.main.transform); 
	}
	#endregion
}
