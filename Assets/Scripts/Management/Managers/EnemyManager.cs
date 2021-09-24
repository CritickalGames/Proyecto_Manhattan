using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Transform enemyParent;
    private GameObject[] enemies;
    private int enemiesLeft;

    void Start()
    {
        GameManager.gM.eM = this;
        this.enemyParent = GameObject.Find("/Enemies").GetComponent<Transform>();
        this.enemiesLeft = this.enemyParent.childCount - 1;
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
