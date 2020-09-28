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
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        //transform.position = new Vector3(transform.position.x + horizontalMovement * moveSpeed, transform.position.y + verticalMovement * moveSpeed, transform.position.z);
    }

    private void FixedUpdate()
    {
        if(horizontalMovement  == 0 && verticalMovement == 0)
        {
            rb2D.velocity = Vector2.zero;
        }
        else
        rb2D.velocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
    }
}
