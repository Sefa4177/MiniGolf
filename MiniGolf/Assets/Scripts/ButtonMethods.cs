using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMethods : MonoBehaviour
{
    public Text Pauseclock;
    public void ShowInterstitial()
    {
        ADManager.Instance.ShowInterstitial();
    }
    public void ContinueAfterAD()
    {
        Lives.Instance.AfterADLives();
        Clock.instance.AfterAdClock();
    }
    public void GamePause()
    {
        Time.timeScale = 0;
        //BallController.instance.Stop();
        Clock.instance.PauseClock();
        Pauseclock.text = Clock.instance.textClock.text;
    }
    public void GameContinue()
    {
        Time.timeScale = 1;
        Clock.instance.UnPauseClock();

    }
}
