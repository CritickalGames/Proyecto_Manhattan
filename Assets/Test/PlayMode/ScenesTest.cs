using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ScenesTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
    [UnityTest]
    public IEnumerator TestNextScene()
    {
        yield return new WaitForSeconds(1);
        LevelManager.lM.StartAnim(5, false);
        yield return new WaitForSeconds(2);;
        Assert.AreNotEqual(3, SceneManager.GetActiveScene().buildIndex);
    }
}
