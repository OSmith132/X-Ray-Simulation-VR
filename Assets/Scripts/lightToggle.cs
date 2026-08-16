using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour

{
	private Light XrayLight;
	private bool  onoff;


	// Use this for initialization
	void Start ()
	{
		XrayLight = GetComponent<Light>();
		XrayLight.intensity = 0;
	}
	


	public void toggleLight()

	{
		onoff = !onoff;
		if (onoff)
		{
			XrayLight.intensity = 10;
		}
		else
		{
			XrayLight.intensity = 0;
		}

	}



}
