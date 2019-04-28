using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
	public static float score, highscore;
	Transform player;

	public float startingDistance;
	
    // Start is called before the first frame update
    void Start() {
		player = FindObjectOfType<PlayerController>().transform;
		startingDistance = player.position.z;
	}

	// Update is called once per frame
	void Update() {
		score = player.position.z - startingDistance;

		highscore = score > highscore ? score : highscore;
    }
}
