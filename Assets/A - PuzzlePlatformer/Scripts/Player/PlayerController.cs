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
        [Title("Movement"), Tooltip("Speed is multipled by the axis (-1, 0, 1) and 10")]
        [SerializeField, FoldoutGroup("Variables")] private float speed;
        [SerializeField, FoldoutGroup("Variables")] private float jumpHeight;

        [Title("Stamina")]
        [SerializeField, FoldoutGroup("Variables")] private float stamina;
        [SerializeField, FoldoutGroup("Variables")] private float maxStamina = 100;
        [SerializeField, FoldoutGroup("Variables")] private float staminaDepletionAmt;

        private Rigidbody2D rb;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Stamina = 100;
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
             
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerMovement();
        }

        protected void PlayerMovement()
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime;
            float moveSpeed = ((x * speed) * 10);

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // flip sprite in direction that the player moves in
        }

        protected void PlayerJumping()
        {

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
