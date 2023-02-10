using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject helpMenu;
    #endregion

    #region Properties
    #endregion

    #region Built in Methods
    #endregion

    #region Custom Methods
    private void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void Help(){
        if (helpMenu.activeSelf){
            StartCoroutine(AnimHelpMenu(false));
        }
        else{
            helpMenu.SetActive(true);
            StartCoroutine(AnimHelpMenu(true));
        }
    }

    public void ButtonPressed(GameObject button){
        StartCoroutine(AnimMenu(button));
    }

    IEnumerator AnimMenu(GameObject button){
        int dir = 1;
        if (!menu.activeSelf){
            dir = -1;
            menu.SetActive(true);
        }
        for (int i = 0; i < 150; i++){
            menu.transform.localPosition += new Vector3(0, 1150 / 150 * dir, 0);
            yield return new WaitForSeconds(0.0001f);
        }
        if (button){
            if (button.name == "Quit"){
                QuitGame();
            }
            else if (button.name == "Play"){
                Play();
            }
            else if (button.name == "Help"){
                menu.SetActive(false);
                Help();
            }
        }
    }

    IEnumerator AnimHelpMenu(bool isActive){
        int dir = 1;
        if (isActive){
            dir = -1;
        }
        for (int i = 0; i < 150; i++){
            helpMenu.transform.localPosition += new Vector3(0, 1150 / 150 * dir, 0);
            yield return new WaitForSeconds(0.0001f);
        }
        if (!isActive){
            helpMenu.SetActive(false);
            StartCoroutine(AnimMenu(null));
        }
    }
    #endregion
}
