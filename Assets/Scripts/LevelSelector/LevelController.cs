using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    Image[] countryImage = new Image[4];
    Button[] countryButton = new Button[4];
    string[] countryName = new string[4];

    void Start()
    {
        countryName[0] = "Germany";
        countryName[1] = "Poland";
        countryName[2] = "Ukraine";
        countryName[3] = "Russia";
        LevelAvailable();
    }
    public void LevelAvailable()
    {
        for (int i = 0 ; i < countryName.Length ; i++)
        {
            countryImage[i] = GameObject.Find(countryName[i]).GetComponent<Image>();
            countryButton[i] = GameObject.Find(countryName[i]+"Button").GetComponent<Button>();
            if (GameManager.gM.GetCountryDictionary(countryName[i]))
            {
                countryImage[i].color = new Color(countryImage[i].color.r, countryImage[i].color.g, countryImage[i].color.b, 1f);
                countryButton[i].interactable = true;
            }
        }
    }
    public void LoadScene(int levelScene)
    {
        SceneManager.LoadScene(levelScene);
    }
}
