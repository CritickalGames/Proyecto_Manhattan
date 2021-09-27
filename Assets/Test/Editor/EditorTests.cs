using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditorTests
{
    [Test]
    public void TestSetMaxHealth()
    {
        GameObject gameManager = new GameObject();
        GameManager gM = gameManager.AddComponent<GameManager>();
        gM.SetMaxHealth();
        Assert.AreEqual(gM.currentPlayerHealth, gM.maxPlayerHealth);
    }
    [Test]
    public void TestPauseScript()
    {
        GameObject gameManager = new GameObject();
        GameObject pauseObject = new GameObject();
        GameManager gM = gameManager.AddComponent<GameManager>();
        PauseController pause = pauseObject.AddComponent<PauseController>();
        gM.SetPauseScript(pause);
        Assert.AreEqual(pause, gM.pauseScript);
    }
    [Test]
    public void TestHealthManage()
    {
        GameObject healthObject = new GameObject();
        HealthManage health = healthObject.AddComponent<HealthManage>();
        health.SetMaxHealth();
        Assert.AreEqual(health.currentHealth, health.maxHealth);
    }
    [Test]
    public void TestAbilityCount()
    {
        GameObject gameManager = new GameObject();
        GameManager gM = gameManager.AddComponent<GameManager>();
        for (int i = gM.abilityCount ; i < 3 ; i++)
        {
            gM.SetAbilityCount();
            Assert.AreEqual(gM.abilityCount, i + 1);
        }
    }
}
