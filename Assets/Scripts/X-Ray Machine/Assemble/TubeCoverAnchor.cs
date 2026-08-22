using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeCoverAnchor : MonoBehaviour {

	public Transform TubeCover;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (TubeCover.position, transform.position);
		//print (dist);

		if (dist <=1) {
			TubeCover.position = transform.position;
			TubeCover.rotation = transform.rotation;
		}

		}
		

}
