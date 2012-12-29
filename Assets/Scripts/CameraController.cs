using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform[] cameraPerspective;
    public int cameraIndex;
    CameraSettings camerasettings;
    float cameraRotationX = 0f;

 


	// Use this for initialization
	void Start () {

        camerasettings = cameraPerspective[cameraIndex].GetComponent<CameraSettings>();
	}
	
	// Update is called once per frame
	void Update () {

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

            //clamp, set cameraPitch min -45 max 45 degs
            cameraRotationX = Mathf.Clamp(cameraRotationX, -camerasettings.cameraPitchMax, camerasettings.cameraPitchMax);


            //Apply rotation to Camera
            Camera.main.transform.Rotate(cameraRotationX, 0f, 0f);

           
        }
       
	}

    public int getCameraIndex()
    {
        return cameraIndex;
    }
}
