using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzleGame.OutOfBounds
{
    public class OutOfBounds : MonoBehaviour
    {
        public string levelName;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(levelName);
            }
        }

    }
}

