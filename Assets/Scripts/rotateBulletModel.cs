using UnityEngine;
using System.Collections;

public class rotateBulletModel : MonoBehaviour {

    public float rotationX = 5f;
    Vector3 rotateBullet;

	// Use this for initialization
	void Start () {
        rotateBullet = new Vector3(rotationX, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
       transform.Rotate(rotateBullet);
	}
}




