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
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(3, SceneManager.GetActiveScene().buildIndex);
    }
    [UnityTest]
    public IEnumerator TestDialogues()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        yield return new WaitForSeconds(1f);
        bool hasCutscene = DialogueManager.dM.InCutscene;
        LevelManager.lM.StartAnim(6, true);
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(hasCutscene, DialogueManager.dM.InCutscene);
    }
    [UnityTest]
    public IEnumerator TestNoDialogues()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        yield return new WaitForSeconds(1f);
        bool hasCutscene = DialogueManager.dM.InCutscene;
        LevelManager.lM.StartAnim(6, false);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(hasCutscene, DialogueManager.dM.InCutscene);
    }
    [UnityTest]
    public IEnumerator TestNotFinishedSign()
    {
        SceneManager.LoadScene(3);
        yield return new WaitForSeconds(0.5f);
        DialogueManager.dM.EndDialogue();
        GameObject.Find("Player(Clone)").transform.position = GameObject.Find("PassLevel").transform.position - new Vector3(1,0,0);
        yield return new WaitForSeconds(1f);
        Animator anim = GameObject.Find("/UI/Canvas/Message/Image").GetComponent<Animator>();
        Assert.IsTrue(anim.GetBool("ShowMessage"));
    }
}
