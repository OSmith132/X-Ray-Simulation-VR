using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeSpinTouch : MonoBehaviour 
{

	public float increase;
	public float slowDown;


	[SerializeField] private DetectTouch rightHandTouch;


	// Update is called once per frame
	void Update()
	{
		if (rightHandTouch.primedown == 1)
		{
			slowDown = 0;
			increase += (Time.deltaTime * 10f);
			transform.Rotate(Vector3.up, increase);

			if (increase >= 20)
			{
				increase = 20;
			}
		}


		if (rightHandTouch.primedown == 0)
		{
			slowDown = 1;
		}






		if (slowDown == 1 && increase > 0)
		{
			increase = Mathf.Max(0f, increase - 0.08f * Time.deltaTime);
			transform.Rotate(Vector3.up, increase * Time.deltaTime);
		}


	}

	}
