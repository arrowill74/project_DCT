using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject MonsterCandidate;
	public List<Transform> SpawnPoint;
	public float SpawnMonsterTime = 10;
	private float spwanCounter = 0;

	// Update is called once per frame
	void Update () {
		spwanCounter += Time.deltaTime;

		if (spwanCounter >= SpawnMonsterTime) {
			spwanCounter = 0;

			GameObject newMonster = GameObject.Instantiate (MonsterCandidate);
			if (newMonster != null) {
				newMonster.transform.position = SpawnPoint [Random.Range (0, SpawnPoint.Count)].position;
			}
		}

	}
}
