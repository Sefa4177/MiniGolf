using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static Lives Instance;
    public List<GameObject> Heartİmages;

    public GameObject gameOverPopUp;
    int lives_;
    int star_;
    public List<GameObject> StarImages;

    private void Awake() 
    {
        if(Instance)
        {
            Destroy(Instance);
        }

        Instance = this;    
    }
    void Start()
    {
        lives_ = 3;
        star_ = lives_;

        for(int heart = 0 ; heart < lives_ ; heart++)
        {
            Heartİmages[heart].SetActive(true);
        }
        
    }

    public void Falled()
    {

        for(int heart = 0 ; heart < lives_ ; heart++)
        {
            if(heart == lives_-1)
            {
                Heartİmages[heart].SetActive(false);
                lives_ = lives_ - 1;
                star_ = star_ - 1;
            }
            
        }
        CheckGameOver();
    }
    private void CheckGameOver()
    {
        if(lives_ <=0)
        {
            Clock.instance.OnGameOver();
            gameOverPopUp.SetActive(true);
        }
    }

    public void starNumber()
    {
        for(int star = 0 ; star < star_ ; star++)
        {
            StarImages[star].SetActive(true);
        }
    }

    public void AfterADLives()
    {
        lives_ = 3;
        Start();
    }
}
