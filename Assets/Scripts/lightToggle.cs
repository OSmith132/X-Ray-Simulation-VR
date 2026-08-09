using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightToggle : MonoBehaviour

{
	private Light XrayLight;
	private bool  onoff;

	[SerializeField] private DetectTouch rightHandTouch;

	// Use this for initialization
	void Start ()
	{
		XrayLight = GetComponent<Light>();
		XrayLight.intensity = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (rightHandTouch.primedown == 1) {
			toggleLight ();
		}
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
