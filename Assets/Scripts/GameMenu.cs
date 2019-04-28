using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenu : MonoBehaviour
{
	PlayerController player;
	public bool prephaseActive;
	public GameObject prephase;
	public GameObject gamephase;

	public TextMeshProUGUI score;

	Vector3 camStartPos;

	void Start() {
		player = FindObjectOfType<PlayerController>();
		prephaseActive = true;


		camStartPos = Camera.main.transform.position;
	}

	private void Update() {


		if(Input.anyKeyDown){
			if (!PlayerController.isAlive) {
				if(Input.GetKeyDown(KeyCode.Escape)){
					Application.Quit(1);
					return;
				}
				ResetGame();
			} else {
				prephaseActive = false;
			}
		}

		UpdateState();

		score.text = Mathf.RoundToInt(ScoreSystem.score) + "m";
	}

	void ResetGame(){
		prephaseActive = true;

		player.ResetPlayer();
		Camera.main.transform.position = camStartPos;
		Camera.main.GetComponent<CameraController>().rotateAround = true;

		EndlessController.instance.ResetWorld();
	}

	void UpdateState(){
		if(!PlayerController.isAlive){
			prephase.SetActive(false);
			gamephase.SetActive(false);
			return;
		}

		player.GetComponent<Rigidbody>().isKinematic = prephaseActive;
		Camera.main.GetComponent<CameraController>().rotateAround = prephaseActive;
		prephase.SetActive(prephaseActive);
		gamephase.SetActive(!prephaseActive);
	}
}
