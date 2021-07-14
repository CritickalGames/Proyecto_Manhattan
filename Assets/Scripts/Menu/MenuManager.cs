using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject configMenu;
    [SerializeField] GameObject mainMenu; 

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Config()
    {
        mainMenu.SetActive(false);
        configMenu.SetActive(true);
    }
    public void ConfigBack()
    {
        mainMenu.SetActive(true);
        configMenu.SetActive(false);
    }
}
