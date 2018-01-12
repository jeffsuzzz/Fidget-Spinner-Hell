using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FidgetSpinnerAI : MonoBehaviour {

	public GameObject target;
	public GameObject explosion;
	public AudioClip hitsound;

	private Vector3 destinationPos;
	private float speed =4 ;
	private float routeNumber = 0 ;
	private float ifspit = 0;
	private float size = 1;
	private AudioSource audio;

	// Use this for initialization
	void Start () {

		// if it has multiple route, randomize it

		if (routeNumber == 0) {
			destinationPos = target.transform.position;
		} else {
			destinationPos = Random.insideUnitCircle * 20;
			destinationPos.y = Mathf.Abs(destinationPos.y);
		}

		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Player playerScript = GetComponent<Player> ();
		playerScript = GameObject.Find ("Head").GetComponent<Player>();
		if (playerScript.health == 0) {
			Destroy (gameObject);
		}

		transform.position = Vector3.MoveTowards (transform.position, destinationPos, speed * Time.deltaTime);
		ifDest ();
	}

	// see if the spinner arrives its destination or not

	void ifDest(){
		float distance = Vector3.Distance (destinationPos, transform.position);
		if (distance < 2) {
			if (routeNumber <= 0) {
				target.SendMessage ("beenHit");
				audio.clip = hitsound;
				audio.volume = 0.5f;
				audio.Play ();

				Destroy (gameObject);
			} else if(routeNumber == 1){
				destinationPos = target.transform.position; 

				if(ifspit == 1){
					split ();
				}
			} else {
				destinationPos = Random.insideUnitCircle * 20 ;
				destinationPos.y = Mathf.Abs(destinationPos.y);
			}
			routeNumber --;
		}
	}

	// the spinners may hit to each other

	void OnTriggerEnter(Collider other){
		if (other.tag != "SPinners") {
			target.SendMessage ("enemyKilled");
		}
		GameObject explode;
		explode = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		Destroy (explode, 1.0f);
		Destroy (gameObject);
	}

	// hanzo's scatter arrow!

	void split(){		
		Vector3 spinner_dir1, spinner_dir2;
		spinner_dir1 = transform.forward + transform.right;
		spinner_dir2 = transform.forward - transform.right;
			
		GameObject temp_spinner1, temp_spinner2;
		temp_spinner1 = Instantiate(gameObject, transform.position + spinner_dir1*5, transform.rotation) as GameObject;
		temp_spinner2 = Instantiate(gameObject, transform.position + spinner_dir2*5, transform.rotation) as GameObject;

		float[] array = { speed, routeNumber-1, 0, size };
		temp_spinner1.SendMessage("setTarget", target);
		temp_spinner1.SendMessage("setEverything", array);
		
		temp_spinner2.SendMessage("setTarget", target);
		temp_spinner2.SendMessage("setEverything", array);

		Destroy(gameObject);
	}

	public void setTarget(GameObject targetobj) {
		target = targetobj;
	}
		
	// set speed, number of route, ifspit, size
	public void setEverything(float[] array){
		speed = array [0];
		routeNumber = array [1];
		ifspit = array [2];
		size = array [3];
		transform.localScale *= size;
	}		
	
}
