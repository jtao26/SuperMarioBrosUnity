using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinBehavior : MonoBehaviour
{
    private Vector3 coinTarget;
    // Start is called before the first frame update
    void Start()
    {
        coinTarget = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        transform.DOMove(coinTarget, 0.35f);
        Invoke("removeCoinImag", 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void removeCoinImag(){
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Invoke("removeCoin", 0.65f);
    }

    private void removeCoin(){
        Destroy(gameObject);
    }
}
