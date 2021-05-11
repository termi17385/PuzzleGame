using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using PuzzleGame.Player.Ladders;

public class GravityChanger : MonoBehaviour
{
    #region Variables
    public static GravityChanger instance = null;
    #region Components
    [FoldoutGroup("Components")]
    public SpriteRenderer sprite;
    [FoldoutGroup("Components")]
    [SerializeField] private Rigidbody2D rb;
    [FoldoutGroup("Components")]
    [SerializeField] private CircleCollider2D circleCol;
    [FoldoutGroup("Components")]
    [SerializeField] private LadderMovement ladder;
    #endregion

    #region General Variables
    [Title("General")]
    [SerializeField] private float depletionAmt;
    [SerializeField] private float refreshAmt;
    
    public float gravityCoolDown = 100;
    public bool canUseGrav = true;
    [Tooltip("Used this to enable gravity Script")] public bool deviceFound = false;
    #endregion
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        sprite = GetComponentInChildren<SpriteRenderer>();
        circleCol = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        ladder = GetComponent<LadderMovement>() ;

        gravityCoolDown = 100;
        canUseGrav = true;

        deviceFound = false;
}

    // Update is called once per frame
    void Update()
    {
        if(deviceFound)
        {
            if(gravityCoolDown <= 0) canUseGrav = false;

            ChangeGravity();
            GravityCoolDown();
            RefreshGravityCoolDown();
        }
        else
        rb.gravityScale = 3f;
    }

    /// <summary>
    /// Starts to refresh the cool down for the ability
    /// </summary>
    public void RefreshGravityCoolDown()
    {
        if(sprite.flipY == false)   // checks if the player has the ability disabled
        {
            gravityCoolDown += refreshAmt * Time.deltaTime; // increases the cooldown
            if (gravityCoolDown >= 100)
            {
                // enables gravity ability when cooldown reaches 100
                canUseGrav = true;
                gravityCoolDown = 100;
            }
        }
    }

    /// <summary>
    /// the cooldown for the player to stop them constantly using gravity
    /// </summary>
    public void GravityCoolDown()
    {
        if(sprite.flipY == true)
        {
            // decreases the cooldown when in use
            gravityCoolDown -= depletionAmt * Time.deltaTime;
            if(gravityCoolDown <= 0)
            {
                // disables gravity when cooldown reaches 0
                canUseGrav = false;
                sprite.flipY = false;
                gravityCoolDown = 0;
            }
        } 
    }

    /// <summary>
    /// Handles turning the ability on and off
    /// </summary>
    private void ChangeGravity()
    {
        if (ladder.ladderInUse == false)
        {
            if (Input.GetKeyDown(KeyCode.G) && canUseGrav)
            {
                // enables or disables the ability
                sprite.flipY = !sprite.flipY;
                
                // checks if the ability has been used then deactivates gravity
                if (gravityCoolDown <= 85f)
                {
                    gravityCoolDown = 0f;
                    canUseGrav = false;
                }
            }
            
            if(sprite.flipY == true)
            {
                // inverts gravity 
                rb.gravityScale = -3f;
                // moves colider into position
                circleCol.offset = new Vector2(0, 0.45f);
            }
            else if(sprite.flipY == false || canUseGrav == false) 
            {
                rb.gravityScale = 3f;
                circleCol.offset = new Vector2(0, -0.48f);
            }
        }
    }
}
