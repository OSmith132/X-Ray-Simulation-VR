using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col4Move : MonoBehaviour {
	public GameObject Col4;
    float zPos2;
    float zStart2 = 0.09851f;
    float zEnd2 = 0.083f;
	float speed = 0.5f;
    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
		float h = 0 * Time.deltaTime;
		transform.Translate(h * Time.deltaTime * speed, 0f, 0f);
		Col4.transform.localPosition = new Vector3 (0.0379142f, -0.12295f, Mathf.Clamp(transform.localPosition.z, 0.083f, 0.09851f));


	}

	public void Collimator4Move(float i){
        zPos2 = zStart2 + ((zEnd2 - zStart2) * i);
        Col4.transform.localPosition = new Vector3(0.0379142f, -0.12295f, zPos2);
        Col4.transform.localPosition = new Vector3 (0.0379142f, -0.12295f, Mathf.Clamp(transform.localPosition.z, 0.083f, 0.09851f));
	}
}