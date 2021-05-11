using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    
    public void Playgame ()
    {
        // load scene from main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quitgame ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // quits game from main menu
        Debug.Log("quit");
         Application.Quit();
        
    }
    public void ReturnToMainMenu(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }







}
