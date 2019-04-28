using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;

	public Vector3 offset;
	
	public float followSpeed;

	public bool rotateAround;
	public float rotateSpeed;
	public Vector3 rotatePosition;
	Quaternion initialRotation;
	
    // Start is called before the first frame update
    void Start() {
		initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {


		if(rotateAround){
			transform.RotateAround(rotatePosition, Vector3.up, rotateSpeed * Time.deltaTime);
		}else{
			transform.rotation = initialRotation;

			transform.position = Vector3.Lerp(transform.position,
					target.position + offset,
					Time.deltaTime * followSpeed);
		}
    }
}
 