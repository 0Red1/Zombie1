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
	public void SetHealthBar(int maxHealth, GameObject healthBar){
		healthBar.GetComponent<Slider>().maxValue = maxHealth;
		healthBar.GetComponent<Slider>().value = maxHealth;
	}

	public void UpdateHealthBar(int currentLife, GameObject healthBar){
		healthBar.GetComponent<Slider>().value = currentLife;
	}

	public void UpdateHealthBarPosition(GameObject healthBar, GameObject objectToFollow){
		Vector3 screenPos = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);
		print(screenPos);
	}
	#endregion
}
