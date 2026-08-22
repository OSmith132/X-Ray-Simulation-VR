using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col3Move : MonoBehaviour {
	public GameObject Col3;
    float zPos;
    float zStart = 0.0407768f;
    float zEnd = 0.0557f;
	float speed = 0.5f;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
		float h = 0 * Time.deltaTime;
		transform.Translate(-h * Time.deltaTime * speed, 0f, 0f);
		Col3.transform.localPosition = new Vector3 (0.0379142f, -0.12295f, Mathf.Clamp(transform.localPosition.z, 0.0407768f, 0.0557f));

	}

	public void Collimator3Move(float i){


        zPos = zStart + ((zEnd - zStart) * i);
        Col3.transform.localPosition = new Vector3(0.0379142f, -0.12295f, zPos);
        Col3.transform.localPosition = new Vector3 (0.0379142f, -0.12295f, Mathf.Clamp(transform.localPosition.z, 0.0407768f, 0.0557f));
	}
}