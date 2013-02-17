using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform[] cameraPerspective;
    public int cameraIndex;
    CameraSettings camerasettings;
    public float cameraRotationX = 0f;


	// Use this for initialization
	void Start () {

        camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
	}
	
	// Update is called once per frame
	void Update () {

        getCameraPerspective(); // moves to 3rd person or first person view

        if (cameraPerspective[cameraIndex])
        {  
            //disable smoothing if smoothing is zero
            if (camerasettings.smoothing == 0f)
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
