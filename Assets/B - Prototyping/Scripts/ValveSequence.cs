using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzleGame.ValveSequencePuzzle
{
    public class ValveSequence : MonoBehaviour
    {
        public GameObject valve1, valve2, valve3;
        public bool lock1, lock2, lock3;
        public Animator valve1Anim, valve2Anim, valve3Anim;
        public Animator doorAnim;
        public bool SequenceCorrect = false;

        // Start is called before the first frame update
        void Start()
        {
            valve1 = GameObject.Find("Valves/Valve");
            valve2 = GameObject.Find("Valves/Valve (1)");
            valve3 = GameObject.Find("Valves/Valve (2)");
        }

        // Update is called once per frame
        void Update()
        {
            
            if (lock1 == true && lock2 == true && lock3 == true) // if all is true
            {
                SequenceCorrect = true; // set sequence correct to true
                if (SequenceCorrect)    // if true
                {
                    doorAnim.SetBool("Correct", true); // play door animation
                }
                
            }
        }
        
        private void OnTriggerStay2D(Collider2D _valve)
        {
            if (_valve.gameObject.CompareTag("Valve") && Input.GetKeyDown(KeyCode.E)) // Detects if the object is marked with the Tag Valve while also needing an input of the Key "E"
            {
                if (_valve.gameObject.name == "Sequence1") // if the game object name is "Sequence1"
                {
                    lock1 = valve1;                 // Sets lock1 to be valve
                    lock1 = true;                   // sets lock1 to true
                    valve1Anim.SetTrigger("Turn");  // plays animation
                }
                if (_valve.gameObject.name == "Sequence2" && lock1 == true) // if the game object name is "Sequence1" and if lock 1 is already true
                {
                    lock2 = valve2;                 // Sets lock2 to be valve
                    lock2 = true;                   // sets lock2 to true
                    valve2Anim.SetTrigger("Turn");  // plays animation
                }
                if (_valve.gameObject.name == "Sequence3" && lock1 == true && lock2 == true)// if the game object name is "Sequence1" and if lock 1 and 2 is already true
                {
                    lock3 = valve3;                 // Sets lock3 to be valve
                    lock3 = true;                   // sets lock3 to true
                    valve3Anim.SetTrigger("Turn");  // plays animation
                }
                //switch (_valve.gameObject.name)
                //{
                //    case "Sequence1":
                //        lock1 = valve1;
                //        lock1 = true;
                //        valve1Anim.SetTrigger("Turn");
                //        break;
                //    case "Sequence2":
                //        lock2 = valve2;
                //        lock2 = true;
                //        valve2Anim.SetTrigger("Turn");
                //        break;
                //    case "Sequence3":
                //        lock3 = valve3;
                //        lock3 = true;
                //        valve3Anim.SetTrigger("Turn");
                //        break;
                //}

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
             
             if (collision.gameObject.CompareTag("Exit"))       //Detects if the player has entered the trigger zone marked with the tag "Exit"
             {
                Debug.Log("Player Detected");                   //Quick debug for Testing purposes 
                if (SequenceCorrect)                            //If the sequence is correct 
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);                  // Loads the next scene acording to the build index
                }
             }
                
        }
        
    }
}

