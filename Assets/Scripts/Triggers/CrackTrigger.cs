using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrackTrigger", menuName = "Trigger/CrackTrigger")]
public class CrackTrigger : BaseTriggerEffect {
	[MinMaxSlider(0,100)]
	public Vector2 jumpAmount;

	public override void TriggerEnter(PlayerController player) {
		player.GetComponent<Rigidbody>().AddForce(
		(Vector3.up + Vector3.right * 1.5f) * Random.Range(jumpAmount.x, jumpAmount.y));
	}
	public override void Trigger(PlayerController player) {
	}
	public override void TriggerExit(PlayerController player) {

	}
}
