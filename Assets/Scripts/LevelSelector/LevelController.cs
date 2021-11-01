using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private Image[] countryImage = new Image[7];
    private Button[] countryButton = new Button[8];
    private string[] countryName = new string[8];

    void Start()
    {
        countryName[0] = "Germany";
        countryName[1] = "Poland";
        countryName[2] = "Ukraine";
        countryName[3] = "Russia";
        countryName[4] = "France";
        countryName[5] = "Spain";
        countryName[6] = "Portugal";
        countryName[7] = "Final";
        LevelAvailable();
    }
    public void LevelAvailable()
    {
        for (int i = 0 ; i < countryName.Length ; i++)
        {
            if (countryName[i] != "Final")
                countryImage[i] = GameObject.Find(countryName[i]).GetComponent<Image>();
            countryButton[i] = GameObject.Find(countryName[i]+"Button").GetComponent<Button>();
            if (LevelManager.lM.GetCountry(countryName[i]))
            {
                if (countryName[i] != "Final")
                    countryImage[i].color = new Color(countryImage[i].color.r, countryImage[i].color.g, countryImage[i].color.b, 1f);
                countryButton[i].interactable = true;
            }
        }
    }
    public void LoadScene(int levelScene)
    {
        LevelManager.lM.StartAnim(levelScene, true);
    }
    public void PlaySound(string sound)
    {
        AudioManager.aM.Play(sound);
    }
}
