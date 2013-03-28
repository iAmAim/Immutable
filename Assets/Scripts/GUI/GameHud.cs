using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]

public class GameHud : MonoBehaviour {

    public static float activeBacteriaCount;
    public float totalBacteriaCount;
    public float totalBacteriaKilled;

    GameObject[] allBacteria;

    public static int gamescore = 0;

    public Texture2D infectionbar = null;
    public float changesize = 5f;
    public Rect infectbarpos;//


   // public Texture2D bcellAvatar;
   // public Rect bcellavatarpos;//

	// Use this for initialization

    void Awake()
    {
        activeBacteriaCount = GameObject.FindGameObjectsWithTag("enemy1").Length
          + GameObject.FindGameObjectsWithTag("enemy2").Length
          + GameObject.FindGameObjectsWithTag("enemy3").Length
          + GameObject.FindGameObjectsWithTag("enemy4").Length;

    }
	void Start () {


        infectbarpos = new Rect(1000, 30, 100, 20);
       // bcellavatarpos = new Rect(10, 500, 100, 20);
	}
	
	// Update is called once per frame
	void Update () {

       

        if (activeBacteriaCount < 0)
        {
            activeBacteriaCount = 0;
        }


	}

    void OnGUI()
    {
        displayScore();
        displayTime();
        displayPlayerHealth();
        displayInfectionLevel();

        if (infectionbar != null)
        {
            infectbarpos.width = activeBacteriaCount * changesize;
            infectbarpos.height = 15f;
            GUI.DrawTexture(infectbarpos, infectionbar);
        }

        // Avatar
       // GUI.DrawTexture(bcellavatarpos, bcellAvatar);
        //bcellAvatar.width = 150;
       // bcellAvatar.height = 150;
      //  GUI.DrawTexture(bcellavatarpos, bcellAvatar, ScaleMode.ScaleToFit, true, 0);

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
        GUI.Label(new Rect(5, 67, 100, 20), UnitPlayer.health.ToString());

        if (UnitPlayer.health < 1)
        { //gameover
            UnitPlayer.health = 0;
            GUI.Label(new Rect(5, 150, 100, 20), "Gameover! ");
        }
    }

    void displayInfectionLevel()
    {
        
        GUI.Label(new Rect(1000, 5, 100, 20), "Infection Level: " );
        GUI.Label(new Rect(1100, 5, 100, 20), activeBacteriaCount.ToString());
    
    }

}
