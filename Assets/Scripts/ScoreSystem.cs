using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
	public float score, maxScore = 500;
	public Transform goal, player;

	public float startingDistance;
	
    // Start is called before the first frame update
    void Start() {
		startingDistance = Vector3.Distance(goal.position, player.position);
	}

	// Update is called once per frame
	void Update() {
		float dis = Vector3.Distance(goal.position, player.position);

		score = maxScore * -(dis / startingDistance - 1);
    }
}
