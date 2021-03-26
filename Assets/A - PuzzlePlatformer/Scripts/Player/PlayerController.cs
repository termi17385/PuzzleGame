using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/* This script is incomplete and will be given a lot of changes
 * need to improve movement and add sprinting while also adding
 * some properties to improve things. - josh
 */
namespace PuzzleGame.player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb;
        [SerializeField, Foldout("Speed and Height")] private float moveSpeed = 10f;
        [SerializeField, Foldout("Speed and Height")] private float jumpHeight = 6f;

        [SerializeField, ReadOnly] private bool isGrounded; // used to check if we are touching the floor
        private bool doJump = false;
        #endregion

        #region Start and Update
        // Start is called before the first frame update
        void Start() => rb = GetComponent<Rigidbody2D>(); // gets the rigidBody of the player

        #region Updates
        // Update is called once per frame
        void Update()
        {
            PlayerMovement();
            PlayerJump();
        }

        /// <summary>
        /// Will be used just for handling the players jumping
        /// </summary>
        private void FixedUpdate()
        {
            if (doJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                doJump = false;
            } 
        }
        #endregion
        #endregion

        /// <summary>
        /// Handles the controls for moving the player left and right
        /// </summary>
        private void PlayerMovement()
        {
            // get the movement axis for the player
            #region no peeking
            float x = Input.GetAxis("Horizontal");
            SpriteRenderer flipDir = GetComponent<SpriteRenderer>();
            #endregion
            #region flipCharacter
            if (x < 0)flipDir.flipX = true; // flips the sprite to look right
            if(x > 0)flipDir.flipX = false; // flips the sprite to look left
            #endregion

            // move the player
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        }

        /// <summary>
        /// Handles the controls for getting the player to jump
        /// </summary>
        private void PlayerJump()
        {
            // get the inputs for moving the player
            #region HardCode
            bool canJump = Input.GetButtonDown("Jump") && isGrounded;
            #endregion

            if (canJump) // checks if button it pressed
            {
                doJump = true; // lets the player jump
            }
        }

        #region GroundCheck
        private void OnTriggerEnter2D(Collider2D other){if(other.gameObject.CompareTag("Ground")) isGrounded = true;}
        private void OnTriggerExit2D(Collider2D other){if(other.gameObject.CompareTag("Ground")) isGrounded = false;}
        #endregion
    }
}