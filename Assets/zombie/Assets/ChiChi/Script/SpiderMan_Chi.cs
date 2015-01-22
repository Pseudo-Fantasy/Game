using UnityEngine;
using System.Collections;

public class SpiderMan_Chi : MonoBehaviour {

	Animator animator;

	//
	//
	// Do Not Use This Script Unless You Are Debugging
	//
	//
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//
		//
		// Do Not Use This Script Unless You Are Debugging
		//
		//
		if(Input.GetKey(KeyCode.W))
		{
			animator.SetBool("Walking", true);
			transform.Translate(Vector3.forward * Time.deltaTime * 3.0f);
			animator.Play("WalkHoldingGun");
		}

		if(Input.GetKey(KeyCode.S))
		{
			animator.SetBool("Walking", true);
			transform.Translate(Vector3.back * Time.deltaTime * 3.0f);
			animator.Play("WalkHoldingGun");
		}

		if(Input.GetKey(KeyCode.A))
		{
			animator.SetBool("Walking", true);
			transform.Rotate(Vector3.down * Time.deltaTime * 70.0f);
		}

		if(Input.GetKey(KeyCode.D))
		{
			animator.SetBool("Walking", true);
			transform.Rotate(Vector3.up * Time.deltaTime * 70.0f);
		}

		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			animator.SetBool("Walking", false);
			animator.CrossFade("HoldingGun", 0.1f);
		}
	}


}
