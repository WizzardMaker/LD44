using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Lane{
	public string name;

	[HideInInspector]
	public EndlessController controller;

	public List<Hazard> currentHazards;
	public List<Hazard> possibleHazards;
	public int currentCooldown = 0;

	public int currentPosition;

	public Transform startingPosition;

	
	public void NextObject(){
		currentCooldown--;

		if(currentCooldown >= 0){
			Hazard h = possibleHazards[UnityEngine.Random.Range(0, possibleHazards.Count)];

			currentCooldown = h.laneCooldown;
			GameObject g = GameObject.Instantiate(h.gameObject, controller.transform.position + startingPosition.localPosition, Quaternion.identity); ;
			currentHazards.Add(g.GetComponent<Hazard>());
		}
	}

	public void CleanHazards(){
		foreach(Hazard h in currentHazards){
			h.Clean(currentPosition);
		}
	}
}