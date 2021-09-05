using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    int enemiesLeft;
    void Start()
    {
        GameManager.gM.SetCounterScript();
        this.enemiesLeft = GameObject.Find("/NPC/Enemies").GetComponent<Transform>().childCount;
    }
    public void SubtractEnemy()
    {
        this.enemiesLeft--;
        if (this.enemiesLeft <= 0)
            GameManager.gM.LevelFinished();
    }
}
