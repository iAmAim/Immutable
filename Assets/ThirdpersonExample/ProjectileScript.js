var lifeTimes = 5.0;

function Awake () {
	Destroy (gameObject, lifeTimes);
}


function OnTriggerEnter (other : Collider) {
	Destroy (gameObject);
}