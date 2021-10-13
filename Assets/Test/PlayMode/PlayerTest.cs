using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
    [UnityTest]
    public IEnumerator TestPlayerSpawn()
    {
        yield return null;
        GameObject player = GameObject.Find("Player(Clone)");
        Assert.NotNull(player);
    }
    [UnityTest]
    public IEnumerator TestPlayerDamaged()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        Player script = player.GetComponent<Player>();
        script.Damaged(20);
        yield return null;
        Assert.AreEqual(GameManager.gM.maxPlayerHealth - 20, GameManager.gM.currentPlayerHealth);
    }
    [UnityTest]
    public IEnumerator TestPlayerJump()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        Player script = player.GetComponent<Player>();
        float initialYPos = player.transform.position.y;
        script.movementScript.Jump();
        yield return new WaitForSeconds(0.5f);
        Assert.Greater(player.transform.position.y, initialYPos);
    }
    [UnityTest]
    public IEnumerator TestPlayerDoubleJump()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        Player script = player.GetComponent<Player>();
        script.movementScript.Jump();
        yield return new WaitUntil(() => script.stateScript.GetState("Grounded"));
        float initialYPos = player.transform.position.y;
        script.movementScript.Jump();
        yield return new WaitForSeconds(0.5f);
        Assert.Greater(player.transform.position.y, initialYPos);
    }
    [UnityTest]
    public IEnumerator TestPlayerShoot()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        Player script = player.GetComponent<Player>();
        yield return new WaitUntil(() => script.stateScript.GetState("Grounded"));
        script.specialScript.Arquebus();
        yield return null;
        Assert.IsNotNull(GameObject.Find("PlayerBullet(Clone)"));
    }
}
