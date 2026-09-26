using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLight : MonoBehaviour

{
	
	private Light XrayLight;
	private bool  onoff;
	private Color OGPrimeButtonColor;


	// Use this for initialization
	void Start ()
	{
		XrayLight = GetComponent<Light>();
		XrayLight.intensity = 3;

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Prime")){
			toggleLight ();
		}
	}

	public void toggleLight()

	{
		onoff = !onoff;
		if (onoff)
		{
			XrayLight.intensity = 3;

		}
		else
		{
			XrayLight.intensity = 0;

		}

	}



}