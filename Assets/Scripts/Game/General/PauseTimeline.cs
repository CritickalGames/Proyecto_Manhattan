using UnityEngine;
using UnityEngine.Playables;


public class PauseTimeline : MonoBehaviour
{
    void OnEnable() => Pause();
    void Pause()
    {
        PlayableDirector playableDirector = GameObject.Find("/Timeline").GetComponent<PlayableDirector>();
        if (playableDirector.state == PlayState.Paused)
            return;
        playableDirector.Pause();
    }
    public void Resume()
    {
        PlayableDirector playableDirector = GameObject.Find("/Timeline").GetComponent<PlayableDirector>();
        if (playableDirector.state == PlayState.Playing)
            return;
        if(DialogueManager.dM.sentences.Count == 0)
            playableDirector.Resume();
    }
}
