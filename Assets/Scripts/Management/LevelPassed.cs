using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    [SerializeField]private int nextLevel = 0;
    [SerializeField]private string[] UnlockCountry;
    [SerializeField]private int spawnScene;
    [SerializeField]private bool healOnPass;

    private void Start()
    {
        LevelManager.lM.spawnScene = this.spawnScene;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (this.UnlockCountry != null)
                for (int i = 0 ; i < this.UnlockCountry.Length ; i++)
                    LevelManager.lM.UnlockCountry(UnlockCountry[i]);
            LevelManager.lM.NextLevel(this.nextLevel, this.healOnPass);
        }
    }
}
