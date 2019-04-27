using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(WindTrigger), menuName = "Trigger/" + nameof(WindTrigger))]
public class WindTrigger : BaseTriggerEffect {
	[MinMaxSlider(0,50)]
	public Vector2 pushForce;
	public Vector3 windDirection;

	public override void TriggerEnter(PlayerController player){

	}
	public override void Trigger(PlayerController player) {
		player.GetComponent<Rigidbody>().AddForce(windDirection * pushForce);
	}
	public override void TriggerExit(PlayerController player) {

	}
}
