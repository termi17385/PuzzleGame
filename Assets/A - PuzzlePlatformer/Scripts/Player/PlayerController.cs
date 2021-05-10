using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleGame.Player.Animations;
using Sirenix.OdinInspector;

/* ChangeLog and ErrorLog
 *  Debugging ui for displaying stamina and speed changes
 *  Added stamina and jumping 
 *  need to get animations working
 *  fixed stamina
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
        [System.Serializable]
        public struct IMGUI
        {
            [Title("Position")]
            [FoldoutGroup("Size And Position")]
            public float x;
            [FoldoutGroup("Size And Position")]
            public float y;
            [Space]
            [Title("Size")]
            [FoldoutGroup("Size And Position")]
            public float width;
            [FoldoutGroup("Size And Position")]
            public float height;
        }
        #endregion
        #region Variables
        [Title("Movement")]
        private float _moveSpeed;
        [SerializeField, FoldoutGroup("Variables")] private float speed;
        [SerializeField, FoldoutGroup("Variables")] private float jumpHeight;
        [FoldoutGroup("Variables")] public bool isGrounded = false;
        [FoldoutGroup("Variables")] public bool canSprint = false;
        [SerializeField] private float time;

        [Title("Stamina")]
        [SerializeField, FoldoutGroup("Variables")] private float stamina;
        [SerializeField, FoldoutGroup("Variables")] private float maxStamina = 100;
        [SerializeField, FoldoutGroup("Variables")] private float staminaDepletionAmt;

        [Title("Components")]
        [SerializeField, FoldoutGroup("Components")] private SpriteRenderer sr;
        [SerializeField, FoldoutGroup("Components")] private Rigidbody2D rb; 

        [Title("Struct")]
        [SerializeField] private IMGUI[] guiSizeAndPos;
        #endregion
        #region Start and Update
        void Start()
        {
            Stamina = 100;
            rb = GetComponent<Rigidbody2D>();
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            PlayerMovement();
        }
        #endregion
        #region PlayerMovement
        protected void PlayerMovement()
        {
            #region Variables
            // move the player left or right using joystick and keyboard
            float x = Input.GetAxis("Horizontal");
            float moveSpeed = (x * _moveSpeed); // sets the speed if the player
            
            // bool for if jump is held to give the player a little boost
            bool jump = ((Input.GetKeyDown(KeyCode.Space) 
            || Input.GetKeyDown(KeyCode.JoystickButton1))&&isGrounded); 
            
            // bool for sprinting
            bool sprint = (Input.GetKey(KeyCode.LeftShift) 
            || Input.GetKey(KeyCode.JoystickButton2));
            #endregion
            #region Walking
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // actually moves the player
            // flip sprite in direction that the player moves in
            if(x < 0f) sr.flipX = true;
            if(x > 0f) sr.flipX = false;
            #endregion
            #region Sprinting
            if (sprint && stamina > 0 && canSprint == true)
            {
                if(x<0||x>0)StaminaDepletion(staminaDepletionAmt);
                if(Stamina == 0) canSprint = false;
                _moveSpeed = 8.5f;
            }
            else
            {
                _moveSpeed = speed;
                if(Stamina == 100)canSprint = true;
                StaminaRecovery(2.5f);
            }
            
            #endregion
            #region Jumping
            if (jump)
            {
                if(sr.flipY == true)  rb.velocity = new Vector2(rb.velocity.x, -jumpHeight);
                else rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
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
        #region Triggers
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "Ground")
            { 
                isGrounded = true;
            }  
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = false;
            }
        }
        #endregion
        #endregion
        
        #region Debug
        private void OnGUI()
        {
            // keeps everything scaled to the native size
            Vector2 nativeSize = new Vector2(1920, 1080);                                          // used to set the native size of the image
            Vector3 scale = new Vector3(Screen.width / nativeSize.x, Screen.height / nativeSize.y, 1.0f);   // gets the scale of the screen
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);                   // sets the matrix and scales the GUI accordingly
            
            float _x = Mathf.RoundToInt(Stamina);
            string text = string.Format(" Stamina: {0}\n speed: {1}", _x, _moveSpeed);

            #region Styling
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 18;
            #endregion
            #region Positioning 
            float heightOne = -GravityChanger.instance.gravityCoolDown;
            //float hieghtTwo =;
            #endregion

            GUI.BeginGroup(new Rect(guiSizeAndPos[0].x, guiSizeAndPos[0].y, guiSizeAndPos[0].width, guiSizeAndPos[0].height));
            #region Backgrounds and text
            GUI.Box(new Rect(guiSizeAndPos[1].x, guiSizeAndPos[1].y, guiSizeAndPos[1].width, guiSizeAndPos[1].height), "");
            GUI.Box(new Rect(guiSizeAndPos[2].x, guiSizeAndPos[2].y, guiSizeAndPos[2].width, guiSizeAndPos[2].height), "");
            GUI.Box(new Rect(guiSizeAndPos[3].x, guiSizeAndPos[3].y, guiSizeAndPos[3].width, heightOne), "");
            #region Text
            GUI.Box(new Rect(0, 0, 110, 100), "Gravity", style);
            GUI.Box(new Rect(389.24f, 0, 110, 100), "Time\nManipulation", style);
            #endregion
            #endregion

            GUI.Box(new Rect(guiSizeAndPos[4].x, guiSizeAndPos[4].y, guiSizeAndPos[4].width, guiSizeAndPos[4].height),"");
            GUI.Label(new Rect(guiSizeAndPos[5].x, guiSizeAndPos[5].y, guiSizeAndPos[5].width, guiSizeAndPos[5].height), "Stamina : " + _x, style);
            GUI.EndGroup();
        }
        #endregion
    }
}
