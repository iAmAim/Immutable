using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

   
    //Change color of text on mouse enter
    void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }

    //Revert back to original
    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    // menu options
    void OnMouseDown()
    {
        if (transform.tag == "quit")
        {
            Application.Quit();
        }
        else
        {
            Screen.lockCursor = true;
            Application.LoadLevel(1);
           
        }
    }
}
