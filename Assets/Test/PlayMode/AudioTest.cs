using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AudioTest
{
     [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
    [UnityTest]
    public IEnumerator TestPlayAudio()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Sound sound in AudioManager.aM.sounds)
            AudioManager.aM.Pause(sound.name);
        foreach (Sound sound in AudioManager.aM.sounds)
        {
            AudioManager.aM.Play(sound.name);
            yield return null;
            Assert.IsTrue(sound.source.isPlaying);
            AudioManager.aM.Pause(sound.name);
        }
    }
}
