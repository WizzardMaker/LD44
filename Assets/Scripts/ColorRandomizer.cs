using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
	public int materialSlot;
	MeshRenderer mr;

    // Start is called before the first frame update
    void Start() {
		mr = GetComponent<MeshRenderer>();

		mr.materials[materialSlot].color = Color.HSVToRGB(Random.Range(0, 100) / 100f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
