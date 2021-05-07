using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzleGame.ValveSequencePuzzle
{
    public class ValveSequence : MonoBehaviour
    {
        [SerializeField] private GameObject valve1, valve2, valve3;
        public Animator anim;
        public Animator doorAnim;
        public GameObject door;
        public bool SequenceCorrect = true;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (SequenceCorrect)
            {
                doorAnim.SetBool("Correct", true);
            }
        }
        public void TurnValve()
        {
            if (true)
            {

            }
        }
        private void OnTriggerEnter2D(Collider2D _valve)
        {
            if (_valve.gameObject.CompareTag("Valve"))
            {
                anim.SetTrigger("Turn");
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
             
             if (collision.gameObject.CompareTag("Exit"))       //Detects if the player has entered the tigger zone marked with the tag "Exit"
             {
                Debug.Log("Player Detected");                   //Quick debug for Testing purposes 
                if (SequenceCorrect)                            //If the sequence is correct 
                {
                    SceneManager.LoadScene(2);                  // Load the following scene
                }
             }
                
        }
        
    }
}

