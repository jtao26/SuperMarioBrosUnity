using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private int dir = 1;
    private bool gamePause = false;
    private GameObject GameController;
    private AudioSource SoundController;

    public AudioClip _stepclip;
    public AudioClip _kickclip;
    private bool deadSound = false;
    [Range(0,15)] public float bounceVelocity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        SoundController = gameObject.GetComponent<AudioSource>();
        _animator.SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        GameController = GameObject.FindWithTag ("Controller");
        gamePause = GameController.GetComponent<GameRules>().getGameTimeStatus();
        if( gamePause == false ){
            // game not pause
            if(gameObject.tag == "Enemy") {
                if(_animator.GetBool("die") == false ){
                    Walk(dir);
                }
                else{
                    if(deadSound == false){
                        SoundController.PlayOneShot(_stepclip);
                        deadSound = true;
                    }
                }
            }


        }
        else{
            // game pause
            // update animator
            _animator.SetBool("walk", false);
        }
        
    }

    void Walk(float dir) {
        transform.position = new Vector3(transform.position.x + (dir * -1 * Time.deltaTime * 0.7f), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        // coll is the third object that interact with this object

        // contatc the right left side of the object
        if (System.Math.Round(coll.contacts[0].normal.x) < 0 || System.Math.Round(coll.contacts[0].normal.x) > 0){
            if(coll.gameObject.tag == "Pipe" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Turtle"){
                // change direction
                dir = dir * (-1);
            }
            if(coll.gameObject.tag == "Shell"){
                if(coll.gameObject.GetComponent<ShellBehavior>().getShellSpd() != 0f)
                    KnockOff_by_Shell();
                else
                    dir = dir * (-1);
            }
        }
        if (coll.contacts[0].normal.y == -1){   
            // contacted the bottom of the third object

        }
        if(coll.gameObject.tag == "Player" && coll.contacts[0].normal.y < 0){
                Debug.Log("Enemy is stepped on");
                // Bounce Mario back
                coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceVelocity, ForceMode2D.Impulse);
                // Delete enemy object
                gameObject.GetComponent<Animator>().SetBool("die", true);
                Destroy(gameObject.GetComponent<Collider2D>(), 0f);
                Destroy(gameObject.GetComponent<Rigidbody2D>(), 0f);
                Destroy(gameObject, 0.5f);
            }
            
    }

    private void KnockOff_by_Shell(){
        // knock out audio
        SoundController.PlayOneShot(_kickclip);
        // stop moving
        gameObject.GetComponent<Animator>().SetBool("walk", false);
        // adding a force to indicate knocked out
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        // flip the imag upside down
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
        // remove collider to prevent collision
        Destroy(gameObject.GetComponent<Collider2D>(), 0f);
        // Destroy entity after 3s
        Destroy(gameObject, 3f);
    }
}
