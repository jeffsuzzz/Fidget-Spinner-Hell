using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	public GameObject player;
	public GameObject enemyManager;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = false;
		foreach(Renderer r in GetComponentsInChildren<Renderer>() ){
			r.enabled = false;
		}
		gameObject.GetComponent<Collider> ().enabled = false;
	}

	// restart the game once been hit by something

	void OnTriggerEnter(Collider other){
		player.SendMessage ("restart");
		enemyManager.SendMessage ("restart");

		turnItOff ();
	}

	// once the player lost, show the score board

	public void turnItOn(int point){
		gameObject.GetComponent<Renderer> ().enabled = true;
		foreach(Renderer r in GetComponentsInChildren<Renderer>() ){
			r.enabled = true;
		}
		gameObject.GetComponent<Collider> ().enabled = true;

		transform.Find("Score").GetComponent<TextMesh>().text = "Score : " + point;
	}

	// once the game restart, let it disappear

	public void turnItOff(){
		gameObject.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponentInChildren<Renderer> ().enabled = false;
		foreach(Renderer r in GetComponentsInChildren<Renderer>() ){
			r.enabled = false;
		}
		gameObject.GetComponent<Collider> ().enabled = false;
	}
}
