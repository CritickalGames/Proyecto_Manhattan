using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    
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
}
