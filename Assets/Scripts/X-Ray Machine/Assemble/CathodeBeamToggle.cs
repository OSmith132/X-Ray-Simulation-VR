using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathodeBeamToggle : MonoBehaviour 

{
	public GameObject ScanButton;
	Color OGScanButtonColor;
	private Light ElectronBeam;
	private bool onoff;

	GameObject rightHand;

	// Use this for initialization
	void Start () 
	{
		ElectronBeam = GetComponent<Light> ();
		ElectronBeam.intensity = 0;
		OGScanButtonColor = ScanButton.GetComponent<Renderer> ().material.color;

	}
	


	public void toggleBeam()

	{
		onoff = !onoff;

		if (onoff)
		{
			ElectronBeam.intensity = 500;
			ScanButton.GetComponent<Renderer> ().material.color = new Color (0.75f, 0.1f, 0.1f, 0.25f);
		}
		else
		{
			ElectronBeam.intensity = 0;
			ScanButton.GetComponent<Renderer> ().material.color = OGScanButtonColor;
		}
			
	}
		
}
