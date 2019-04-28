using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenu : MonoBehaviour
{
	PlayerController player;
	public bool prephaseActive, paused;
	public GameObject prephase, gamephase, endphase, pause;

	public TextMeshProUGUI score, highscore;

	Vector3 camStartPos;

	void Start() {
		player = FindObjectOfType<PlayerController>();
		prephaseActive = true;

		PlayerController.isAlive = false;

		camStartPos = Camera.main.transform.position;
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)){
			if (prephaseActive) {
				prephaseActive = false;
				player.StartPlayer();
			} else if (!PlayerController.isAlive) {
				ResetGame();
			} else if (paused){
				paused = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!PlayerController.isAlive || paused) {
				Application.Quit(1);
				return;
			}
			if(!prephaseActive) {
				paused = true;
			}

		}

		UpdateState();

		score.text = Mathf.RoundToInt(ScoreSystem.score/10) + "m";
		highscore.text = "Session Highscore: " + Mathf.RoundToInt(ScoreSystem.highscore / 10) + "m";
	}

	void ResetGame(){
		prephaseActive = true;

		player.ResetPlayer();
		Camera.main.transform.position = camStartPos;
		Camera.main.GetComponent<CameraController>().rotateAround = true;

		EndlessController.instance.ResetWorld();
	}

	void UpdateState(){
		if(!PlayerController.isAlive && !prephaseActive){
			prephase.SetActive(false);
			gamephase.SetActive(false);
			pause.SetActive(false);
			endphase.SetActive(true);
			return;
		}

		if(paused){
			prephase.SetActive(false);
			gamephase.SetActive(false);
			endphase.SetActive(false);
			pause.SetActive(true);

			Time.timeScale = 0;
			player.GetComponent<AudioSource>().Pause();


			return;
		}

		Time.timeScale = 1;

		player.GetComponent<Rigidbody>().isKinematic = prephaseActive;
		Camera.main.GetComponent<CameraController>().rotateAround = prephaseActive;
		prephase.SetActive(prephaseActive);
		endphase.SetActive(false);
		gamephase.SetActive(!prephaseActive);
		pause.SetActive(false);
	}
}
