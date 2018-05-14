﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderSpawner : MonoBehaviour {
	public GameObject MonsterCandidate;
	public List<Transform> SpawnPoint;
	public NavMeshSurface surface;
	public float SpawnMonsterTime = 10;
	private float spwanCounter = 0;

	void Start(){
		surface.Bake ();
	}
	// Update is called once per frame
	void Update () {
		spwanCounter += Time.deltaTime;

		if (spwanCounter >= SpawnMonsterTime) {
			spwanCounter = 0;

			GameObject newMonster = GameObject.Instantiate (MonsterCandidate);
			newMonster.transform.position = SpawnPoint [Random.Range (0, SpawnPoint.Count)].position;
			UpdateNavMesh ();

		}

	}
	void UpdateNavMesh()
	{
		surface.RemoveData ();
		surface.Bake ();
	}
}
