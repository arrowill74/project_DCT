﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject MonsterCandidate;
	public List<Transform> SpawnPoint;
	public float SpawnMonsterTime = 1;
	private float spwanCounter = 0;
	public GameObject Light;
	// Update is called once per frame
	void Update () {
		spwanCounter += Time.deltaTime;

		if (spwanCounter >= SpawnMonsterTime) {
			spwanCounter = 0;

			GameObject newMonster = GameObject.Instantiate (MonsterCandidate);
			GameObject newlight = GameObject.Instantiate (Light);
			newMonster.transform.position = SpawnPoint [Random.Range (0, SpawnPoint.Count)].position;

		}

	}
}
