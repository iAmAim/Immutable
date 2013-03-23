using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]

public class GameHud : MonoBehaviour {


    public static int gamescore = 0;
	// Use this for initialization
	void Start () {

      
	
	}
	
	// Update is called once per frame
	void Update () {

       // Debug.Log("Gamescore is " + gamescore);
       
	}

    void OnGUI()
    {
        displayScore();
        displayTime();
        displayPlayerHealth();
        displayInfectionLevel();
      

    }

    void displayScore()
    {
        GUI.Label(new Rect(5, 5, 100, 20), "Score " );
        GUI.Label(new Rect(5, 22, 100, 20), gamescore.ToString());
    }

    void displayTime()
    {
        GUI.Label(new Rect(Screen.width / 2, 5, 50, 50), "" + Time.timeSinceLevelLoad);
        double rounded = Mathf.Round(Time.timeSinceLevelLoad * 100) / 100;
        GUI.Label(new Rect(300, 5, 50, 50), "" + rounded);
    }

    void displayPlayerHealth()
    {
        GUI.Label(new Rect(5, 50, 100, 20), "Health ");
        GUI.Label(new Rect(5, 67, 100, 20), UnitPlayer.DisplayHealth().ToString());

        if (UnitPlayer.DisplayHealth() < 1)
        { //gameover
            GUI.Label(new Rect(5, 150, 100, 20), "Gameover! ");
        }
    }

    void displayInfectionLevel()
    {
        
    }

}
