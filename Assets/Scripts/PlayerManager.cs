using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameObject thirdperson;
    GameObject firstperson;
    GameObject character2;
    GameObject character1;
    bool isactive;


   public int charindex;
   GameObject activeChar;
 
    void Awake()
    {
        thirdperson = GameObject.FindGameObjectWithTag("ThirdPersonCamera");
        firstperson = GameObject.FindGameObjectWithTag("FirstPersonCamera");
        character2 = GameObject.FindGameObjectWithTag("character2");
        character1 = GameObject.FindGameObjectWithTag("Player");
       
    }
	// Use this for initialization
	void Start () {

        activateChar(character1,character2);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("3"))
        {

            switchPlayer();

        }
	
	}

    void switchPlayer()
    {
        if (!character1.activeInHierarchy){

            character1.transform.position = character2.transform.position + new Vector3(0, 20, 0);
           // activateCharacter1();
            activateChar(character1, character2);
            character1.transform.position = character2.transform.position + new Vector3(0, 20, 0);

       }
        else
        { 
            character2.transform.position = character1.transform.position + new Vector3(0, 20, 0);
            //activateCharacter2();
            activateChar(character2, character1);
        }
    }

    /*void activateCharacter1()
    {
        character1.SetActive(true);
        character2.SetActive(false);

        thirdperson.transform.parent = character1.transform;
        firstperson.transform.parent = character1.transform;
        thirdperson.transform.localPosition = new Vector3(2f, 1.33f, -5f);
        thirdperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localPosition = new Vector3(.64f, 0.96f, -1.21f);
        character1.transform.position = character2.transform.position;
        character1.transform.rotation = character2.transform.rotation;


        
        Debug.Log("player switched");
    }


    void activateCharacter2()
    {
   
        
        character1.SetActive(false); //deactivate character1
        character2.SetActive(true);  //activate player2
        character2.transform.position = character1.transform.position;
        // attach gameobjects to character2
        thirdperson.transform.parent = character2.transform;
        firstperson.transform.parent = character2.transform;
        thirdperson.transform.localPosition = new Vector3(2f, 1.33f, -5f);
        firstperson.transform.localPosition = new Vector3(.64f, 0.96f, -1.21f);
        thirdperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localRotation = Quaternion.identity;
     
        character2.transform.rotation = character1.transform.rotation;
        
        Debug.Log("player switched");



        //character1.GetComponent<FireBullet>().enabled = false;
        // character1.GetComponentInChildren<FireBullet>().enabled = false;
        // var playerscript = character1.GetComponent<UnitPlayer>();
        // playerscript.enabled = false;
    }*/

    void activateChar(GameObject activeChar, GameObject inactiveChar)
    {
        inactiveChar.SetActive(false); //deactivate character1
        activeChar.SetActive(true);  //activate player2
        activeChar.transform.position = inactiveChar.transform.position;
        // attach gameobjects to character2
        thirdperson.transform.parent = activeChar.transform;
        firstperson.transform.parent = activeChar.transform;
        thirdperson.transform.localPosition = new Vector3(2f, 1.33f, -5f);
        firstperson.transform.localPosition = new Vector3(.64f, 0.96f, -1.21f);
        thirdperson.transform.localRotation = Quaternion.identity;
        firstperson.transform.localRotation = Quaternion.identity;

        activeChar.transform.rotation = inactiveChar.transform.rotation;
      

    }


  
}
