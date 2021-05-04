using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace PuzzleGame.Obstacle
{
    public class Obstacles : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Obstacle")     // If Collision with an object marked with the "obstacle" tag 
            {
                SceneManager.LoadScene(2);      //Loads the third scene in the build index (starts from 0)
            }
        }
    }
}

