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
    [UnityTest]
    public IEnumerator TestEnemyHit()
    {
        SceneManager.LoadScene(14);
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        GameObject dimitri = GameObject.Find("Dimitri");
        DimitriAttack dimitriAttack = dimitri.GetComponent<DimitriAttack>();
        dimitri.GetComponent<DimitriAI>().enabled = false;
        GameObject player = GameObject.Find("Player(Clone)");
        dimitri.transform.position = player.transform.position;
        dimitriAttack.Hit();
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(GameManager.gM.maxPlayerHealth, GameManager.gM.currentPlayerHealth);
    }
    [UnityTest]
    public IEnumerator TestChristopherShoot()
    {
        SceneManager.LoadScene(23);
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        ChristopherAttack christopher = GameObject.Find("Christopher Prorstata").GetComponent<ChristopherAttack>();
        yield return new WaitForSeconds(0.5f);
        christopher.Shoot();
        yield return new WaitForSeconds(0.5f);
        GameObject bullet = GameObject.Find("Bullet(Clone)");
        Assert.IsNotNull(bullet);
    }
}
