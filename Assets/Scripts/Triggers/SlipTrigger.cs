using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlipTrigger", menuName = "Trigger/Slip")]
public class SlipTrigger : BaseTriggerEffect {
	[MinMaxSlider(0,5)]
	public Vector2 slipAmount;

	public bool enableWaterParticles;

	public float playerSlowdown;
	public override void TriggerEnter(PlayerController player){
		player.staticAcceleration -= playerSlowdown;
	}
	public override void Trigger(PlayerController player) {
		player.transform.Rotate( Vector3.up + Vector3.right*0.1f,
			Random.Range(slipAmount.x, slipAmount.y));

		if (enableWaterParticles) {
			var e = player.water.emission;
			e.enabled = true;
		}
	}
	public override void TriggerExit(PlayerController player) {
		if (enableWaterParticles) {
			var e = player.water.emission;
			e.enabled = false;
		}
		player.staticAcceleration += playerSlowdown;
	}
}
