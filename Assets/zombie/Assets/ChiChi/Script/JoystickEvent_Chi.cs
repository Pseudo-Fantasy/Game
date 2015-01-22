using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Joystick event
/// </summary>
public class JoystickEvent_Chi : MonoBehaviour {
	private float v,h;
	Animator animator;
	bool walking = false;
	public AudioClip walkingAudio;
	public int doorClosedCount = 0;

	void Start () {
		animator = GetComponent<Animator> ();
		InvokeRepeating ("WalkingAudio", 0, 0.5f);
	}
	void FixedUpdate(){
		Vector3 velocity = new Vector3 (0, 0, v);
		velocity = transform.TransformDirection (velocity);
		transform.localPosition += velocity*3 * Time.fixedDeltaTime;
		transform.Rotate (0, h * 2, 0);	

	}

	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
	}
	
	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove	;
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}
		
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}
	
	
	void On_JoystickMoveEnd(MovingJoystick move){
		if (move.joystickName == "Move_Turn_Joystick"){
			animator.SetBool("Walking", false);
			animator.Play("HoldingGun");
			walking = false;
			v=0;
			h=0;
		}
	}
	void On_JoystickMove( MovingJoystick move){
		if (move.joystickName == "Move_Turn_Joystick"){
			animator.SetBool("Walking", true);
			walking = true;
			h=move.joystickAxis.x;
			v=move.joystickAxis.y;
		}
	}

	public void WalkingAudio()
	{
		if(walking)
		{
			AudioSource.PlayClipAtPoint(walkingAudio, transform.position);
		}
	}

	public void OneDoorClosed()
	{
		doorClosedCount++;
	}
}
