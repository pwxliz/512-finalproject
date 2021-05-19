using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavespwan2 : MonoBehaviour
{
	// Start is called before the first frame update
	
	public int WaveSize;
	public GameObject EnemyPrefab1;
	public GameObject EnemyPrefab2;
	public float EnemyInterval;
	public Transform spawnPoint;
	public float startTime;
	public Transform[] WayPoints;
	public static int enemyCount;
	public static int enemyCount1;
	public static int maxsize;
	public int wave;
	bool wave2;

	

	void Start()
	{

		wave2 = false;
		maxsize = WaveSize;
		enemyCount = 0;
		enemyCount1 = 0;
		InvokeRepeating("SpawnEnemy", startTime, EnemyInterval);

	}

	
		
	void Update()
	{


		if (enemyCount == WaveSize)
		{
			CancelInvoke("SpawnEnemy");
			wave2 = true;
		}
		if (maxsize == 0)
		{

			if (wave2)
			{

				InvokeRepeating("Wave", startTime, EnemyInterval);
				wave2 = false;
				maxsize = WaveSize;

			}
		}
		if (enemyCount1 == WaveSize)
		{
			CancelInvoke("Wave");
			gamemanager.instance.stillwave = false;
		}

	}

	void SpawnEnemy()
	{

		enemyCount++;
		GameObject enemy = GameObject.Instantiate(EnemyPrefab1, spawnPoint.position, Quaternion.identity) as GameObject;
		gamemanager.instance.monsternumber++;
		enemy.GetComponent<Enemy>().waypoints = WayPoints;

	}
	void Wave()
	{
		enemyCount1++;
		GameObject enemy = GameObject.Instantiate(EnemyPrefab2, spawnPoint.position, Quaternion.identity) as GameObject;
		gamemanager.instance.monsternumber++;
		enemy.GetComponent<Enemy>().waypoints = WayPoints;
	}
}
