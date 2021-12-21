using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public GameObject playerObj;
    private AudioSource SoundController;
    public AudioClip _bg1;
    public AudioClip _bg2;

//    private bool bg1_playable = true;
 //   private bool bg2_playable = false;

    private bool gamePause = false;
    void Start()
    {
        SoundController = gameObject.GetComponent<AudioSource>();
        Invoke("PlayMsc1", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGamePause(){
        gamePause = true;
    }
    public void setGameContinue(){
        gamePause = false;
    }
    public bool getGameTimeStatus(){
        return gamePause;
    }
    
    public void PlayMsc1(){
        SoundController.clip = _bg1;
        SoundController.Play();
    }

    public void StopMsc(){
        SoundController.Stop();
    }



}
