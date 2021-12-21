using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShellBehavior : MonoBehaviour
{
    public GameObject GameController;
    private AudioSource SoundController;
    public AudioClip _hideclip;
    public AudioClip _kickclip;
    public AudioClip _bounceclip;
    private bool gamePause = false;
    //private bool kick = false;
    private int dir;
    private float moveSpd, respawnTimeCountDown;
    [Range(0,15)] public float bounceVelocity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        SoundController = gameObject.GetComponent<AudioSource>();
        SoundController.PlayOneShot(_hideclip);
        respawnTimeCountDown = 9f;
        
    }

    // Update is called once per frame
    void Update()
    {
        gamePause = GameController.GetComponent<GameRules>().getGameTimeStatus();
        if( gamePause == true ){
            // game pause
            // freeze everyone
            
        }
        else{
            
            if(moveSpd != 0){
                Walk(dir);
            }
            else{
                respawnTimeCountDown = respawnTimeCountDown - Time.deltaTime;
                if(respawnTimeCountDown < 0){
                    Debug.Log("TURTLE NOW SHOULD AWAKE");
                }
                else if(respawnTimeCountDown < 3){
                    Debug.Log("TURTLE NOW START TO AWAKE");
                }
            }
                

        }


    }

    void Walk(float dir) {
        transform.position = new Vector3(transform.position.x + (dir * Time.deltaTime * moveSpd), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        // coll is the third object that interact with this object
        
        // contatc the right left side of the object
        if (coll.contacts[0].normal.x < 0){
            
        }
            // contatc the right left side of the object
        if (coll.contacts[0].normal.x > 0){
            
        }

        if(coll.gameObject.tag == "Player"){
            if(moveSpd != 0){
                if(System.Math.Round(coll.contacts[0].normal.y) < 0){
                    Debug.Log("Player Stopped the shell");
                    // stop the shell
                    moveSpd = 0;
                    SoundController.PlayOneShot(_hideclip);
                    coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceVelocity, ForceMode2D.Impulse);
                }
                else{
                    Debug.Log("ShellBehavior.cs: Player hits the moving shell and died.");
                    Debug.Log("Triggle y value(y > 0): " + coll.contacts[0].normal.y);
                    Debug.Log("After Rounding: " + System.Math.Round(coll.contacts[0].normal.y));
                    coll.gameObject.GetComponent<Player>().setGameover();
                }
                
                
            }
            else{
                    // play kick audio
                    SoundController.PlayOneShot(_kickclip);
                    // reset respawn status
                    respawnTimeCountDown = 9f;
                    // Player kick the shell from the relatively right
                    // set shell movespd and direction
                    if(coll.gameObject.transform.position.x > gameObject.transform.position.x){
                        Debug.Log("Player Kick the shell toward left");
                        moveSpd = 5f;
                        dir = -1;
                    }
                    // Player kick the turtle from the relatively left
                    // set shell movespd and direction
                    else{
                        Debug.Log("Player Kick the shell toward right");
                        moveSpd = 5f;
                        dir = 1;
                    }
                
            }
        }
        if(coll.gameObject.tag == "Pipe"){
            SoundController.PlayOneShot(_bounceclip);
            dir = dir * (-1);
        }
            
    }

    public float getShellSpd(){
        return moveSpd;
    }

}
