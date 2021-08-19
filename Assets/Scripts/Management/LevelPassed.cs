using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    [SerializeField] int nextLevel = 0;

    public void NextLevel()
    {
        GameManager.gM.NextLevel(nextLevel);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            GameManager.gM.NextLevel(nextLevel);
    }
}
