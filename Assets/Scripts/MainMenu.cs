using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame(){
		SceneManager.LoadScene("Level0", LoadSceneMode.Single);
		SceneManager.LoadScene("Player", LoadSceneMode.Additive);
	}
}
