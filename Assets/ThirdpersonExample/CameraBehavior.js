var target : Transform;
var distance = 10.0; 

function Start(){

}

function Update () {

distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
if (distance < 1){
	distance = 1;
}
if (distance > 10){
	distance = 10;
}
transform.localPosition.z =  -distance;



    var hit : RaycastHit; 
	var trueTargetPosition : Vector3 = target.transform.position;
	
    if (Physics.Linecast (trueTargetPosition, transform.position, hit)) {  

	var tempDistance = Vector3.Distance (trueTargetPosition, hit.point) - 0.28; 
     
	if (tempDistance < 1){
		tempDistance = 1;
	}
	if (tempDistance > 10){
		tempDistance = 10;
	}
	 
		transform.localPosition.z = -tempDistance;

   }



}