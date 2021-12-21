using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMove : MonoBehaviour
{
    private Animator _animator;
    private int dir = 1;
    private bool gamePause = false;
    private Vector3 shellPos;
    private GameObject GameController;
    public GameObject ShellObj;
    public GameObject Player;
    [Range(0,15)] public float bounceVelocity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        GameController = GameObject.FindWithTag ("Controller");
        gamePause = GameController.GetComponent<GameRules>().getGameTimeStatus();
        if( gamePause == false ){
            // game not pause
            Debug.Log("Turtle_is_moving");
            _animator.SetBool("walk", true);
            Walk(dir);

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
            // contact from left
        if (System.Math.Round(coll.contacts[0].normal.x) < 0 || System.Math.Round(coll.contacts[0].normal.x) > 0){
            if(coll.gameObject.tag == "Pipe" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Turtle"){
                // change direction
                dir = dir * (-1);
                // flip imag 
                gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            }
        }
            // contact from above
            // Enemy stepped by Player
        if(coll.gameObject.tag == "Player" && coll.contacts[0].normal.y < 0 ){
            Debug.Log("Turtle is stepped on, turning to shell");
            Debug.Log("Triggle y value(y < 0): " + coll.contacts[0].normal.y);
            Debug.Log("After Rounding: " + System.Math.Round(coll.contacts[0].normal.y));
            shellPos = gameObject.transform.position;
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceVelocity, ForceMode2D.Impulse);
            Player.GetComponent<Player>().setOnGroundF();
            Destroy(gameObject);
            Instantiate(ShellObj,shellPos,Quaternion.identity);
            }
    }
}
