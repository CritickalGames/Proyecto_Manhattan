using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField]private Image abilityImage;
    [SerializeField]private Sprite defaultSprite;
    [SerializeField]private Sprite vodkaSprite;
    [SerializeField]private Sprite arquebusSprite;
    
    void Awake()
    {
        this.healthSlider = this.gameObject.GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        this.healthSlider.maxValue = health;
    }
    public void SetHealth(int health)
    {
        this.healthSlider.value = health;
    }
    public void UpdateImage(int i)
    {
        switch (i)
        {
            case 1:
                this.abilityImage.sprite = vodkaSprite;
                break;
            case 2:
                this.abilityImage.sprite = arquebusSprite;
                break;
            default:
                this.abilityImage.sprite = defaultSprite;
                break;
        }
    }
}
