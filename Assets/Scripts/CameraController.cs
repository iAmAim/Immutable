using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]

public class CameraController : MonoBehaviour {

    public Transform[] cameraPerspective;
    public int cameraIndex;
    CameraSettings camerasettings;
    public float cameraRotationX = 0f;

    public Transform player;
    float temporaryDistance = 5f;
    private Transform thirdPersonTransform;

	// Use this for initialization
	void Awake () {
         
        camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thirdPersonTransform = cameraPerspective[cameraIndex].transform;
	}
	
	// Update is called once per frame
	void Update () {
        //check if View is blocked
        removeBlockFromView();
        getCameraPerspective(); // moves to 3rd person or first person view
       

        if (cameraPerspective[cameraIndex])
        {  
            //disable smoothing if smoothing is zero
            if (cameraIndex == 1)
            {
                //snap to position
                transform.position = cameraPerspective[cameraIndex].position;
                transform.rotation = cameraPerspective[cameraIndex].rotation;
            }
            else
            {  //the higher the smoothing is the faster the camera will be in position
                transform.position = Vector3.Lerp(transform.position, cameraPerspective[cameraIndex].position, Time.deltaTime * camerasettings.smoothing);
                transform.rotation = cameraPerspective[cameraIndex].rotation; 
            }

            //this stores rotation independently, tracks rotation frame to frame                                                                                    
            cameraRotationX -= Input.GetAxis("Mouse Y");

            //clamp, set camera pitch min and max values
            cameraRotationX = Mathf.Clamp(cameraRotationX, -camerasettings.minCameraPitch, camerasettings.cameraPitchMax);


            //Apply rotation to Camera
           Camera.main.transform.Rotate(cameraRotationX, 0f, 0f);
         

            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
           // Debug.DrawRay(Camera.main.transform.position, forward, Color.green);
           //returnCameraRotationX();
           
        }
       
	}


    public void removeBlockFromView()
    {
        Debug.DrawLine(transform.position, player.position, Color.cyan);  
        RaycastHit hit;

        /* Check if something is blocking the view*/


        if (Physics.Linecast(player.position, thirdPersonTransform.position, out hit))
        {
            temporaryDistance = Vector3.Distance(player.position, hit.point) - 1f;
            Debug.Log("Distance is" + temporaryDistance);
            if (temporaryDistance < 1f)
            {
                temporaryDistance = 1f;
            }


        }
        
        thirdPersonTransform.localPosition = new Vector3(1f, 2f, -temporaryDistance);


    }

    public int getCameraIndex()
    {
        
            return cameraIndex;
        
    }

    public void getCameraPerspective()
    {
      
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cameraIndex++;
            Debug.Log("Camera switched");
            if (cameraIndex >= cameraPerspective.Length)
            {
                cameraIndex = 0;

            }
            camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
        }
        
    }

  

    public float returnCameraRotationX()
    {
        return cameraRotationX;
    }
}
