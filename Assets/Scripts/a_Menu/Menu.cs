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
        else if (transform.tag =="nextlevel")
        {
            Application.LoadLevel(GameManager.level+1);
        }
        else if (transform.tag == "mainmenu")
        {
            Application.LoadLevel(0);
        }
        else
        {
            Application.LoadLevel(1);
           
        }
    }
}
