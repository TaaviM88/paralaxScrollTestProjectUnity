using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class basic2DMovement : MonoBehaviour
{
    public float moveSpeed = 2;
    float horizontalMovement;
    float verticalMovement;
    Rigidbody2D rb2D;
    public int side = 1;
    SpriteRenderer sprite;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove)
        {
            return;
        }
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        if(horizontalMovement != 0)
        {
            if(horizontalMovement > 0)
            {
                side = 1;
                sprite.flipX = false;
            }

            if(horizontalMovement < 0)
            {
                side = -1;
                sprite.flipX = true;
            }
        }
        //transform.position = new Vector3(transform.position.x + horizontalMovement * moveSpeed, transform.position.y + verticalMovement * moveSpeed, transform.position.z);
        
    }

    private void FixedUpdate()
    {
        if(horizontalMovement  == 0 && verticalMovement == 0)
        {
            rb2D.velocity = Vector2.zero;
        }
        else
        {
            rb2D.velocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        }
    }

    public void FreezeMovement()
    {
        canMove = false;
        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void StartPlayerMovement()
    {
        canMove = true;
        rb2D.constraints = RigidbodyConstraints2D.None;
        transform.localEulerAngles = new Vector3(0,0,0);
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
