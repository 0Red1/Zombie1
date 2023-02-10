using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private Material opacityMaterial;
	[SerializeField] private List<GameObject> objectToChangeOpacity = new List<GameObject>();
	[SerializeField] private int baseNumberEnemies;

	private Material _originMaterial;
	
	private UIManager _ui;
	private WaveManager _wm;

	private bool _isPlaying = true;
	private int _numberWave = 1;
	private int _currentZombiesNumber;

	public static GameManager instance;
	#endregion
	
	#region Properties
	public bool IsPlaying => _isPlaying;
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
		_ui = UIManager.instance;
		_wm = WaveManager.instance;

		_wm.StartWave(baseNumberEnemies);
		_currentZombiesNumber = baseNumberEnemies;
		_ui.UpdateZombieNumber(_currentZombiesNumber);
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

	public void ZombieDeath(){
		_currentZombiesNumber--;
		_ui.UpdateZombieNumber(_currentZombiesNumber);
		if (_currentZombiesNumber == 0){
			StartCoroutine(NextWave());
		}
	}

	public void PlayerDead(){
		_isPlaying = false;
	}
	
	public void EndGame(){
		_ui.EndBlackScreen();
	}

	public void Menu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void Pause(){
		if (_isPlaying){
			_isPlaying = false;
			_ui.MenuPause(true);
		}
		else{
			_isPlaying = true;
			_ui.MenuPause(false);
		}
	}

	IEnumerator NextWave(){
		_ui.WaveCleared(true, _numberWave);
		yield return new WaitForSeconds(5f);
		_ui.WaveCleared(false, _numberWave);
		_numberWave++;
		_wm.StartWave(baseNumberEnemies * _numberWave);
		_currentZombiesNumber = baseNumberEnemies * _numberWave;
		_ui.UpdateZombieNumber(_currentZombiesNumber);
	}
	#endregion
}
