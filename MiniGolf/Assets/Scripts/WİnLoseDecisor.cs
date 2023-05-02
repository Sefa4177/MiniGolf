using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WİnLoseDecisor : MonoBehaviour
{
    public Text winclock;
    public Text Loseclock;

    public GameObject winObject;
    public GameObject loseObject;
    
    public GameObject winPopUp;

    public GameObject startPosition;
    public string SceneName;
    
    void Start()
    {
;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == winObject)
        {
            winPopUp.SetActive(true);
            Lives.Instance.starNumber();
            Clock.instance.OnWin();
            winclock.text = Clock.instance.textClock.text;
            
        }
        else if(other.gameObject == loseObject)
        {
            gameObject.transform.position = startPosition.transform.position;
            Lives.Instance.Falled();
            Loseclock.text = Clock.instance.textClock.text;
        }
    }

}
