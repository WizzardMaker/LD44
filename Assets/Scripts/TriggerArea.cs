using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using NaughtyAttributes;

public class TriggerArea : MonoBehaviour
{
	[ReorderableList]
	public List<BaseTriggerEffect> effectsOnTrigger;

	private void OnTriggerEnter(Collider other) {
		Trigger(other,
		effectsOnTrigger.Select<BaseTriggerEffect, Action<PlayerController>>
		((e, b) => e.TriggerEnter).ToList());
	}
	private void OnTriggerStay(Collider other) {
		Trigger(other,
		effectsOnTrigger.Select<BaseTriggerEffect, Action<PlayerController>>
		((e, b) => e.Trigger).ToList());
	}
	private void OnTriggerExit(Collider other) {
		Trigger(other,
		effectsOnTrigger.Select<BaseTriggerEffect, Action<PlayerController>>
		( (e, b) => e.TriggerExit).ToList() );
	}

	private void Trigger(Collider c, List<Action<PlayerController>> thens){
		if (!PlayerController.isEnabled)
			return;

		PlayerController p = c.GetComponentsInParent<PlayerController>().FirstOrDefault();
		if (p != null) {
			foreach (var a in thens) {
				a(p);
			}
		}
	}
}
