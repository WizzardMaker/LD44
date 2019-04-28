using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
	public int cleanDistance = 3;
	public int apperanceCooldown = 3;
	public int laneCooldown = 5;

	[HideInInspector]
	public int spawnedPosition;

	public Vector3 spawnOffset;
}
