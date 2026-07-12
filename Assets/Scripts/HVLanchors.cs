using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HVLanchors : MonoBehaviour {

	//this code is attached to the Gameobject HVL in the HVL scene, it controls all the unique functionailty in the HVL scene calculating dose when each sheet of Al is placed in the beam

	public Transform Source;
	public Transform Table;
	float distTable;
	float kVError;

	public Text DoseTxt;
	public Text DoseTxt1;

	public GameObject AL1;
	public GameObject AL2;
	public GameObject AL3;
	public GameObject AL4;
	public GameObject AL5;
	public GameObject AL6;
	public Transform AL1a;
	public Transform AL2a;
	public Transform AL3a;
	public Transform AL4a;
	public Transform AL5a;
	public Transform AL6a;

	public Transform AL1ab;
	public Transform AL2ab;
	public Transform AL3ab;
	public Transform AL4ab;
	public Transform AL5ab;
	public Transform AL6ab;

	public GameObject AL1s;
	public GameObject AL2s;
	public GameObject AL3s;
	public GameObject AL4s;
	public GameObject AL5s;

	public Transform AL1as;
	public Transform AL2as;
	public Transform AL3as;
	public Transform AL4as;
	public Transform AL5as;

	public Transform AL1abs;
	public Transform AL2abs;
	public Transform AL3abs;
	public Transform AL4abs;
	public Transform AL5abs;

	Color original;

	bool[] Alb;
	bool[] Alsb;

	public float AlThickness;
	public float Dose;
	public float dose1;

	Scene currentScene;
	string sceneName;

	// Use this for initialization
	void Start () 
	{

		//scene manager
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

		Alb = new bool[6];
		Alsb = new bool[5];
		for (int i = 0; i < Alb.Length; i++) 
		{
			Alb [i] = false;
		}
		for (int j = 0; j < Alsb.Length; j++) 
		{
			Alsb [j] = false;
		}

		AlThickness = 0;

		original = AL1.GetComponent<Renderer> ().material.color;
	}
	
	// Update is called once per frame
	void Update () {




		float distAL1 = Vector3.Distance (AL1.transform.position, AL1a.position);
		float distAL2 = Vector3.Distance (AL2.transform.position, AL2a.position);
		float distAL3 = Vector3.Distance (AL3.transform.position, AL3a.position);
		float distAL4 = Vector3.Distance (AL4.transform.position, AL4a.position);
		float distAL5 = Vector3.Distance (AL5.transform.position, AL5a.position);
		float distAL6 = Vector3.Distance (AL6.transform.position, AL6a.position);

		float distAL1b = Vector3.Distance (AL1.transform.position, AL1ab.position);
		float distAL2b = Vector3.Distance (AL2.transform.position, AL2ab.position);
		float distAL3b = Vector3.Distance (AL3.transform.position, AL3ab.position);
		float distAL4b = Vector3.Distance (AL4.transform.position, AL4ab.position);
		float distAL5b = Vector3.Distance (AL5.transform.position, AL5ab.position);
		float distAL6b = Vector3.Distance (AL6.transform.position, AL6ab.position);

		float distAL1s = Vector3.Distance (AL1s.transform.position, AL1as.position);
		float distAL2s = Vector3.Distance (AL2s.transform.position, AL2as.position);
		float distAL3s = Vector3.Distance (AL3s.transform.position, AL3as.position);
		float distAL4s = Vector3.Distance (AL4s.transform.position, AL4as.position);
		float distAL5s = Vector3.Distance (AL5s.transform.position, AL5as.position);

		float distAL1bs = Vector3.Distance (AL1s.transform.position, AL1abs.position);
		float distAL2bs = Vector3.Distance (AL2s.transform.position, AL2abs.position);
		float distAL3bs = Vector3.Distance (AL3s.transform.position, AL3abs.position);
		float distAL4bs = Vector3.Distance (AL4s.transform.position, AL4abs.position);
		float distAL5bs = Vector3.Distance (AL5s.transform.position, AL5abs.position);

		if (distAL1 <= 0.05) 
		{
			Alb[0] = true;
			AL1.transform.position = AL1a.position;
			AL1.transform.rotation = AL1a.rotation;
			AL1.GetComponent<Renderer> ().material.color = Color.white;
		} 
		else
		{
			Alb[0] = false;
			AL1.GetComponent<Renderer> ().material.color = original;
		}

		if (distAL1b <= 0.05) 
		{
			AL1.transform.position = AL1ab.position;
			AL1.transform.rotation = AL1ab.rotation;
		}

		if (distAL2 <= 0.05) 
		{
			Alb[1] = true;
			AL2.transform.position = AL2a.position;
			AL2.transform.rotation = AL2a.rotation;
			AL2.GetComponent<Renderer> ().material.color = Color.white;
		} else 
		{
			Alb[1] = false;
			AL2.GetComponent<Renderer> ().material.color = original;
		}

		if (distAL2b <= 0.05) 
		{
			AL2.transform.position = AL2ab.position;
			AL2.transform.rotation = AL2ab.rotation;
		}

		if (distAL3 <= 0.05) 
		{
			Alb[2] = true;
			AL3.transform.position = AL3a.position;
			AL3.transform.rotation = AL3a.rotation;
			AL3.GetComponent<Renderer> ().material.color = Color.white;
		} else 
		{
			Alb[2] = false;
			AL3.GetComponent<Renderer> ().material.color = original;
		}

		if (distAL3b <= 0.05) 
		{
			AL3.transform.position = AL3ab.position;
			AL3.transform.rotation = AL3ab.rotation;
		}

		if (distAL4 <= 0.05) 
		{
			Alb[3] = true;
			AL4.transform.position = AL4a.position;
			AL4.transform.rotation = AL4a.rotation;
			AL4.GetComponent<Renderer> ().material.color = Color.white;
		} else 
		{
			Alb[3] = false;
			AL4.GetComponent<Renderer> ().material.color = original;
		}

		if (distAL4b <= 0.05) 
		{
			AL4.transform.position = AL4ab.position;
			AL4.transform.rotation = AL4ab.rotation;
		}

		if (distAL5 <= 0.05) 
		{
			Alb[4] = true;
			AL5.transform.position = AL5a.position;
			AL5.transform.rotation = AL5a.rotation;
			AL5.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL5.GetComponent<Renderer> ().material.color =original;
			Alb[4] = false;}

		if (distAL5b <= 0.05) 
		{
			AL5.transform.position = AL5ab.position;
			AL5.transform.rotation = AL5ab.rotation;
		}

		if (distAL6 <= 0.05) 
		{
			Alb[5] = true;
			AL6.transform.position = AL6a.position;
			AL6.transform.rotation = AL6a.rotation;
			AL6.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL6.GetComponent<Renderer> ().material.color =original;
			Alb[5] = false;}

		if (distAL6b <= 0.05) 
		{
			AL6.transform.position = AL6ab.position;
			AL6.transform.rotation = AL6ab.rotation;
		}

		if (distAL1s <= 0.1) 
		{
			Alsb[0] = true;
			AL1s.transform.position = AL1as.position;
			AL1s.transform.rotation = AL1as.rotation;
			AL1s.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL1s.GetComponent<Renderer> ().material.color = original;
			Alsb[0] = false;}

		if (distAL1bs <= 0.1) 
		{
			AL1s.transform.position = AL1abs.position;
			AL1s.transform.rotation = AL1abs.rotation;
		}

		if (distAL2s <= 0.1) 
		{
			Alsb[1] = true;
			AL2s.transform.position = AL2as.position;
			AL2s.transform.rotation = AL2as.rotation;
			AL2s.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL2s.GetComponent<Renderer> ().material.color = original;
			Alsb[1] = false;}

		if (distAL2bs <= 0.1) 
		{
			AL2s.transform.position = AL2abs.position;
			AL2s.transform.rotation = AL2abs.rotation;
		}

		if (distAL3s <= 0.1) 
		{
			Alsb[2] = true;
			AL3s.transform.position = AL3as.position;
			AL3s.transform.rotation = AL3as.rotation;
			AL3s.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL3s.GetComponent<Renderer> ().material.color = original;
			Alsb[2] = false;}

		if (distAL3bs <= 0.1) 
		{
			AL3s.transform.position = AL3abs.position;
			AL3s.transform.rotation = AL3abs.rotation;
		}

		if (distAL4s <= 0.1) 
		{
			Alsb[3] = true;
			AL4s.transform.position = AL4as.position;
			AL4s.transform.rotation = AL4as.rotation;
			AL4s.GetComponent<Renderer> ().material.color = Color.white;

		}  else {
			AL4s.GetComponent<Renderer> ().material.color = original;
			Alsb[3] = false;}

		if (distAL4bs <= 0.1) 
		{
			AL4s.transform.position = AL4abs.position;
			AL4s.transform.rotation = AL4abs.rotation;
		}

		if (distAL5s <= 0.1) 
		{
			Alsb[4] = true;
			AL5s.transform.position = AL5as.position;
			AL5s.transform.rotation = AL5as.rotation;
			AL5s.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			AL5s.GetComponent<Renderer> ().material.color = original;
			Alsb[4] = false;}

		if (distAL5bs <= 0.1) 
		{
			AL5s.transform.position = AL5abs.position;
			AL5s.transform.rotation = AL5abs.rotation;
		}



		if (Input.GetButtonDown ("Scan")) 
		{
			CalcOutput ();
		}




	}


	public void CalcOutput()
	{
		for (int k = 0; k < Alb.Length; k++) 
		{
			if (Alb [k] == true) {AlThickness = AlThickness + 1f;}
		}
		for (int l = 0; l < Alsb.Length; l++) 
		{
			if (Alsb [l] == true) {AlThickness = AlThickness + 0.2f;}
		}

		distTable = Vector3.Distance (Source.position, Table.position);

		//Modify attenuation coefficient of Al for different kV settings
		float muRef = 0.21f;
		float kVRef = 70f; //Reference kV value on which the empirically derived attenuation coefficient was based

		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1.0f) 
		{
			if (sceneName == "HVL Xray Room Oculus Touch")
			{
				kVError = 1.5f;
			}
		}
		else 
		{
			kVError = 1;
		}


		float mukV = muRef * (Mathf.Pow (kVRef, 3) / Mathf.Pow (((float)DetectTouch.kV * kVError), 3));

		dose1 = ((1050 * Mathf.Exp (-mukV * AlThickness))/(distTable*distTable));

		//modify dose for set mAs kV
		int mAsRef = 10; //mAs value on which the empirical data was based
		//dose is proportional to mAs
		float dose2 = dose1 * (float)DetectTouch.mAs / (float)mAsRef;
		//dose increases with kV^2


		float dose3 = dose2 * (Mathf.Pow (((float)DetectTouch.kV * kVError), 2) / Mathf.Pow ((kVRef * kVError), 2));


		Dose = dose3 * Random.Range (0.975f, 1.025f);



		//Dose = ((1050 * Mathf.Exp (-0.21f * AlThickness));
		DoseTxt.text = string.Concat ("Dose: ", Dose.ToString ("F2"), "uGy");
		DoseTxt1.text = string.Concat ("Dose: ", Dose.ToString ("F2"), "uGy");

		Dose = 0;
		AlThickness = 0;
	
	}
}
