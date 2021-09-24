using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [System.NonSerialized]public static LevelManager lM;
    [SerializeField]private Sprite trafficGreenLight;
    [System.NonSerialized] public Dictionary<string, bool> countriesUnlocked = new Dictionary<string, bool>();
    private bool levelPassed;

    #region Getters & Setters
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
        LoadScene(2);
    }
    public void LevelFinished()
    {
        GameObject trafficLights = GameObject.Find("World/Terrain/TrafficLights");
        if (trafficLights != null)
        {
            SpriteRenderer spriteRenderer = trafficLights.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = trafficGreenLight;
        }
        levelPassed = true;
    }
    public void NextLevel(int nextLevel)
    {
        if(nextLevel < SceneManager.sceneCountInBuildSettings && levelPassed && nextLevel > 1)
        {
            levelPassed = false;
            LoadScene(nextLevel);
        } else if (levelPassed)
        {
            GameManager.gM.SetMaxHealth();
            levelPassed = false;
            LoadScene(1);
        } else if (!levelPassed)
        {
            MessageBar messageScript = GameObject.Find("/UI/Canvas/Message/Image").GetComponent<MessageBar>();
            messageScript.SetTrueBool();
        }
    }
    public void InstantiateLevels()
    {
        countriesUnlocked.Add("Germany", true);
        countriesUnlocked.Add("Poland", false);
        countriesUnlocked.Add("Ukraine", false);
        countriesUnlocked.Add("Russia", false);
        countriesUnlocked.Add("France", false);
        countriesUnlocked.Add("Spain", false);
        countriesUnlocked.Add("Portugal", false);
    }
    private void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Destroy(GameManager.gM.eM.gameObject);
    }
}
