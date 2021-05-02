using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleGame.Player.Animations;
using Sirenix.OdinInspector;

/* ChangeLog and ErrorLog
 * 
 * 
 */

namespace PuzzleGame.Player
{
    [HideMonoScript]
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimationManager))]
    public class PlayerController : SerializedMonoBehaviour
    {
        #region Properties
        public float Stamina
        {
            get
            {
                if (stamina >= 100) stamina = 100;
                if (stamina <= 0) stamina = 0;

                return stamina;
            }
            set => stamina = value;
        }
        public float DepleteAmt
        {
            get => staminaDepletionAmt;
        }
        public float MaxStamina => maxStamina;
        #endregion

        #region Variables
        [Title("Movement")]
        [SerializeField, FoldoutGroup("Variables")] private float speed;
        [SerializeField, FoldoutGroup("Variables")] private float jumpHeight;


        [Title("Stamina")]
        [SerializeField, FoldoutGroup("Variables")] private float stamina;
        [SerializeField, FoldoutGroup("Variables")] private float maxStamina = 100;
        [SerializeField, FoldoutGroup("Variables")] private float staminaDepletionAmt;

        [Title("Components")]
        [SerializeField, FoldoutGroup("Components")] private SpriteRenderer sr;
        [SerializeField, FoldoutGroup("Components")] private Rigidbody2D rb;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Stamina = 100;
            rb = GetComponent<Rigidbody2D>();
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerMovement();
        }

        protected void PlayerMovement()
        {
            #region Walking
            #region Variables
            // move the player left or right using joystick and keyboard
            float x = Input.GetAxis("Horizontal");
                float moveSpeed = (x * speed); // sets the speed if the player
            
            // bool for checking if space or x is pressed
            bool jump = (Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.JoystickButton1));
            #endregion

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // actually moves the player

            // flip sprite in direction that the player moves in
            if(x < 0f) sr.flipX = true;
            if(x > 0f) sr.flipX = false;
            #endregion

            #region Jumping
            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
            #endregion
        }

        private void StaminaDepletion(float x)
        {
            Stamina -= ((x * 10) * Time.deltaTime);
        }

        private void StaminaRecovery(float x)
        {
            Stamina += ((x * 10) * Time.deltaTime);
        }
    }
}
