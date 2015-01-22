using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieCreator_Chi : MonoBehaviour {

	public GameObject[] Zombies = new GameObject[2];
	GameObject HPbar;
	Animator animator;
	bool DoorClosed = false;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreatOneZomebie", 0, 4);
		HPbar = GameObject.Find ("HP");
		animator = GetComponent<Animator> ();
	}

	public void CreatOneZomebie()
	{
		int Rnd = Random.Range (0, 100);

		if(Rnd < 50)
			Instantiate(Zombies[0], transform.position, transform.rotation);
		else
			Instantiate(Zombies[1], transform.position, transform.rotation);
	}

	void OnCollisionEnter( Collision sth)
	{
		if (sth.gameObject.name == "Spiderman" && !DoorClosed)
		{
			CancelInvoke("CreatOneZomebie");
			animator.Play("DoorClose");
			audio.Play();
			DoorClosed = true;
			GameObject spiderman = GameObject.Find("Spiderman");
			spiderman.GetComponent<JoystickEvent_Chi>().OneDoorClosed();
			if(spiderman.GetComponent<JoystickEvent_Chi>().doorClosedCount == 4)
				GameObject.Find("Text").GetComponent<Text>().text = "Win";
		}			
	}
}
