using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class EnemyTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
    [UnityTest]
    public IEnumerator TestDimitriShoot()
    {
        SceneManager.LoadScene(14);
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        DimitriAttack dimitri = GameObject.Find("Dimitri").GetComponent<DimitriAttack>();
        yield return new WaitForSeconds(0.5f);
        dimitri.Shoot();
        yield return new WaitForSeconds(0.5f);
        GameObject bottle = GameObject.Find("Bottle(Clone)");
        Assert.IsNotNull(bottle);
    }
    [UnityTest]
    public IEnumerator TestBottleExplode()
    {
        SceneManager.LoadScene(14);
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        DimitriAttack dimitri = GameObject.Find("Dimitri").GetComponent<DimitriAttack>();
        yield return new WaitForSeconds(0.5f);
        dimitri.Shoot();
        yield return new WaitForSeconds(1f);
        GameObject bottle = GameObject.Find("Bottle(Clone)");
        yield return new WaitForSeconds(1f);
        Assert.AreNotEqual(bottle, GameObject.Find("Bottle(Clone)"));
    }
}
