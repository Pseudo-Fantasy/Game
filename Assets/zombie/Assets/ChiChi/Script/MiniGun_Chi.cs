using UnityEngine;
using System.Collections;

public class MiniGun_Chi : MonoBehaviour {
	Animator animator;
	public GameObject fireEffect;
	public GameObject firePos;
	bool shootingPress;
	public AudioClip MiniGunAD;
	public LayerMask Wall;
	void Start () {
		animator = GetComponent<Animator> ();
		InvokeRepeating ("ShootingRayCaster", 0, 0.1f);
	}
	
	public void Shooting()
	{
		AudioSource.PlayClipAtPoint (MiniGunAD, transform.position);
		InvokeRepeating ("shootingAudio", 0.2f, 0.2f);
		shootingPress = true;
	}

	public void NotShooting()
	{
		CancelInvoke ("shootingAudio");
		shootingPress = false;
		animator.SetBool ("Attack", false);
	}

	void Update()
	{
		if(shootingPress)
		{
			GameObject f;
			animator.Play("MiniGunAttack");
			animator.SetBool ("Attack", true);
			f = Instantiate(fireEffect, firePos.transform.position, firePos.transform.rotation) as GameObject;
			f.transform.parent = gameObject.transform.parent;
			Destroy(f, 0.1f);
		}
	}

	public void ShootingRayCaster()
	{
		if(shootingPress)
		{
			Vector3 fwd = transform.TransformDirection(Vector3.forward); //somthing in front of me
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(transform.position, fwd, out hit, 24, Wall) )
			{
				//Debug.Log(hit.transform.name);
				//Debug.DrawLine(transform.position, hit.transform.position);
				if(hit.transform.name == "zombieBoy(Clone)")
				{
					if(hit.transform.gameObject.GetComponent<Zombie_Chi>().blood > 0)
						hit.transform.gameObject.GetComponent<Zombie_Chi>().hit();
					else
						hit.transform.gameObject.GetComponent<Zombie_Chi>().death();
					//Destroy(hit.transform.gameObject);
					
				}
				if(hit.transform.name == "zombieGirl(Clone)")
				{
					if(hit.transform.gameObject.GetComponent<Zombie_Chi>().blood > 0)
						hit.transform.gameObject.GetComponent<Zombie_Chi>().hit();
					else
						hit.transform.gameObject.GetComponent<Zombie_Chi>().death();
				}
			}
		}
	}

	void shootingAudio()
	{
		AudioSource.PlayClipAtPoint (MiniGunAD, transform.position);
	}
}
