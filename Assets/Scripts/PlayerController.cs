using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour {
	Rigidbody rig;

	Vector3 startingPos;
	Vector3 startingColliderScale;
	Quaternion startingRot;

	public static bool isAlive = true;

	public float steerForce;
	public float staticAcceleration;
	public float startAcceleration;
	public float jumpForce;

	public ParticleSystem water;

	float deathTimer = 0, endTime = 5;


	public float deathAngle;
	public float inverseMovementAngle;

	public float turnSpeed;
	float turn;
	// Start is called before the first frame update
	void Start() {
		rig = GetComponent<Rigidbody>();

		rig.maxAngularVelocity = 16f;
		rig.centerOfMass = Vector3.zero;

		water = GetComponentInChildren<ParticleSystem>();
		rig.AddTorque(transform.right * startAcceleration, ForceMode.VelocityChange);

		startingPos = transform.position;
		startingColliderScale = GetComponentInChildren<MeshCollider>().transform.localScale;
		startingRot = transform.rotation;

	}

	// Update is called once per frame
	void FixedUpdate() {
		if (!isAlive)
			return;


		rig.maxAngularVelocity = 16f * (Input.GetAxis("Vertical") + 1);

		float angle = Vector3.Angle(Vector3.up, transform.right) - 90;//transform.rotation.eulerAngles.z - 90;
																	  //Debug.Log(transform.rotation.eulerAngles);
		angle *= Mathf.Sign(angle);

		turn = Mathf.Lerp(turn, Input.GetAxis("Horizontal"), Time.fixedDeltaTime * turnSpeed);

		rig.AddTorque(Vector3.up * steerForce * turn * (angle > inverseMovementAngle ? -1:1));
		//rig.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * steerForce * Input.GetAxis("Horizontal") * (angle > inverseMovementAngle ? -1 : 1)));
		rig.AddTorque(transform.right * staticAcceleration);


		if (Input.GetButtonDown("Jump")){
			rig.AddForce(Vector3.up * jumpForce);
		}

		var e = water.emission;
		e.enabled = false;

		CheckDeath();
	}

	void CheckDeath(){

		float angle = Vector3.Angle(Vector3.up, transform.right) -90;//transform.rotation.eulerAngles.z - 90;
																	 //Debug.Log(transform.rotation.eulerAngles);
		angle *= Mathf.Sign(angle);

		//Debug.Log(angle);
		if (angle > deathAngle || (Time.time > deathTimer && rig.velocity.magnitude < 0.2f)) {
			Debug.Log("Dead!");

			isAlive = false;
			GetComponentInChildren<MeshCollider>().transform.localScale = Vector3.one;

			GetComponent<AudioSource>().Pause();
		}
	}

	[Button]
	public void ResetPlayer() {
		isAlive = true;
		turn = 0;
		rig.MovePosition(startingPos);
		rig.maxAngularVelocity = 16f;
		transform.rotation = startingRot;
		GetComponentInChildren<MeshCollider>().transform.localScale = startingColliderScale;
		rig.velocity = Vector3.zero;
	}

	public void StartPlayer(){
		deathTimer = Time.time + endTime;
		isAlive = true;
		rig.isKinematic = false;
		rig.AddForce(Vector3.forward * 20, ForceMode.VelocityChange);
		rig.AddTorque(transform.right * startAcceleration, ForceMode.VelocityChange);
	}

	private void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.CompareTag("Floor")) {
			if(!GetComponent<AudioSource>().isPlaying && isAlive)
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
