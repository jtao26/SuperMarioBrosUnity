using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockHit : MonoBehaviour
{
    public GameObject PuzzleBoxHitted;
    public GameObject Coin;
    BoxCollider2D _boxCollider2D;
    Vector3 target, original, coinTarget;
    private bool Hitable = true;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        original = transform.position;
        target = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        coinTarget = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        // coll is the third object that interact with this object
        // contact from left
        Debug.Log("Detect!");
        if (coll.contacts[0].normal.y == 1){
            if(coll.gameObject.tag == "Player" && Hitable == true){
                // play animation once
                Debug.Log("Hit!");
                
                hittedUp();
                Invoke("returnBack", 0.2f);
            }
        }
        else{
            Debug.Log("Block contact not from bottom");
        }
         
    }

    private void hittedUp(){
        transform.DOMove(target, 0.2f);
        if(gameObject.tag == "QBlock"){
            jumpACoin();
            Hitable = false;
        }
    }

    private void returnBack(){
        transform.DOMove(original, 0.1f);
        if(gameObject.tag == "QBlock")
            Invoke("removeBlocks", 0.1f);
    }
    private void removeBlocks(){
        Instantiate(PuzzleBoxHitted, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void jumpACoin(){
        Instantiate(Coin, original, Quaternion.identity);
    }
    
}
