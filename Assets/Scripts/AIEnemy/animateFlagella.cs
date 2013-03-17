using UnityEngine;
using System.Collections;

public class animateFlagella : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        animation.CrossFade("flagella_flap", 1.5f);
      

        
	}
}
