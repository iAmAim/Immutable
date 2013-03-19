using UnityEngine;
using System.Collections;

public class animateStaph : MonoBehaviour {

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        animation.CrossFade("walk", .1f);
      
	}
}
