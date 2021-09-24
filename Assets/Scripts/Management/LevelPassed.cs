using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    [SerializeField]private int nextLevel = 0;
    [SerializeField]private string[] UnlockCountry;

    public void NextLevel()
    {
        LevelManager.lM.NextLevel(nextLevel);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (UnlockCountry != null)
            for (int i = 0 ; i < UnlockCountry.Length ; i++)
                LevelManager.lM.UnlockCountry(UnlockCountry[i]);

        if (col.gameObject.CompareTag("Player"))
            LevelManager.lM.NextLevel(nextLevel);
    }
}
