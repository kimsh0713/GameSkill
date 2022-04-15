using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class TitleUi : MonoBehaviour
{
    public PlayableDirector PD;

    public TimelineAsset Start_ani;

    private void Start()
    {
        //PD.Play();
    }

    public void StartAni()
    {
        PD.playableAsset = Start_ani;
        PD.Play();
    }

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
