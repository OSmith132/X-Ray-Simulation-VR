using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntiRotation : MonoBehaviour {

	public GameObject VerticalButton;
	public GameObject FreeButton;
	Color OriginalUnpressed;
	Color OriginalUnpressed1;

	public Transform targetPosVert;


	private bool onoff;
	private bool onoff2;



	GameObject rightHand;

	float step;
	float speed = 0.5f;

	// Use this for initialization
	void Start () {
		OriginalUnpressed = FreeButton.GetComponent<Renderer> ().material.color;
		gameObject.GetComponent<MeshCollider> ().enabled = false;

		rightHand = GameObject.Find ("RightHandAnchor");
		//DetectTouch detectTouch = rightHand.GetComponent<DetectTouch> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.eulerAngles = new Vector3 (0, 0, 0);
		//transform.localPosition = new Vector3 (0.035f, Mathf.Clamp (transform.localPosition.y, 0.354f, 0.903f), 0.0697f);
		step = speed * Time.deltaTime;

		if (rightHand.GetComponent<DetectTouch>().controlF == 1) 
		{
			gameObject.GetComponent<MeshCollider> ().enabled = true;
			transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.Clamp (transform.localPosition.y, 0.335f, 1.037f), transform.localPosition.z);
			FreeButton.GetComponent<Renderer> ().material.color = Color.green;
			VerticalButton.GetComponent<Renderer> ().material.color = OriginalUnpressed;
			rightHand.GetComponent<DetectTouch>().controlV = 0;
		}

		if (rightHand.GetComponent<DetectTouch> ().returntocent == 1) 
		{
			transform.position = Vector3.MoveTowards (transform.position, targetPosVert.position, step);

			if (transform.position.x == targetPosVert.position.x)
			{
				rightHand.GetComponent<DetectTouch> ().returntocent = 0;
			}
		}

		if (rightHand.GetComponent<DetectTouch>().controlV == 1 && rightHand.GetComponent<DetectTouch> ().returntocent == 0)
		{
			gameObject.GetComponent<MeshCollider> ().enabled = true;
			transform.localPosition = new Vector3 (0.035f, Mathf.Clamp (transform.localPosition.y, 0.335f, 1.037f), 0.0697f);
			FreeButton.GetComponent<Renderer> ().material.color = OriginalUnpressed;
			VerticalButton.GetComponent<Renderer> ().material.color = Color.green;
			rightHand.GetComponent<DetectTouch>().controlF = 0;
		}

	}


}
