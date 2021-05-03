using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleGame.Prototyping;

namespace PuzzleGame.Player.Ladders
{
    public class LadderMovement : MonoBehaviour
    {
        #region Variables 
        private Rigidbody2D rb;
        public float distance;
        public LayerMask laderLayer;
        public bool ladderInUse;
        private float vertMovement;
        private float ladderClimbSpeed = 5;
        #endregion

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>(); // Sets so Rigidbody will be attached on startup
        }
        private void FixedUpdate()
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distance, laderLayer); // shoots out a vertical ray from the players position with a declared layer mask
            if (hitinfo.collider != null)
            {
                Debug.Log("Triggered Layer"); // used to test if the collider was being detected
                if (Input.GetKeyDown(KeyCode.W))
                {
                    ladderInUse = true; // sets the bool value to true 
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    ladderInUse = false;
                }
                
            }
            if (ladderInUse == true && hitinfo.collider != null)
            {
                vertMovement = Input.GetAxis("Vertical"); // gets the unity input system and assigns it to vert movement
                rb.velocity = new Vector2(rb.velocity.x, vertMovement * ladderClimbSpeed);
                rb.gravityScale = 0; // sets gravity to 0 so there is no force when climbing the ladder
            }
            else
            {
                rb.gravityScale = 1.5f; // sets gravity back to normal if the player has exited the layermask
            }
        }
    }
}

