using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PuzzleGame.TimeManipulator
{
    public class TimeManipulator : MonoBehaviour
    {
        #region Variables
        [Header("Background")]
        public GameObject bgPast;
        public GameObject bgPresent;
        [Header("Base")]
        public GameObject basePast;
        public GameObject basePresent;
        [Header("Grounds")]
        public GameObject groundPast;
        public GameObject groundPresent;
        [Header("Boundry")]
        public GameObject boundryPast;
        public GameObject boundryPresent;
        [Header("Foregorund")]
        public GameObject foregorundPast;
        public GameObject foregorundPresent;
        [Header("Screen Flash")]
        public Image timeFlash;
        [SerializeField] private float flashSpeed;
        #endregion



        // Update is called once per frame
        void Update()
        {
            StartCoroutine(ToThePast());        // Calls the method to go to the past
            StartCoroutine(BackToTheFuture());  // Calls the method to go to the future
        }
        #region Past Time Travel
        private IEnumerator ToThePast()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) // when left click is pressed
            {
                bgPresent.SetActive(false);                     //Disable the background
                basePresent.SetActive(false);                   //Disable the base layer
                groundPresent.SetActive(false);                 //Disable the ground
                boundryPresent.SetActive(false);                //Disable the boundry
                foregorundPresent.SetActive(false);             //Disable the foreground

                bgPast.SetActive(true);                         //enable the Background
                basePast.SetActive(true);                       //enable the Base layer
                groundPast.SetActive(true);                     //enable the ground
                boundryPast.SetActive(true);                    //enable the boundry
                foregorundPast.SetActive(true);                 //enable the foreground

                timeFlash.gameObject.SetActive(true);           //Sets the Gameobject to true
                yield return new WaitForSeconds(flashSpeed);    //Has the game wait till the next line of code is ran 
                timeFlash.gameObject.SetActive(false);          //Sets the Gameobject to false
            }
        }
        #endregion
        #region Present Time Travel
        private IEnumerator BackToTheFuture()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))// when right click is pressed 
            {
                bgPresent.SetActive(true);                 //enable the Background
                basePresent.SetActive(true);               //enable the Base layer
                groundPresent.SetActive(true);             //enable the ground
                boundryPresent.SetActive(true);            //enable the boundry
                foregorundPresent.SetActive(true);         //enable the foreground

                bgPast.SetActive(false);                   //Disable the background
                basePast.SetActive(false);                 //Disable the base layer
                groundPast.SetActive(false);               //Disable the ground
                boundryPast.SetActive(false);              //Disable the boundry
                foregorundPast.SetActive(false);           //Disable the foreground

                timeFlash.gameObject.SetActive(true);      //Sets the Gameobject to true
                yield return new WaitForSeconds(flashSpeed);    //Has the game wait till the next line of code is ran 
                timeFlash.gameObject.SetActive(false);     //Sets the Gameobject to false
            }
        }
        #endregion
    }
}

