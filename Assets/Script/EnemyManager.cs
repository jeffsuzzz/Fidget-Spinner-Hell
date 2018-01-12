using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject target;
    public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject enemy2;
    public float spawnTime;            // How long between each spawn.
	public float speed = 4f;
	public float routeNumber = 0f;
	public float size = 1f;
	public int stageNumber = 0;

	private float stageTime = 10f;
	private float stageclock = 0f;
    private bool enableSpawn;
	private float targetHPnow = 100f;
	private float targetHPtemp;

    // Use this for initialization
    void Start () {
        enableSpawn = true;
		targetHPtemp = targetHPnow;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
		if(targetHPnow <= 0){
			enableSpawn = false;
		}
			
		stageclock += Time.deltaTime;

		// speed up and add route every amount of time

		if(stageclock >= stageTime){
			speed+=2;
			routeNumber++;
			stageclock = 0;

			// adjust the level of difficulty every certain amount of time 

			EstimateDifficulty ();
			targetHPtemp = targetHPnow;
		}
    }

	// if the player has 80% health or more, get to the next stage
	// less than 50%, the size of the fidget spinners will increase

	void EstimateDifficulty(){
		if (targetHPnow / targetHPtemp > 0.8f) {
			stageNumber++;
		} else if(targetHPnow / targetHPtemp < 0.5f){
			size += 0.5f;
			speed -= 2;
			routeNumber--;
		}
	}

    void Spawn()
    {
        if (enableSpawn)
        {
			Vector3 spawnPos = new Vector3(Random.Range(-30.0F, 30.0F), Random.Range(-10.0F, 10.0F), Random.Range(0.0F, 30.0F));
			GameObject enemies;
			float ifspit;
			float[] array;

			// change the model and ifspit of the enemy depends on the stage
			// default is final stage

			switch (stageNumber) {
			case 0:
				enemies = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
				ifspit = 0;
				enemies.SendMessage("setTarget", target);
				array = new float[] { speed, routeNumber, ifspit, size };
				enemies.SendMessage ("setEverything", array);
				break;
			case 1:
				enemies = Instantiate (enemy2, spawnPos, Quaternion.identity) as GameObject;
				enemies.SendMessage("setTarget", target);
				ifspit = 0;
				array = new float[] { speed, routeNumber, ifspit, size };
				enemies.SendMessage ("setEverything", array);
				break;
			case 2:
				enemies = Instantiate (enemy2, spawnPos, Quaternion.identity) as GameObject;
				enemies.SendMessage("setTarget", target);
				ifspit = 1;
				array = new float[] { speed, routeNumber, ifspit, size };
				enemies.SendMessage ("setEverything", array);
				break;
			default:
				enemies = Instantiate (enemy2, spawnPos, Quaternion.identity) as GameObject;
				enemies.SendMessage("setTarget", target);
				ifspit = 1;
				array = new float[] { speed, routeNumber, ifspit, size };
				enemies.SendMessage ("setEverything", array);
				break;
			}

			          
        }

    }

	// get player's health

	public void getHealth(float h){
		targetHPnow = h;
	}

	public void restart(){
		speed = 4f;
		routeNumber = 0f;
		size = 1f;
		stageNumber = 0;
		targetHPnow = 100f;
		targetHPtemp = 100f;

		stageclock = 0f;
		enableSpawn = true;
	}
}
