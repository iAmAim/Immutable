using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameObject thirdperson;
    GameObject firstperson;
    GameObject character2;
    GameObject character1;
    Transform rayOrigin;
    bool isactive;
    float cooldown = 5f;
    float nextcooldown = 0;

    Vector3 thirdPersonOffset;
    Vector3 thirdPersonZoomOffset;
    Vector3 characterOffset;


   GameObject activeChar;
 
    void Awake()
    {
        thirdperson = GameObject.FindGameObjectWithTag("ThirdPersonCamera");
        firstperson = GameObject.FindGameObjectWithTag("ThirdPersonZoomCamera");
        character2 = GameObject.FindGameObjectWithTag("character2");
        character1 = GameObject.FindGameObjectWithTag("Player");
        rayOrigin = GameObject.FindGameObjectWithTag("target").transform;

        thirdPersonOffset = new Vector3(.5f, 0.70f, -1.5f);
        thirdPersonZoomOffset = new Vector3(.5f, 0.7f, -.8f);
        characterOffset = new Vector3(0, 4, 0); 
       
    }
	// Use this for initialization
	void Start () {

        activateChar(character1,character2);
	}
	
	// Update is called once per frame
	void Update () {

        // if user presses tab, character is changed
        if (Input.GetKeyDown(KeyCode.Tab) && Time.time > nextcooldown)
        {
            nextcooldown = Time.time + cooldown;
            switchPlayer();

        }
	
	}

    void switchPlayer()
    {
        if (!character1.activeInHierarchy){

           // activateCharacter1();
            activateChar(character1, character2);
          
       }
        else
        { 
            //activateCharacter2();
            activateChar(character2, character1);
          
        }
    }


    void activateChar(GameObject activeChar, GameObject inactiveChar)
    {
        inactiveChar.SetActive(false); //deactivate param2
        activeChar.SetActive(true);  //activate param1
        activeChar.transform.position = inactiveChar.transform.position;
        // attach gameobjects to character2
        thirdperson.transform.parent = activeChar.transform;
        firstperson.transform.parent = activeChar.transform;
        //  thirdperson.transform.localPosition = new Vector3(2f, 1.33f, -5f);old

        thirdperson.transform.localPosition = thirdPersonOffset;
        firstperson.transform.localPosition = thirdPersonZoomOffset;
        thirdperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localRotation = Quaternion.identity;
        rayOrigin.localPosition = new Vector3(0,0,5f);

        activeChar.transform.rotation = inactiveChar.transform.rotation;
        activeChar.transform.position = inactiveChar.transform.position + characterOffset;
      

    }


  
}
