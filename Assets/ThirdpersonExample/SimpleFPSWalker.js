var runSpeed = 4.0;
var jumpSpeed = 5.0;
var gravity = 20.0;
var antiBunnyHopFactor = 1;
var jumpMoveSpeed = 1.3;

static var toggleCombatMode = true;

var sensitivityX = 3;
var rotationX = 0.0;

private var moveDirection = Vector3.zero;
private var grounded = false;
private var controller : CharacterController;
private var myTransform : Transform;
private var speed : float;
private var jumpTimer : int;

var go : GameObject;

var inputX = 0.0;
var inputY = 0.0;

var inputModifyFactor = 0.0;

var jump = true;

function Start () {
    controller = GetComponent(CharacterController);
    myTransform = transform;
    speed = runSpeed;
    jumpTimer = antiBunnyHopFactor;
	Screen.showCursor = false;
	go = GameObject.FindGameObjectWithTag("Player");
	//script = go.GetComponent("PlayerStatsScript"); 
	//Screen.lockCursor = true;
	// need to make it so u have to clikc on the game window so we can center mouse and make it so it doesnt glitch so mucb
}

function Update(){

	// If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
	if( Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W)) {
		inputY = 0.0;
	} else {
		inputY = Input.GetAxis("Vertical");
	}
	
	if( Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
		inputX = 0.0;
	} else {
		inputX = Input.GetAxis("Horizontal");
	}
	
	//NEED TO ADJUST THIS STILL FOR BACKWARDS AND LEFT RIGHT MOVEMENT SPEED
	if (inputX == 0.0){
		inputModifyFactor = (inputY < 0.0)? .8 : 1.0;
	} else {
		if (inputY < 0.0 && inputX != 0.0){
			inputModifyFactor = (inputX != 0.0 && inputY != 0.0)? .5 : 1.0;
		} else {
			inputModifyFactor = (inputX != 0.0 && inputY != 0.0)? .7071 : 1.0;
		}
	}
	if (grounded) {
		if (!Input.GetButton("Jump")){
			jump = false;
		} else {
			jump = true;
		}
	}

	//This is the tab for combatmobe part
	if( Input.GetKeyDown(KeyCode.Tab) ) 
	{ 
		if( toggleCombatMode ) {
		toggleCombatMode = false; 
		Screen.lockCursor = false;
		Screen.showCursor = true;
		} else {
		toggleCombatMode = true; 
		Screen.lockCursor = true;
		} 
	}
	

	//This is the heavy lifting part of the combat mode part and mouselook part
	if (toggleCombatMode == true )
	{
	rotationX += Input.GetAxis("Mouse X") * sensitivityX;
	}
	xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
	transform.localRotation = xQuaternion;

}

function FixedUpdate () {

    if (grounded) {
        
		moveDirection = Vector3(inputX * inputModifyFactor, 0, inputY * inputModifyFactor);
		moveDirection = myTransform.TransformDirection(moveDirection) * speed;

        // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
        if (!jump){
            jumpTimer++;
        } else {
			if (jumpTimer >= antiBunnyHopFactor) {
				moveDirection = Vector3(inputX * inputModifyFactor, 0, inputY * inputModifyFactor);
				moveDirection = myTransform.TransformDirection(moveDirection) * speed * jumpMoveSpeed;
				moveDirection.y = jumpSpeed;
				jumpTimer = 0;
			}
        }
    }
    // Apply gravity
    moveDirection.y -= gravity * Time.deltaTime;
    // Move the controller, and set grounded true or false depending on whether we're standing on something
    grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
}

@script RequireComponent(CharacterController)

