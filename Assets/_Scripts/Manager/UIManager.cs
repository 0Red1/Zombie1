using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private GameObject lifeText;

	public static UIManager instance;

	private int _maxHealth;
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
	public void SetHealthBar(int maxHealth, GameObject healthBar, int currentLife, bool isPlayer){
		healthBar.GetComponent<Slider>().maxValue = maxHealth;
		if (currentLife != 0){
			healthBar.GetComponent<Slider>().value = currentLife;
			if (isPlayer){
				lifeText.GetComponent<TMP_Text>().text = currentLife.ToString() + " / " + maxHealth.ToString();
				_maxHealth = maxHealth;
			}
		}
		else{
			healthBar.GetComponent<Slider>().value = maxHealth;
			if (isPlayer){
				lifeText.GetComponent<TMP_Text>().text = maxHealth.ToString() + " / " + maxHealth.ToString();
				_maxHealth = maxHealth;
			}
		}
	}

	public void UpdateHealthBar(int currentLife, GameObject healthBar, bool isPlayer){
		healthBar.GetComponent<Slider>().value = currentLife;
		if (isPlayer){
			lifeText.GetComponent<TMP_Text>().text = currentLife.ToString() + " / " + _maxHealth.ToString();
		}
	}

	public void UpdateHealthBarRotation(GameObject healthBar){
		healthBar.transform.LookAt(Camera.main.transform); 
	}
	#endregion
}
