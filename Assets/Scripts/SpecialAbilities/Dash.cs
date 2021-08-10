using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] int playerMask;
    void Start()
    {
        playerMask = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        int mask = target.gameObject.layer;
        if (mask == playerMask)
        {
            GameManager.gM.playerScript.SetItem(0, true);
            Destroy(this.gameObject);
        }
    }
}
