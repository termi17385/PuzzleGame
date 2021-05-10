using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzleGame.Door
{
    public class NextLevel : MonoBehaviour
    {
        public Animator doorAnim;
        public GameObject door;
        public bool doorOpen;
        
        private void OnTriggerEnter2D(Collider2D _playerInRadius)
        {
            if (_playerInRadius.gameObject.CompareTag("Player"))
            {
                doorAnim.SetBool("Correct", true);
                doorOpen = true;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}

