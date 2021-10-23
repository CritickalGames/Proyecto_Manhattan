using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [HideInInspector]public static LevelManager lM;
    [SerializeField]private Sprite trafficGreenLight;
    [HideInInspector]public int spawnScene;
    [HideInInspector] public Dictionary<string, bool> countriesUnlocked = new Dictionary<string, bool>();
    [HideInInspector]public bool transitioning = false;
    private bool levelPassed;
    private int nextScene;

    #region Getters & Setters
    public bool CanPass()
    {
        return this.levelPassed;
    }
    public bool GetCountry(string name)
    {
        if (this.countriesUnlocked.ContainsKey(name))
            return this.countriesUnlocked[name];
        else
            return false;
    }
    public void UnlockCountry(string name)
    {
        if (this.countriesUnlocked.ContainsKey(name))
        {
            countriesUnlocked[name] = true;
            GameManager.gM.SaveGame();
        }
    }
    #endregion

    void Awake()
    {
        if (lM != null)
            Destroy(this.gameObject);
        else
            lM = this;
        DontDestroyOnLoad(this);
    }
    public void RestartLevel()
    {
        StartAnim(this.spawnScene);
    }
    public void LevelFinished()
    {
        GameObject trafficLights = GameObject.Find("World/Terrain/TrafficLights");
        if (trafficLights != null)
        {
            SpriteRenderer spriteRenderer = trafficLights.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.trafficGreenLight;
        }
        this.levelPassed = true;
    }
    public void NextLevel(int nextLevel, bool heal)
    {
        if(nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            if (heal)
                GameManager.gM.SetMaxHealth();
            this.levelPassed = false;
            StartAnim(nextLevel);
        }
    }
    public void InstantiateLevels()
    {
        this.countriesUnlocked.Add("Germany", true);
        this.countriesUnlocked.Add("Poland", false);
        this.countriesUnlocked.Add("Ukraine", false);
        this.countriesUnlocked.Add("Russia", false);
        this.countriesUnlocked.Add("France", false);
        this.countriesUnlocked.Add("Spain", false);
        this.countriesUnlocked.Add("Portugal", false);
        this.countriesUnlocked.Add("Final", false);
    }
    public void StartAnim(int scene)
    {
        Animator anim = GameObject.Find("Transition").GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Start");
        this.nextScene = scene;
        this.transitioning = true;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(this.nextScene);
        Destroy(GameManager.gM.eM.gameObject);
        this.transitioning = false;
    }
}
