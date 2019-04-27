using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Obstacle : MonoBehaviour {
	public float speed;
	
	CharacterController cc;
	
	[ReorderableList]
	public List<Transform> obstaclePath;

	[Slider(0,100)]
	public float distanceThreshold;
	public int pathIndex = 0;

	public bool resetOnEnd;

    // Start is called before the first frame update
    void Start() {
		cc = GetComponent<CharacterController>();

	}

    // Update is called once per frame
    void FixedUpdate() {
		Vector3 dir = obstaclePath[pathIndex].position - transform.position;
		dir.Normalize();

		cc.Move(dir * speed);

		if(Vector3.Distance( transform.position, obstaclePath[pathIndex].position) < distanceThreshold){
			pathIndex++;

			if (pathIndex >= obstaclePath.Count)
				pathIndex = 0;

			if(pathIndex == 0 && resetOnEnd) {
				transform.position = obstaclePath[0].position;
				pathIndex = 1;
			}
		}
    }
}