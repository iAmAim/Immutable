var bullet : Transform;
var Crosshairtex : Texture;

var target : Transform;

function Update () {


	if (SimpleFPSWalker.toggleCombatMode){
		if(Input.GetButton("Fire1")){
         Debug.Log("shooting..");
				var hit: RaycastHit;
			    
				if (Physics.Raycast(target.position,target.forward, hit)) {
					var hitpoint = hit.point;
					transform.LookAt(hitpoint);
					var bullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
					bullet1.transform.rotation = transform.rotation;
					bullet1.rigidbody.AddForce(transform.forward * 1000);
			}
		}
	}
}
	


function OnGUI(){
	GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, Vector3
	(Screen.width / 1024.0, Screen.height / 768.0, 1));
	GUI.DrawTexture (Rect (500,372,24,24), Crosshairtex);
}