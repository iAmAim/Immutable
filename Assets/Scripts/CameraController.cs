using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]

public class CameraController : MonoBehaviour {

    public Transform[] cameraPerspective;
    public int cameraIndex;
    CameraSettings camerasettings;
    public float cameraRotationX = 0f;

    private GameObject char1;
    private GameObject char2;
    private GameObject activeChar;
    float temporaryDistance = 5f;
    private Transform thirdPersonTransform;
    private Transform myTransform;
    private bool overtheshoulder = false;


	// Use this for initialization
	void Awake () {


        camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
        char1 = GameObject.FindGameObjectWithTag("Player");
        char2 = GameObject.FindGameObjectWithTag("character2");
        thirdPersonTransform = cameraPerspective[cameraIndex].transform;
        
        myTransform = transform;
        activeChar = char1;
	}
	
	// Update is called once per frame
	void Update () {
        //check if View is blocked
        switchView();
        getCameraPerspective(); // moves to 3rd person or first person view
 
            // no smoothing if index = 1 (firstperson)
            if (cameraIndex == 1)
            {
             //snap to position
             myTransform.position = cameraPerspective[cameraIndex].position;
             myTransform.rotation = cameraPerspective[cameraIndex].rotation;
            }
            else
            {  //the higher the smoothing is the faster the camera will be in position
            myTransform.position = Vector3.Lerp(transform.position, cameraPerspective[cameraIndex].position, Time.deltaTime * camerasettings.smoothing);
            myTransform.rotation = cameraPerspective[cameraIndex].rotation; 
            }

            //this stores rotation independently, tracks rotation frame to frame                                                                                    
            cameraRotationX -= Input.GetAxis("Mouse Y");

            //clamp, set camera pitch min and max values
            cameraRotationX = Mathf.Clamp(cameraRotationX, -camerasettings.minCameraPitch, camerasettings.cameraPitchMax);


            //Apply rotation to Camera
            myTransform.Rotate(cameraRotationX, 0f, 0f);
         

            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
           // Debug.DrawRay(Camera.main.transform.position, forward, Color.green);
           //returnCameraRotationX();
           
        
       
	}

    // switch view to avoid block from view
    public void switchView()
    {
        if (char1.activeInHierarchy)
        {
            Debug.DrawLine(char1.transform.position + new Vector3(0, 1, 0), thirdPersonTransform.position, Color.cyan);
            activeChar = char1;
        }
        else
        {
            Debug.DrawLine(char2.transform.position + new Vector3(0, 1, 0), thirdPersonTransform.position, Color.cyan);
            activeChar = char2;
        }
      

        /* Check if something is blocking the view*/

        if (!overtheshoulder)
        {
            RaycastHit hit;

            /* Check if  something is in between the player and the camera */
            if (Physics.Linecast(activeChar.transform.position + new Vector3(0, 1, 0), thirdPersonTransform.position, out hit, 9))
            {
                /* temporaryDistance = Vector3.Distance(player.position, hit.point) - 1f;
                  Debug.Log("Distance is" + temporaryDistance);
                  if (temporaryDistance < 1f)
                   {
                  temporaryDistance = 1f;
                   }*/
                //Debug.Log("CAMERA HITS " + hit.transform.tag);
                cameraIndex = 1;
            }
            else
            {
                cameraIndex = 0;
            }
        }
        else
        {
            cameraIndex = 1;
        }
        
        //thirdPersonTransform.localPosition = new Vector3(1f, 2f, -temporaryDistance);

    }


    public void getCameraPerspective()
    {
      
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cameraIndex++;
            overtheshoulder = true;
            Debug.Log("Camera switched");
            if (cameraIndex >= cameraPerspective.Length)
            {
                cameraIndex = 0;
                overtheshoulder = false;

            }
            
        }
        camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
        
    }


}
