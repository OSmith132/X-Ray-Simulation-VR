using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anodeSpin : MonoBehaviour 

{
	float increase;
	float slowDown;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (slowDown == 1) 
		{
			increase += (Time.deltaTime * 4f);

			transform.Rotate (Vector3.up, increase);

			if (increase >= 20)
			{
				increase = 20;
			}
		}


		if (increase == 20) 
		{
			slowDown = 0;
		}

		if (slowDown == 0) 
		{
			increase -= 0.008f;
			if (increase <= 0) 
			{
				increase = 0f;
			}
			transform.Rotate (Vector3.up, increase);
		}

	}

	public void Spin ()
	{
		slowDown = 1;
	}
}
