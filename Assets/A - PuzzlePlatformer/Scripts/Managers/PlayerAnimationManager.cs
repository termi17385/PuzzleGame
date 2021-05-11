using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using PuzzleGame.Prototyping;

/*  Changelog and Description

 *  this script was created for handling the animations
 *  and the effects of the player using IEnumerators
 *  functions are being called by the player controller script

 +  added animations for in air and jumping
 +  added animations for when the player lands
  
 *  created by josh
 */

namespace PuzzleGame.Player.Animations
{
    [HideMonoScript]
    public class PlayerAnimationManager : SerializedMonoBehaviour
    {
        [SerializeField] private Animator anim;
        private PlayerController pController;    

        [SerializeField, TabGroup("jumpAnim")] private float jumpDur = 0.2f;
        [SerializeField, TabGroup("jumpAnim")] private float airDur = 0.2f;

        private void Start()
        { 
            pController = GetComponent<PlayerController>();
        }
        public IEnumerator JumpEffects()
        {
            anim.SetBool("isJumping", true);
            yield return new WaitForSeconds(jumpDur);
            anim.SetBool("isJumping", false);

            yield return new WaitForSeconds(airDur);
            anim.SetBool("InAir", true);  
            
            if(pController.isGrounded == true) 
                anim.SetBool("InAir", false);
        }   

        //private IEnumerator WalkEffects()
        //{
        
        //}
        
        //private IEnumerator RunEffects()
        //{
        //
        //}
        
        public IEnumerator LandingEffects()
        {
            anim.SetBool("HasLanded", true);
            yield return new WaitForSeconds(0.4f);
            anim.SetBool("HasLanded", false);
        }
    }
}
