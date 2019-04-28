using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EndlessController : MonoBehaviour {
	[ReorderableList]
	public List<Lane> lanes;

	public float playerStepDistance;
	float _currentStepDistance;
	public PlayerController player;

    // Start is called before the first frame update
    void Start() {
        foreach(Lane l in lanes){
			l.controller = this;
		}

		player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if(player.transform.position.z > _currentStepDistance) {
			_currentStepDistance += playerStepDistance;
			Debug.Log("Next!");
		}
    }
}
