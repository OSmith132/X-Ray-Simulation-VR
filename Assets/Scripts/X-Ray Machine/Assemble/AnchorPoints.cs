using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class AnchorPoints : MonoBehaviour
{

	float tubePlaced;
	float glassPlaced;
	float coilPlaced;
	float anodePlaced;
	float cathodePlaced;
	float rotorPlaced;
	//float statorPlaced;
	float collimatorPlaced;
	float bePlaced;
	float panelPlaced;

	private bool labelsHidden;

	public Transform Tube;
	public Transform TubeAnchor;
	public Transform Glass;
	public Transform GlassAnchor;
	public Transform Coil;
	public Transform CoilAnchor;
	public Transform Anode;
	public Transform AnodeAnchor;
	public Transform Cathode;
	public Transform CathodeAnchor;
	public Transform Rotor;
	public Transform RotorAnchor;
	//public Transform Stator;
	//public Transform StatorAnchor;
	public Transform Collimator;
	public Transform CollimatorAnchor;
	public Transform Be;
	public Transform BeAnchor;
	public Transform Panel;
	public Transform PanelAnchor;

	public GameObject Completion;
	public GameObject Instructions;
	public GameObject CriticalInspection;

	public GameObject AnodeLabel;
	public GameObject CathodeLabel;
	public GameObject RotorLabel;
	//public GameObject StatorLabel;
	public GameObject CoilLabel;
	public GameObject GlassLabel;
	public GameObject PanelLabel;
	public GameObject CollimatorLabel;
	public GameObject CoverLabel;
	public GameObject BeLabel;

	//public GameObject AnodeRef;
	//public GameObject CathodeRef;
	//public GameObject RotorRef;
	//public GameObject StatorRef;
	//public GameObject CoilRef;
	//public GameObject GlassRef;
	//public GameObject PanelRef;
	//public GameObject CollimatorRef;
	//public GameObject CoverRef;
	//public GameObject BeRef;

	//GameObject FinalText;

	Color defaultColor;

	bool freeAnodeRotation;

	// Use this for initialization
	void Start()
	{

		defaultColor = GetComponent<MeshRenderer>().material.color;

		//FinalText = GameObject.Find ("FinalText");

		GetComponent<MeshRenderer>().material.color = Color.grey;
		GetComponent<XRGrabInteractable>().enabled = false;


		CriticalInspection.GetComponent<MeshRenderer>().material.color = Color.grey;
		CriticalInspection.GetComponent<XRGrabInteractable>().enabled = false;


		// parts 
		AnodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		CathodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		RotorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		CoilLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		GlassLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		PanelLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		CollimatorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		CoverLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		BeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;

		Instructions.gameObject.GetComponent<MeshRenderer>().enabled = true;
		Completion.gameObject.GetComponent<MeshRenderer>().enabled = false;

		freeAnodeRotation = false;

	}

	// Update is called once per frame
	void Update()
	{

		float distTube = Vector3.Distance(Tube.position, TubeAnchor.position);
		float distGlass = Vector3.Distance(Glass.position, GlassAnchor.position);
		float distCoil = Vector3.Distance(Coil.position, CoilAnchor.position);
		float distAnode = Vector3.Distance(Anode.position, AnodeAnchor.position);
		float distCathode = Vector3.Distance(Cathode.position, CathodeAnchor.position);
		float distRotor = Vector3.Distance(Rotor.position, RotorAnchor.position);
		//float distStator = Vector3.Distance (Stator.position, StatorAnchor.position);
		float distCollimator = Vector3.Distance(Collimator.position, CollimatorAnchor.position);
		float distBe = Vector3.Distance(Be.position, BeAnchor.position);
		float distPanel = Vector3.Distance(Panel.position, PanelAnchor.position);


		//if (Input.GetButtonDown("Label"))
		//{
		//	labelsHidden = !labelsHidden;
		//	if (labelsHidden)
		//	{
		//		AnodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		CathodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		RotorLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		//		CoilLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		GlassLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		PanelLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		CollimatorLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		CoverLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//		BeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		//	}
		//	else
		//	{
		//		AnodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		CathodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		RotorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		//		CoilLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		GlassLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		PanelLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		CollimatorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		CoverLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//		BeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		//	}
		//}





		


		// Distances of objects from their positions:


		if (distTube <= 0.05)
		{
			Tube.position = TubeAnchor.position;
			Tube.rotation = TubeAnchor.rotation;

			tubePlaced = 1;
			CoverLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			tubePlaced = 0;
			CoverLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}


		if (distGlass <= 0.05)
		{
			Glass.position = GlassAnchor.position;
			Glass.rotation = GlassAnchor.rotation;

			glassPlaced = 1;
			GlassLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			glassPlaced = 0;
			GlassLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}



		if (distCoil <= 0.05)
		{
			Coil.position = CoilAnchor.position;
			Coil.rotation = CoilAnchor.rotation;

			coilPlaced = 1;
			CoilLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;

		}
		else
		{
			coilPlaced = 0;
			CoilLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}



		if (distAnode <= 0.05)
		{

			Anode.position = AnodeAnchor.position;
			if (!freeAnodeRotation) { Anode.rotation = AnodeAnchor.rotation; }

			anodePlaced = 1;
			AnodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			anodePlaced = 0;
			AnodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}



		if (distCathode <= 0.05)
		{
			Cathode.position = CathodeAnchor.position;
			Cathode.rotation = CathodeAnchor.rotation;
			cathodePlaced = 1;
			CathodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			cathodePlaced = 0;
			CathodeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}



		if (distRotor <= 0.05)
		{
			Rotor.position = RotorAnchor.position;
			Rotor.rotation = RotorAnchor.rotation;

			rotorPlaced = 1;
			RotorLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			rotorPlaced = 0;
			RotorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}


		if (distCollimator <= 0.05)
		{
			Collimator.position = CollimatorAnchor.position;
			Collimator.rotation = CollimatorAnchor.rotation;

			collimatorPlaced = 1;
			CollimatorLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			collimatorPlaced = 0;
			CollimatorLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}


		if (distBe <= 0.05)
		{
			Be.position = BeAnchor.position;
			Be.rotation = BeAnchor.rotation;

			bePlaced = 1;
			BeLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			bePlaced = 0;
			BeLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}





		if (distPanel <= 0.05)
		{
			Panel.position = PanelAnchor.position;
			Panel.rotation = PanelAnchor.rotation;

			panelPlaced = 1;
			PanelLabel.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			panelPlaced = 0;
			PanelLabel.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}





		// When completed
		if (tubePlaced == 1 && glassPlaced == 1 && coilPlaced == 1 && anodePlaced == 1 && cathodePlaced == 1 && rotorPlaced == 1 && collimatorPlaced == 1 && bePlaced == 1 && panelPlaced == 1)
		{
			GetComponent<MeshRenderer>().material.color = defaultColor;
			GetComponent<XRGrabInteractable>().enabled = true;

			CriticalInspection.GetComponent<MeshRenderer>().material.color = defaultColor;
			CriticalInspection.GetComponent<XRGrabInteractable>().enabled = true;

			this._FixPositions(); // So objects can't be grabbed

			freeAnodeRotation = true; // To allow for the anode to spin during testing

			Completion.gameObject.GetComponent<MeshRenderer>().enabled = true;
			Instructions.gameObject.GetComponent<MeshRenderer>().enabled = false;

			

		}
	}



	void _FixPositions()
	{
		
		Tube.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Glass.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Coil.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Anode.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Cathode.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Rotor.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Collimator.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Be.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
		Panel.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
	}





	void _FreePositions()
	{

		Tube.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Glass.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Coil.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Anode.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Cathode.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Rotor.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Collimator.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Be.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
		Panel.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
	}


}