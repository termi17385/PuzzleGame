using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
/*  ChangeLog and Description
 
 *  This script is incomplete and will be given a lot of changes                
 *  need to improve movement and add sprinting while also adding
 *  some properties to improve things.
 
 +  Added sprinting and stamina to the player
 +  Added debugging to the player to show stamina being depleted or recovered
 +  Made it so stamina only drains when the player has actually moved while sprinting
 +  added a public property for detecting if the player is grounded
 
 *  ChangeLog made by - josh
 */
namespace PuzzleGame.player
{   
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimationManager))]
    [HideMonoScript]
    public class PlayerController : SerializedMonoBehaviour
    {
        #region Properties
        /// <summary>
        /// this property changes the characters speed
        /// <br/> when the player holds shift or circle 
        /// <br/> on ps4 controller 
        /// </summary>
        [ShowInInspector, TabGroup("Variables and Properties/Split/Properties", "Properties")] 
        private float Movement
        {
            get
            {
                // these are the input keys for changing speed if either of these keys are pressed
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton2)) && stamina > 0)
                {
                    return moveSpeed * 1.5f; // increases the speed by 1.5
                }
                else
                {
                    return moveSpeed; // else returns speed to default value
                }
            }
        }

        public bool IsGrounded
        {
            get => isGrounded;
        }

        #region Stamina Properties
        /// <summary>
        /// Handles getting and assigning stamina
        /// </summary>
        [ShowInInspector, TabGroup("Variables and Properties/Split/Properties", "Properties")]
        private float GetStamina
        {
            get 
            {
                if(stamina <= 0)stamina = 0;        // makes sure stamina gets reset to 0
                if(stamina >= 100)stamina = 100;    // makes sure stamina gets reset to 100

                return Mathf.Clamp01(stamina/maxStamina);
            }
        }
        /// <summary>
        /// Handles depleting stamina
        /// </summary>
        [ShowInInspector, ReadOnly, TabGroup("Variables and Properties/Split/Properties", "Debugging")]
        private float DepleteStamina
        {
            get => stamina;
            set => stamina -= value;
        }
        /// <summary>
        /// Handles recovering stamina
        /// </summary>
        [ShowInInspector, ReadOnly, TabGroup("Variables and Properties/Split/Properties", "Debugging")]
        private float RecoverStamina
        {
            get => stamina;
            set => stamina += value;
        }
        #endregion
        #endregion
        #region Variables
        private Rigidbody2D rb;
        [TitleGroup("Variables and Properties")]
        [HorizontalGroup("Variables and Properties/Split", Width = 0.5f)]
        [SerializeField, TabGroup("Variables and Properties/Split/Parameters", "MovementVars")] private float moveSpeed = 10f;
        [SerializeField, TabGroup("Variables and Properties/Split/Parameters", "MovementVars")] private float jumpHeight = 6f;

        [SerializeField, TabGroup("Variables and Properties/Split/Parameters", "StaminaVars"), ProgressBar(0, 100, r: 0, g: 1, b: 0.3f, Height = 10)] private float stamina;
        [SerializeField, TabGroup("Variables and Properties/Split/Parameters", "StaminaVars")] private float maxStamina = 100;

        [SerializeField] private PlayerAnimationManager pAnimManager;
        [SerializeField] private bool isGrounded; // used to check if we are touching the floor
        private bool doJump = false;

        private Animator anim;
        private WaitForSeconds animationDuration = new WaitForSeconds(0.4f);
        private float playerMoved;
        #endregion

        #region Methods
        #region Start and Update
        // Start is called before the first frame update
        void Start()
        { 
            rb = GetComponent<Rigidbody2D>();   // gets the rigidBody of the player
            anim = GetComponent<Animator>();    // gets the animator of the player   '
            pAnimManager = GetComponent<PlayerAnimationManager>();
            stamina = maxStamina;
        }
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
        #region Movement
        /// <summary>
        /// Handles the controls for moving the player left and right
        /// </summary>
        private void PlayerMovement()
        {
            #region Misc
            // get the movement axis for the player
            playerMoved = Input.GetAxis("Horizontal");
            
            SpriteRenderer flipDir = GetComponent<SpriteRenderer>();
            #endregion

            #region flipCharacter
            if (playerMoved < 0) flipDir.flipX = true; // flips the sprite to look right
            if (playerMoved > 0) flipDir.flipX = false; // flips the sprite to look left
            #endregion

            // move the player
            rb.velocity = new Vector2(playerMoved * Movement, rb.velocity.y);
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
                StartCoroutine(pAnimManager.JumpEffects());
                doJump = true; // lets the player jump
                
            }
        }
        #endregion
        #region GroundCheck
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                #region Misc
                bool inAir = anim.GetBool("InAir");
                #endregion

                isGrounded = true;
                anim.SetBool("Grounded", true);
                anim.SetBool("InAir", false);

                if (inAir == true) 
                    StartCoroutine(pAnimManager.LandingEffects());
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
                anim.SetBool("Grounded", false);
            }
        }
        #endregion
        #endregion
        #region Debugging
        private void OnGUI()
        {
            #region Hard code
            bool keyPressed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton2));
            bool hasMoved = (playerMoved < 0 || playerMoved > 0);

            float x = GetStamina;// variable for changing the size of the box
            #endregion

            if (keyPressed && hasMoved)DepleteStamina = 0.2f;   // if shift or circle (ps4) pressed deplete stamina
            else RecoverStamina = 0.05f;                        // recovers stamina when the player isnt running

            GUI.Box(new Rect(10 * 10, 50 * 10, 50, x * 100), "");   // display for showing how much stamina the character has
            GUI.Box(new Rect(10 * 10, 50 * 10, 50, 100), "");       // background box
        }
        #endregion

    }
}