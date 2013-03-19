using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameObject thirdperson;
    GameObject firstperson;
    GameObject character2;
    GameObject character1;
    Transform rayOrigin;
    bool isactive;
    float charchangecooldown = 5f;
    float nextcooldown = 0;


   GameObject activeChar;
 
    void Awake()
    {
        thirdperson = GameObject.FindGameObjectWithTag("ThirdPersonCamera");
        firstperson = GameObject.FindGameObjectWithTag("FirstPersonCamera");
        character2 = GameObject.FindGameObjectWithTag("character2");
        character1 = GameObject.FindGameObjectWithTag("Player");
        rayOrigin = GameObject.FindGameObjectWithTag("target").transform;
       
    }
	// Use this for initialization
	void Start () {

        activateChar(character1,character2);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("3") && Time.time > nextcooldown)
        {
            nextcooldown = Time.time + charchangecooldown;
            switchPlayer();

        }
	
	}

    void switchPlayer()
    {
        if (!character1.activeInHierarchy){

           // activateCharacter1();
            activateChar(character1, character2);
            character1.transform.position = character2.transform.position + new Vector3(0, 10, 0);

       }
        else
        { 
            //activateCharacter2();
            activateChar(character2, character1);
            character2.transform.position = character1.transform.position + new Vector3(0, 10, 0);
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

        thirdperson.transform.localPosition = new Vector3(.5f, 0.70f, -1.5f);
        firstperson.transform.localPosition = new Vector3(.64f, 0.96f, -1.21f);
        thirdperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localRotation = Quaternion.identity;
        rayOrigin.localPosition = new Vector3(0,0,5f);

        activeChar.transform.rotation = inactiveChar.transform.rotation;
      

    }


  
}
