using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
	TrailRenderer t;

	private void Start() {
		t = GetComponent<TrailRenderer>();
	}

	// Update is called once per frame
	void Update() {
		t.emitting = false;

		if (Physics.RaycastAll(new Ray(transform.position, Vector3.down),0.05f)
			.Select((R)=>R.transform.tag == "Floor").ToArray().Length != 0) {
			t.emitting = true;

		}
    }
}
