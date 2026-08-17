using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col1Move : MonoBehaviour {
	public GameObject Col1;
    float xStart = 0.068664f;
    float xEnd = 0.051f;
    float xPos;
	float speed = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float h =0 * Time.deltaTime;
		transform.Translate(-h * Time.deltaTime * speed, 0f, 0f);
		Col1.transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, 0.051f, 0.068664f), -0.1261682f, 0.06924f);
	}

	public void Collimator1Move(float h){

        xPos = xStart + ((xEnd - xStart) * h);
		Col1.transform.localPosition = new Vector3 ( xPos, -0.12616f, 0.06924f);
		Col1.transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, 0.051f, 0.068664f), -0.1261682f, 0.06924f);
	}
}
