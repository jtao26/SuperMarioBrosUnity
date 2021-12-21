using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public GameObject bgmControl;
    private float speed = 6f;
    [Range(0,15)] public float jumpVelocity = 9f;
    [Range(0,15)] public float bounceVelocity = 7f;
    private bool jumpRequest = false;
    private bool onGround = false;
    private float boxHeight;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource SoundController;
    public AudioClip _jumpclip;
    public AudioClip _deadclip;
    public AudioClip _gameover;
    private float x;
    private float y;
    public bool Gameover = false;
    private bool deadMark = false;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.6f, boxHeight);
        target = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        SoundController = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Gameover){
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            if(Input.GetButtonDown("Jump") && onGround)
            {
                jumpRequest = true;
                _animator.SetBool("jump", true);
                SoundController.PlayOneShot(_jumpclip);
            }
            if(x > 0){
                _rigidbody2D.transform.eulerAngles = new Vector3 (0f,0f,0f);
                _animator.SetBool("walk", true);
            }
            if(x < 0){
                _rigidbody2D.transform.eulerAngles = new Vector3 (0f,180f,0f);
                _animator.SetBool("walk", true);
            }
            if(x < 0.001f && x > -0.001f){
                _animator.SetBool("walk", false);
            }

            Walk();
        }
        else{
            deathProcudure();
        }
    }

    private void FixedUpdate() {
        if(jumpRequest && onGround){
            _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
            onGround = false;
        
        }
    }
    
    private void OnDrawGizmos() {
        if(onGround){
            Gizmos.color = Color.red;
        }
        else{
            Gizmos.color = Color.green;
        }
        Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize * 0.5f);
        Gizmos.DrawWireCube(boxCenter,boxSize);
    }

    private void Walk(){
        Vector3 movement = new Vector3(x,y,0);
        _rigidbody2D.transform.position += movement * speed * Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D coll) {
        // coll is the third object that interact with this object
        // contact from above
        if (coll.contacts[0].normal.y < 0){
            
        }
        // contact from beolow
        else if(coll.contacts[0].normal.y > 0){
            // check if the player is on the ground
            if(coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Block" || coll.gameObject.tag == "Pipe"){
                onGround = true;
                _animator.SetBool("jump", false);
            }
        }
        // contact from left
        else if (coll.contacts[0].normal.x < 0){
            
        }
        // contatc from right
        else if (coll.contacts[0].normal.x > 0){
            
        }

        if(System.Math.Round(coll.contacts[0].normal.x) < 0 || System.Math.Round(coll.contacts[0].normal.x) > 0){ 
            if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Turtle"){
                Debug.Log("Player.cs: Player hits Enemy or turtle and died.");
                Debug.Log("Triggle x value(x < 0 or x > 0): " + coll.contacts[0].normal.x);
                Debug.Log("After Rounding: " + System.Math.Round(coll.contacts[0].normal.x));
                setGameover();
            }
        }
    }

    public void setOnGroundF(){
        onGround = false;
    }

    public void setOnGroundT(){
        onGround = true;
    }

    public bool getGameOver(){
        return Gameover;
    }

    public void setGameover(){
        Gameover = true;
    }

    private void deathProcudure(){
        if(!deadMark && getGameOver()){
            Debug.Log("Enter death procedure");
            deadMark = true;
            bgmControl = GameObject.FindWithTag ("Controller");
            // Time.timeScale = 0;
            bgmControl.GetComponent<GameRules>().setGamePause();

            // Pre-assign variable bgmControl is not applicable
            // This line allows program searching through the
            // existing gameobj lista to find the right object
            bgmControl.GetComponent<GameRules>().StopMsc();

            _animator.SetBool("die", true);
            SoundController.PlayOneShot(_deadclip);
            SoundController.PlayOneShot(_gameover);
            Invoke("deathMovement", 1.1f);
        }
        else{

        }
        
    }

    private void deathMovement(){
        Destroy(gameObject.GetComponent<Collider2D>(), 0f);
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}
