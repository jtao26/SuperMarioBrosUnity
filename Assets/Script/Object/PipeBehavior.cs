using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PipeBehavior : MonoBehaviour
{
    public GameObject GameController;
    private Vector3 Inside, Horizon;
    public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D coll) {
        if (coll.contacts[0].normal.y == -1 && coll.gameObject.tag == "Player"){
            Debug.Log("Something contact Pipe from above");
            Debug.Log("It's player, what would he do?");
            if(Input.GetKey(KeyCode.S)){
                Debug.Log("Pushing S");
                // pause the game
                GameController.GetComponent<GameRules>().setGamePause();
                //GameObject.Find("GameController").GetComponent<GameRules>().setGamePause();
                // remove pipe collosion
                Destroy(coll.gameObject.GetComponent<BoxCollider2D>(), 0f);
                Destroy(coll.gameObject.GetComponent<EdgeCollider2D>(), 0f);
                // let player character moving inside the pipe
                if(coll.gameObject.transform.position.x != gameObject.transform.position.x){
                    Horizon = new Vector3(gameObject.transform.position.x, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
                    coll.gameObject.transform.DOMove(Horizon, 0.2f);
                    
                    Invoke("EnterPipe", 0.2f);
                }
                // switch scece

            }
            else{
                // do-nothing
            }
        }
        else{
            // do-nothing
        }
    }

    private void EnterPipe(){
        Inside = new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y - 1f, PlayerObj.transform.position.z);
        PlayerObj.transform.DOMove(Inside, 1f);
    }

}
