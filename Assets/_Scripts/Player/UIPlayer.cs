using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
	#region Variables
    public Slider playerHealthBar;
    public Transform playerPosition;
    public GameObject panelPause;
    private bool menuIsOpen = false;
    #endregion

    #region Properties
    #endregion
	
	
    #region Builts Methods
    void Start()
    {
        panelPause.SetActive(false);
        menuIsOpen = false;
    }
	
	
    
    void Update()
    {
        //playerPosition = 
        //playerHealthBar = playerPosition;
        MenuPause();
    }
	#endregion

    #region Custom Methods
    void MenuPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !menuIsOpen)
        {
            panelPause.SetActive(true);
            menuIsOpen = true;
        }
    }
    #endregion
}
