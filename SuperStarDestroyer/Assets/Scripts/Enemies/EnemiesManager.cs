using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

 
	public GameObject enemy;
	public float nextWaveTime;
	public int nextWaveEnemiesCount;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(WaitNextWave());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnEnemies(int count)
	{
		int spawned = 0;
		
		while (spawned <= count)
		{
			Instantiate(enemy, transform.position, transform.rotation);
			spawned++;

		}
		
	}

	IEnumerator WaitNextWave()
	{
		while (true)
		{
			SpawnEnemies(nextWaveEnemiesCount);
			yield return new WaitForSeconds(nextWaveTime);
		}
	}
}
