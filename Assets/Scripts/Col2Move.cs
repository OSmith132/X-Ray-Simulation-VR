using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col2Move : MonoBehaviour {
	public GameObject Col2;
    float xPos2;
	float xStart2 = 0.007949414f;
    float xEnd2 = 0.0244f;
	float speed=0.5f;
    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
		float h = 0 * Time.deltaTime;
		transform.Translate(h * Time.deltaTime * speed, 0f, 0f);
		Col2.transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, 0.007949414f, 0.0244f), -0.1261682f, 0.06924f);

	}

	public void Collimator2Move(float h){

        xPos2 = xStart2 + ((xEnd2 - xStart2) * h);
        Col2.transform.localPosition = new Vector3 ( xPos2, -0.12616f, 0.06924f);
		Col2.transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x,0.007949414f, 0.0244f), -0.1261682f, 0.06924f);
	}
}