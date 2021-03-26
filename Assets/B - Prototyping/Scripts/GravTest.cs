using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravTest : MonoBehaviour
{
    private float moveSpeed = 10f;
    private Rigidbody2D rb2d;

    private SpriteRenderer pSprite;
    private float grav = 2;
    private float negGrav = -2;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pSprite = GetComponent<SpriteRenderer>();
        rb2d.gravityScale = grav;
    }

    private void Update()
    {
        Move();
        GravInverter();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        rb2d.velocity = new Vector2(x * moveSpeed, rb2d.velocity.y);
        if (x >= 1){pSprite.flipX = false;}
        if (x <= -1){pSprite.flipX = true;}
    }

    private void GravInverter()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(rb2d.gravityScale == grav){rb2d.gravityScale = negGrav; pSprite.flipY = true;}                               
            else {rb2d.gravityScale = grav; pSprite.flipY = false;}
        }
    }
}
