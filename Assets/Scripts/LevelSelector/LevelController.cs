using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    Image countryImage;
    Button countryButton;
    [SerializeField]string countryName;

    void Start()
    {
        countryImage = GameObject.Find(countryName).GetComponent<Image>();
        if (countryName != "Germany")
            countryImage.color = new Color(countryImage.color.r, countryImage.color.g, countryImage.color.b, 0f);
        countryButton = GameObject.Find(countryName+"Button").GetComponent<Button>();
    }
}
