using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStatus : MonoBehaviour
{
    public float lowJumpGravity = 3.5f;
    public float fallGravity = 4f;
    private Rigidbody2D _rigidbpdy2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbpdy2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // performance might be different when using different hardware
    // using fixedupdate to avoid performance differences
    void FixedUpdate()
    {
        if(_rigidbpdy2D.velocity.y < 0){
            _rigidbpdy2D.gravityScale = fallGravity;
        }
        else if(_rigidbpdy2D.velocity.y > 0 && !Input.GetButton("Jump")){
            _rigidbpdy2D.gravityScale = lowJumpGravity;
        }
        else{
            _rigidbpdy2D.gravityScale = 1f;
        }
    }
}
