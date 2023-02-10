using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private GameObject lifeText;
	[SerializeField] private GameObject blackscreen;
	[SerializeField] private GameObject zombieNumberText;
	[SerializeField] private GameObject waveClearedText;
	[SerializeField] private GameObject menuPause;

	public static UIManager instance;

	private int _maxHealth;

	private GameManager _gm;
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

	void Start(){
		_gm = GameManager.instance;
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

	public void EndBlackScreen(){
		StartCoroutine(ShowBlackScreen());
	}
	
	IEnumerator ShowBlackScreen(){
		blackscreen.SetActive(true);
		for (int i = 0; i < 100; i++){
			Color currentColor = blackscreen.GetComponent<Image>().color;
			blackscreen.GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		_gm.Menu();
	}

	public void UpdateZombieNumber(int number){
		zombieNumberText.GetComponent<TMP_Text>().text = number.ToString();
	}

	public void WaveCleared(bool mustActive, int waveNumber){
		waveClearedText.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Wave " + waveNumber.ToString() + " cleared !";
		StartCoroutine(WaveClearedAnim(mustActive));
	}

	public void MenuPause(bool isPaused){
		menuPause.SetActive(isPaused);
	}
	
	IEnumerator WaveClearedAnim(bool mustActive){
		if (mustActive){
			waveClearedText.SetActive(true);
		}
		int dir = 1;
		if (mustActive){
			dir = -1;
		}
		for (int i = 0; i < 150; i++){
			waveClearedText.transform.localPosition += new Vector3(0, 2 * dir, 0);
			yield return new WaitForSeconds(0.0001f);
		}
		if (!mustActive){
			waveClearedText.SetActive(false);
		}
	}
	#endregion
}
