using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
	[SerializeField] private Material opacityMaterial;
	[SerializeField] private List<GameObject> objectToChangeOpacity = new List<GameObject>();

	private Material _originMaterial;
	
	private UIManager _ui;

	private bool _isPlaying = true;

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

	public void PlayerDead(){
		_isPlaying = false;
	}
	
	public void EndGame(){
		_ui.EndBlackScreen();
	}

	public void Menu(){
		SceneManager.LoadScene("MainMenu");
	}
	#endregion
}
