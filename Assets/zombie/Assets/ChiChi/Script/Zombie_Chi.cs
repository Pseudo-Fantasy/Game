using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Zombie_Chi : MonoBehaviour {
	Animator animator;
	enum WalkState{Crawl = 1, funnierWalk, Run};
	WalkState walkState;
	public int blood = 100;
	public GameObject BloodEffect;
	public Texture2D[] textures = new Texture2D[8];
	public GameObject HPbar;
	bool ZombieWin = false;
	bool StopMoving = false;
	GameObject Precious;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		int Rnd = Random.Range (0, 100);
		int textureChange = Random.Range (0, 8);
		HPbar = GameObject.Find ("HP");
		Precious = GameObject.Find("Precious");
		if(Rnd < 33)
		{
			walkState = WalkState.Crawl;
			animator.SetInteger("Walk", 1);
		}
			
		else if(Rnd < 66)
		{
			walkState = WalkState.funnierWalk;
			animator.SetInteger("Walk", 2);
		}
			
		else
		{
			walkState = WalkState.Run;
			animator.SetInteger("Walk", 3);
		}
			
		if(gameObject.transform.name == "zombieBoy(Clone)")
			transform.FindChild("ZombieBoy").renderer.material.SetTexture("_MainTex", textures[textureChange % 4]);
			//GameObject.Find("ZombieBoy").renderer.material.SetTexture("_MainTex", textures[textureChange % 4]);
		else
			transform.FindChild("zombieGirl").renderer.material.SetTexture("_MainTex", textures[textureChange % 4 +4]);
	}
	
	// Update is called once per frame
	void Update () {
		if(!ZombieWin && !StopMoving)
		{
			if(blood > 0)
			{
				transform.LookAt (Precious.transform.position);
			}

			if(blood > 0)
			{
				if(walkState == WalkState.Crawl && animator.GetInteger("AttackAction") == 0)
				{
					transform.Translate(Vector3.forward * 0.03f);
					animator.SetInteger("Walk", 1);
				}
				else if(walkState == WalkState.funnierWalk && animator.GetInteger("AttackAction") == 0)
				{
					transform.Translate(Vector3.forward * 0.03f);
					animator.SetInteger("Walk", 2);
				}
				else if(walkState == WalkState.Run && animator.GetInteger("AttackAction") == 0)
				{
					transform.Translate(Vector3.forward * 0.045f);
					animator.SetInteger("Walk", 3);
				}

				animator.SetInteger("AttackAction", 0);
			}
		}
		else if(blood > 0 && ZombieWin)
		{
			animator.Play("Win");
			GameObject Precious = GameObject.Find("Precious");
			transform.LookAt(Precious.transform.position);
			CancelInvoke("attack");

			GameObject text = GameObject.Find("Text");
			text.GetComponent<Text>().text = "Lose";
		}
	}

	void OnCollisionEnter( Collision sth)
	{
		if (sth.gameObject.name == "PreciousWall")
		{
			InvokeRepeating ("attack", 0.3f, 0.73f);
			StopMoving = true;
		}			
	}

	public void attack()
	{
		if(blood > 0 && !ZombieWin)
		{
			transform.LookAt(Precious.transform.position);
			HPbar.GetComponent<Image>().fillAmount -= 0.00005f;
			int Rnd = Random.Range (0, 100);
			if(Rnd < 50 && walkState != WalkState.Crawl)
				animator.SetInteger("AttackAction", 1);
			else if(Rnd >= 50 && walkState != WalkState.Crawl)
				animator.SetInteger("AttackAction", 2);

			if(HPbar.GetComponent<Image>().fillAmount <= 0)
				ZombieWin = true;
		}
	}

	public void hit()
	{
		blood -= 100;

		GameObject f;
		f = Instantiate(BloodEffect, transform.position, transform.rotation) as GameObject;
		f.transform.parent = gameObject.transform.parent;
		Destroy (f, 0.5f);

		if(blood <= 0)
			death();
		int rnd = Random.Range (0, 100);
		if(rnd < 50 && walkState != WalkState.Crawl)
		{
			animator.Play("hit1");
		}

		else if(rnd > 50 && walkState != WalkState.Crawl)
		{
			animator.Play("hit2");
		}
	}

	public void death()
	{
		animator.SetBool ("Death", true);
		//animator.Play("Death");
		Destroy (gameObject, 3.0f);
		gameObject.collider.enabled = false;
	}
}
