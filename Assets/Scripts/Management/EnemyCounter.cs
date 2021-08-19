using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    int enemiesLeft;
    void Start()
    {
        this.enemiesLeft = (GameObject.FindGameObjectsWithTag("HasToBeKilled")).Length;
    }
    public void SubtractEnemy()
    {
        this.enemiesLeft--;
        if (this.enemiesLeft <= 0)
            GameManager.gM.LevelFinished();
    }
}
