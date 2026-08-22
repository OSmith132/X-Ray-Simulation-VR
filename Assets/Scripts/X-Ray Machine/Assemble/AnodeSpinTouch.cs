using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeSpinTouch : MonoBehaviour 
{

	public float increase;
	public float slowDown;






	public void SpeedUp()
	{
		slowDown = 0;
		increase += (Time.deltaTime * 10f);
		transform.Rotate(Vector3.up, increase);
		if (increase >= 20)
		{
			increase = 20;
		}
	}

	public void SlowDown()
	{
		slowDown = 1;
	}




	// Update is called once per frame
	void Update()
	{



		if (slowDown == 1 && increase > 0)
		{
			increase = Mathf.Max(0f, increase - 0.08f * Time.deltaTime);
			transform.Rotate(Vector3.up, increase * Time.deltaTime);
		}


	}

	}
