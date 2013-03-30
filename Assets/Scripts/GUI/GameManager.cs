using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]

public class GameManager: MonoBehaviour {

    public static float activeBacteriaCount;
    public float totalBacteriaCount;
    public float totalBacteriaKilled;

    GameObject[] allBacteria;

    public static int gamescore = 0;

    public Texture2D infectionbar = null;
    public float changesize = 5f;
    public Rect infectbarpos;//

    public static int level;
    int currentlevel;
    // Countdown Timer variables
    public static float startTime;
    private float remainingSeconds;
    private int roundedRestSeconds;
    private int displaySeconds;
    private int displayMinutes;
    float countDownSeconds = 60;
   // public Texture2D bcellAvatar;
   // public Rect bcellavatarpos;//
    public Transform  myTransform;
    GameObject level1objects;
    GameObject tutorialLevelobjects;

    public Font MyFont;
    public GUIStyle scoreGuiStyle;
    string text;

	// Use this for initialization

    void Awake()
    {
       

        level = 0;
        currentlevel = 0;

         level1objects = GameObject.FindGameObjectWithTag("level1_enemyspawner");
         tutorialLevelobjects = GameObject.FindGameObjectWithTag("level0_activator");
    }
	void Start () {

        BacteriaCount();
        infectbarpos = new Rect(1000, 30, 100, 20);
       // bcellavatarpos = new Rect(10, 500, 100, 20);
	}
	
	// Update is called once per frame
	void Update () {

        
        BacteriaCount();
      //  print("BacteriaCount: " + activeBacteriaCount);

        if (activeBacteriaCount < 0)
        {
            activeBacteriaCount = 0;
        }

        if (level == 0)
        {
            
            ActivateGameObjects(level1objects,false);
            ActivateGameObjects(tutorialLevelobjects, true);
        }
        else if (level == 1)
        {
            currentlevel = level;
            ActivateGameObjects(tutorialLevelobjects, false);
            ActivateGameObjects(level1objects, true);
            
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

        if (currentlevel ==1)
        {
           
            _DisplayTime();
        }
       
    }

    void displayScore()
    {
        GUI.skin.font = MyFont;
        GUI.Label(new Rect(5, 5, 100, 20), "Score: " + gamescore.ToString(), scoreGuiStyle);
       // GUI.Label(new Rect(80, 5, 100, 20), gamescore.ToString(), scoreGuiStyle);
        

    }
    //delete this function
    void displayTime()
    {
        //GUI.Label(new Rect(Screen.width / 2, 5, 50, 50), "" + Time.timeSinceLevelLoad);
        double rounded = Mathf.Round(Time.timeSinceLevelLoad * 100) / 100;
        GUI.Label(new Rect(300, 5, 50, 50), "" + rounded);
    }

    void displayPlayerHealth()
    {
        
        GUI.Label(new Rect(5, 50, 100, 20), "Health ", scoreGuiStyle);
        GUI.Label(new Rect(5, 67, 100, 20), UnitPlayer.health.ToString());

        if (UnitPlayer.health < 1 || activeBacteriaCount >30)
        { //gameover
            UnitPlayer.health = 0;
            GUI.Label(new Rect(5, 150, 100, 20), "Gameover! ");
            Screen.lockCursor = false;
            Application.LoadLevel(2);
            UnitPlayer.health = 100;
        }
    }

    void displayInfectionLevel()
    {
        
        GUI.Label(new Rect(1000, 5, 100, 20), "Infection Level: " );
        GUI.Label(new Rect(1100, 5, 100, 20), activeBacteriaCount.ToString());
    
    }


    void _DisplayTime()
    {

        
    float guiTime = Time.time - startTime;
 
    remainingSeconds = countDownSeconds - guiTime;
 
 
    if (remainingSeconds == 59) {
        print ("59 Seconds..");
    }
    if (remainingSeconds == 0) {
        print ("Time is Over");

    } 
 
    roundedRestSeconds = Mathf.CeilToInt(remainingSeconds);
    displaySeconds = roundedRestSeconds % 60;
    displayMinutes = roundedRestSeconds / 60; 
 
     text = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds);
    GUI.Label(new Rect(Screen.width / 2, 5, 50, 50), text);

        
    }

    // Activate or Deactivate objects when as needed.
    void ActivateGameObjects(GameObject spawner, bool activate)
    {

     //int i;
     //for (i = 0; i < spawner.transform.childCount; ++i)

   // {
      //  spawner.transform.GetChild(i).gameObject.SetActive(activate);
   // }
     spawner.transform.gameObject.SetActive(activate);


    }


    //Keeps track of Bacteria count for determining infection level
    void BacteriaCount()
    {
       
        activeBacteriaCount = GameObject.FindGameObjectsWithTag("enemy1").Length
        + GameObject.FindGameObjectsWithTag("enemy2").Length
        + GameObject.FindGameObjectsWithTag("enemy3").Length
        + GameObject.FindGameObjectsWithTag("enemy4").Length;
    }
  

}
