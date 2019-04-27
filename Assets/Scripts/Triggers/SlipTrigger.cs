using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlipTrigger", menuName = "Trigger/Slip")]
public class SlipTrigger : BaseTriggerEffect {
	[MinMaxSlider(0,5)]
	public Vector2 slipAmount;

	public bool enableWaterParticles;

	public override void TriggerEnter(PlayerController player){
		if(enableWaterParticles){
			var e = player.water.emission;
			e.enabled = true;
		}
	}
	public override void Trigger(PlayerController player) {
		player.transform.Rotate( Vector3.up,
		 Random.Range(slipAmount.x, slipAmount.y));
	}
	public override void TriggerExit(PlayerController player) {
		if (enableWaterParticles) {
			var e = player.water.emission;
			e.enabled = false;
		}
	}
}
