using UnityEngine;
using System;
using System.Collections.Generic;
using NaughtyAttributes;

[Serializable]
public class Lane{
	public string name;

	[HideInInspector]
	public EndlessController controller;

	public List<Hazard> currentHazards;
	[ReorderableList]
	public List<Hazard> possibleHazards;
	public int[] apperanceCooldown;
	public int currentCooldown = 0;

	public Vector3 laneRotation;

	public int currentPosition;

	public int lookAhead;

	public Transform startingPosition;

	
	public void NextObject(){
		currentCooldown--;
		currentPosition++;

		if(currentCooldown <= 0){
			int r = UnityEngine.Random.Range(0, possibleHazards.Count);
			if (apperanceCooldown[r] > 0) //Just check once?
				r = UnityEngine.Random.Range(0, possibleHazards.Count);

			Hazard h = possibleHazards[r];
			h.spawnedPosition = currentPosition;
			currentCooldown = h.laneCooldown;

			GameObject g = GameObject.Instantiate(
							h.gameObject,
							h.spawnOffset + controller.transform.position + startingPosition.localPosition,
							Quaternion.identity * Quaternion.Euler(laneRotation));
			currentHazards.Add(g.GetComponent<Hazard>());

			CleanHazards();
		}
	}

	public void CleanHazards(){
		List<Hazard> clean = new List<Hazard>();
		foreach (Hazard h in currentHazards) {
			if (h.cleanDistance + h.spawnedPosition + lookAhead < currentPosition) {
				clean.Add(h);
				GameObject.Destroy(h.gameObject);
			}
		}
		currentHazards.RemoveAll((g) => clean.Contains(g));
	}
}