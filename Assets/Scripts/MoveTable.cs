using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.localEulerAngles = new Vector3 (0, 0, 90);
		transform.localPosition = new Vector3 (42.84615f, 40f, 25.12308f);
		
	}
}
