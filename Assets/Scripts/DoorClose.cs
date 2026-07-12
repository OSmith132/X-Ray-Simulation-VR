using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.eulerAngles = new Vector3 (0, 0, 0);

		transform.position = new Vector3 (3.536f, 1.071f, Mathf.Clamp (transform.position.z, -0.41f, 0.549f));
		
	}
}
