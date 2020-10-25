using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    private enum State
    {
        ground,
        jump,
        attack,
    }

    public float moveSpeed = 1;

    [SerializeField]    
    private AnimationCurve jumpSpeedCurve;

    Rigidbody2D rb2d;
    float jumpheight;
    float jumpTime;

    bool onGround;
    bool hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && onGround)
        {
            StartJump();
          
        }


        if(Input.GetButtonUp("Fire2"))
        {
            hasJumped = false;
        }

        if(hasJumped)
        {
            DoJump();
        }

    }


    #region Jump hommat

    public void StartJump()

    {   if(!hasJumped)
        {
            jumpheight = jumpSpeedCurve.Evaluate(0);
            jumpTime = 0f;
            hasJumped = true;
        }
    }

    public void DoJump()
    {   
        if(hasJumped)
        {
            jumpTime += Time.deltaTime;
            jumpheight = jumpSpeedCurve.Evaluate(jumpTime);

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpheight);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            Debug.Log($"Osuttiin maahan {collision.gameObject.name}");

            onGround = true;
            hasJumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log($"Ei osuta maahan");

            onGround = false;
        }
    }
    #endregion
}
