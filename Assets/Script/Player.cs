using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject bullet;
	public float health = 100;
	public GameObject EnemyMan;
	public GameObject resetObj;
	public AudioClip backgroundmusic;
	public AudioClip failsound;
	public Text healthText;

	private int point = 0;
	private AudioSource audio;
	private Text hptext;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = backgroundmusic;
		audio.volume = 0.5f;
		audio.Play();
		hptext = healthText.GetComponent<Text> () ;
	}
	
	// Update is called once per frame
	void Update () {

		EnemyMan.SendMessage ("getHealth", health);
		if (health <= 0) {
			hptext.text = "0 / 100";
		} else {
			hptext.text = health + " / 100";
		}

   		
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			Shootbullet ();
		}

	}

	void Shootbullet(){
		GameObject temp_bullet;
		temp_bullet = Instantiate (bullet, transform.position, transform.rotation)as GameObject;

		Destroy(temp_bullet, 4.0f);
	}

	public void enemyKilled(){
		point++;
	}

	public void beenHit(){
		health -= 10;
		if (health == 0) {
			audio.clip = failsound;
			audio.volume = 1.0f;
			audio.Play();
			resetObj.SendMessage ("turnItOn", point);
		}
	}

	public float getHealth(){
		return health;
	}

	public void restart(){
		health = 100;
		audio.clip = backgroundmusic;
		audio.volume = 0.5f;
		audio.Play();
	}

}
