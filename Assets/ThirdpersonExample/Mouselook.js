var sensitivityY = 3.0;
var minimumY = -90.0;
var maximumY = 90.0;

var rotationY = 0.0;
	
function Update (){

	if (SimpleFPSWalker.toggleCombatMode){
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
	}
	rotationY = ClampAngle (rotationY, minimumY, maximumY);

	yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
	transform.localRotation = yQuaternion;
		
}
	
static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}
