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
		XrayLight.intensity = 5;
	}
	


	public void toggleLight()

	{
		onoff = !onoff;
		if (onoff)
		{
			XrayLight.intensity = 5;
		}
		else
		{
			XrayLight.intensity = 0;
		}

	}


	public void TurnOn()
	{
		XrayLight.intensity = 5;
	}

	public void TurnOff()
	{
		XrayLight.intensity = 0;
	}



}
