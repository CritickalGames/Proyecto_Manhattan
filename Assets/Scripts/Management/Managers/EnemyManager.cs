using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.NonSerialized]public static EnemyManager eM;
    private Transform enemyParent;
    GameObject[] enemies;
    int enemiesLeft;

    void Awake()
    {
        if (eM != null)
            Destroy(this.gameObject);
        else
            eM = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        this.enemyParent = GameObject.Find("/Enemies").GetComponent<Transform>();
        this.enemiesLeft = this.enemyParent.childCount;
        ListEnemies();
    }
    private void ListEnemies()
    {
        enemies = new GameObject[this.enemyParent.childCount];
        for (int i=0 ; i < this.enemyParent.childCount ; i++)
            enemies[i]= this.enemyParent.GetChild(i).GetComponent<GameObject>();
    }
    public void SubtractEnemy()
    {
        this.enemiesLeft--;
        if (this.enemiesLeft <= 0)
            LevelManager.lM.LevelFinished();
    }
}
