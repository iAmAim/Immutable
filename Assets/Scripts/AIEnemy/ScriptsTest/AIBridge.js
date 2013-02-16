
var bridges = new Array();

function OnTriggerEnter (other:Collider) 
{
	if (other.tag == "AISector")
	bridges.Push(other.gameObject);
}