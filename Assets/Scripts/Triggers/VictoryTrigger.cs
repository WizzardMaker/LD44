using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VictoryTrigger", menuName = "Trigger/Victory")]
public class VictoryTrigger : BaseTriggerEffect {
	public override void TriggerEnter(PlayerController player){
		Debug.Log("Victory!");
		//TODO - Add victory screen
	}
	public override void Trigger(PlayerController player) {

	}
	public override void TriggerExit(PlayerController player) {

	}
}
