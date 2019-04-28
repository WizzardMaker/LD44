using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EndlessController : MonoBehaviour {
	public static EndlessController instance;

	[BoxGroup("Street")]
	public GameObject streetPrefab;
	[BoxGroup("Street")]
	List<GameObject> streetParts = new List<GameObject>();
	[BoxGroup("Street")]
	public Transform streetSpawn;
	[BoxGroup("Street")]
	public float streetCleanDistance = 300f;
	[BoxGroup("Street")]
	public float controllerSpawnStepDistance = 50f;

	[BoxGroup("Player")]
	public float playerStepDistance;
	[BoxGroup("Player")]
	float _currentStepDistance;
	[BoxGroup("Player")]
	public PlayerController player;

	public int lookAhead;

	[ReorderableList]
	public List<Lane> lanes;

	// Start is called before the first frame update
	void Start() {
		instance = this;

        foreach(Lane l in lanes){
			l.controller = this;
		}

		for(int i = 0; i < lookAhead; i++){
			streetParts.Add(Instantiate(streetPrefab, streetSpawn.position, Quaternion.identity));
			transform.position += Vector3.forward * controllerSpawnStepDistance;
		}


		lanes.ForEach((l) => { l.lookAhead = lookAhead; l.apperanceCooldowns = new int[l.possibleHazards.Count]; });

		player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if(player.transform.position.z > _currentStepDistance) {
			_currentStepDistance += playerStepDistance;

			List<GameObject> clean = new List<GameObject>();
			foreach (GameObject sP in streetParts){
				if(Vector3.Distance(sP.transform.position, transform.position) > streetCleanDistance){
					clean.Add(sP);
					Destroy(sP);
				}
			}
			streetParts.RemoveAll((g) => clean.Contains(g));

			streetParts.Add(Instantiate(streetPrefab, streetSpawn.position, Quaternion.identity));
			lanes.ForEach((l) => l.NextObject());
			transform.position += Vector3.forward * controllerSpawnStepDistance;


			Debug.Log("Next!");
		}
    }

	public void BlockHazard(int turns, Hazard hazardPrefab){
		foreach(Lane l in lanes){
			int i = 0;
			l.possibleHazards.ForEach(
			(h) => {
				if (h == hazardPrefab)
					l.apperanceCooldowns[i] += turns;
				i++;
			});
		}
	}

	public void ResetWorld() {
		_currentStepDistance = 0;

		transform.position = Vector3.zero;
			lanes.ForEach((l) => {
				l.currentPosition = 0;
				l.apperanceCooldowns = new int[l.possibleHazards.Count];
				l.currentHazards.ForEach((h) => Destroy(h.gameObject));
				l.currentHazards.Clear();
			});
		
		streetParts.ForEach((s) => Destroy(s));
		streetParts.Clear();

		for (int i = 0; i < lookAhead; i++) {
			streetParts.Add(Instantiate(streetPrefab, streetSpawn.position, Quaternion.identity));
			transform.position += Vector3.forward * controllerSpawnStepDistance;
		}
	}
}
