using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

   
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
            Application.LoadLevel(1);
           
        }
    }
}
