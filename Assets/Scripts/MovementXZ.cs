using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementXZ : MonoBehaviour {

	float yPosFix;


	// Use this for initialization
	void Start () {

		yPosFix = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3 (0, 0, 0);

		transform.position = new Vector3 (transform.position.x, yPosFix, transform.position.z);
		
	}
}
