using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeSpinTouch : MonoBehaviour 
{
	//public float minAngle=0.0f;
	//public float maxAngle=3600.0f;
	public float increase;
	public float slowDown;


	GameObject rightHand;

	// Use this for initialization
	void Start () 
	{
		rightHand = GameObject.Find ("RightHandAnchor");
	}

	// Update is called once per frame
	void Update () 
	{
		if (rightHand.GetComponent<DetectTouch> ().primedown == 1) 
		{
			slowDown = 0;
			increase += (Time.deltaTime*10f);
			transform.Rotate (Vector3.up, increase);

			if (increase >= 20)
			{
				increase = 20;
			}
		}


//		if (Input.GetButtonDown ("Prime"))
//		{
//			slowDown = 0;
//			increase += (Time.deltaTime*10f);
//
//			Debug.Log (increase);
//			transform.Rotate (Vector3.up, increase);
//
//			if (increase >= 20)
//			{
//				increase = 20;
//			}


			//float angle = Mathf.LerpAngle (0.0f, 3600.0f, 2f);
			//Debug.Log (angle);
			//transform.Rotate (0, 3600 * Time.deltaTime, 0);
			//transform.Rotate(0f,angle,0f);
//		}

		if (rightHand.GetComponent<DetectTouch> ().primedown == 0) 
		{
			slowDown = 1;
		}


//		if (Input.GetButtonUp ("Prime"))
//		{
//			slowDown = 1;
//		}

		if (slowDown == 1)
		{
			increase -= 0.08f;
			if (increase <= 0)
			{
				increase = 0f;
			}
			transform.Rotate (Vector3.up, increase);

		}

	}
}
