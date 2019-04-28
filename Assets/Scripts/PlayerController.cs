using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour {
	Rigidbody rig;

	Vector3 startingPos;
	Vector3 startingColliderScale;
	Quaternion startingRot;

	public static bool isEnabled = true;

	public float steerForce;
	public float staticAcceleration;
	public float startAcceleration;
	public float jumpForce;

	public ParticleSystem water;

	public float deathAngle;

	// Start is called before the first frame update
	void Start() {
		rig = GetComponent<Rigidbody>();

		rig.maxAngularVelocity = 12f;
		rig.centerOfMass = Vector3.zero;

		water = GetComponentInChildren<ParticleSystem>();

		rig.AddTorque(transform.right * startAcceleration, ForceMode.VelocityChange);

		startingPos = transform.position;
		startingColliderScale = GetComponentInChildren<MeshCollider>().transform.localScale;
		startingRot = transform.rotation;
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (!isEnabled)
			return;

		rig.AddTorque(Vector3.up * steerForce * Input.GetAxis("Horizontal"));
		
		rig.AddTorque(transform.right * staticAcceleration);


		if (Input.GetButtonDown("Jump")){
			rig.AddForce(Vector3.up * jumpForce);
		}

		CheckDeath();
	}

	void CheckDeath(){

		float angle = Vector3.Angle(Vector3.up, transform.right) -90;//transform.rotation.eulerAngles.z - 90;
																	 //Debug.Log(transform.rotation.eulerAngles);
		angle *= Mathf.Sign(angle);

		//Debug.Log(angle);
		if (angle > deathAngle){
			Debug.Log("Dead!");

			isEnabled = false;
			GetComponentInChildren<MeshCollider>().transform.localScale = Vector3.one;

			GetComponent<AudioSource>().Pause();
		}
	}

	[Button]
	private void Reset() {
		isEnabled = true;
		transform.position = startingPos;
		transform.rotation = startingRot;
		GetComponentInChildren<MeshCollider>().transform.localScale = startingColliderScale;
		rig.velocity = Vector3.zero;
	}
	private void OnCollisionEnter(Collision collision) {
		Debug.Log("Ding!");
		if (collision.collider.gameObject.CompareTag("Floor")) {
			GetComponent<AudioSource>().Play();
		}
	}
	private void OnCollisionExit(Collision collision) {
		Debug.Log(collision.collider.gameObject.tag);
		if(collision.collider.gameObject.CompareTag("Floor")){
			GetComponent<AudioSource>().Pause();
		}
	}
}
