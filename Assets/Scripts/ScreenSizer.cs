using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        Screen.fullScreen = true;
        Debug.Log(Screen.currentResolution);
    }
}
