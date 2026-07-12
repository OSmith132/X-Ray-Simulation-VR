using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour 
{

	//this code is attached to Gameobject Transport in the Menu Secene, it controls navigation in the menu scene and controls fault scoring, faulst are set in Faultcontroller script and then called back into this script where they are tallied up  

	float HVLdist;
	float Assembledist;
	float ISLDist;
	float HVLLoaded;
	float Anatdist;
	float LBAdist;
	float FaultDist;
	float runOnce =1;


	GameObject HVLcube;
	GameObject HVLcubeBase;
	GameObject Assemblecube;
	GameObject AssemblecubeBase;
	GameObject ISLcube;
	GameObject ISLcubeBase;
	GameObject anatcube;
	GameObject anatcubebase;
	GameObject LBAcube;
	GameObject LBAcubebase;
	GameObject Fault;
	GameObject FaultBase;



	// Use this for initialization
	void Start ()

	{
		Debug.Log (PlayerPrefs.GetFloat ("FaultsActivated") + " HVL " + PlayerPrefs.GetFloat ("HVLCorrect"));
//		if (runOnce == 1)
//		{
//			PlayerPrefs.SetFloat ("FaultsActivated", 0.0f);
//			PlayerPrefs.SetFloat ("AssembleCorrect1", 0.0f);
//			PlayerPrefs.SetFloat ("AssembleCorrect2", 0.0f);
//			PlayerPrefs.SetFloat ("HVLCorrect", 0.0f);
//			PlayerPrefs.SetFloat ("DAPCorrect", 0.0f);
//			PlayerPrefs.SetFloat ("PhantomsCorrect1", 0.0f);
//			PlayerPrefs.SetFloat ("PhantomsCorrect2", 0.0f);
//			runOnce = 0;
//		}


		HVLcube = GameObject.Find ("TransportHVL");
		HVLcubeBase = GameObject.Find ("Transport HVL base");
		Assemblecube = GameObject.Find ("Transport build");
		AssemblecubeBase = GameObject.Find ("Transport build base");
		ISLcube = GameObject.Find ("TransportISL");
		ISLcubeBase = GameObject.Find ("Transport ISL base");
		anatcube = GameObject.Find ("Anatomy");
		anatcubebase = GameObject.Find ("Anatomy base");
		LBAcube = GameObject.Find ("LBA");
		LBAcubebase = GameObject.Find ("LBA base");
		Fault = GameObject.Find ("Faults");
		FaultBase = GameObject.Find ("Faults base");

				
	}
	
	// Update is called once per frame
	void Update ()
	{

		HVLdist = Vector3.Distance (HVLcube.transform.position, HVLcubeBase.transform.position);
		Assembledist = Vector3.Distance (Assemblecube.transform.position, AssemblecubeBase.transform.position);
		ISLDist = Vector3.Distance (ISLcube.transform.position, ISLcubeBase.transform.position);
		Anatdist = Vector3.Distance (anatcube.transform.position, anatcubebase.transform.position);
		LBAdist = Vector3.Distance (LBAcube.transform.position, LBAcubebase.transform.position);
		FaultDist = Vector3.Distance (Fault.transform.position, FaultBase.transform.position);


		if (HVLdist >= 0.1) {
			SceneManager.LoadScene ("HVL Xray Room Oculus Touch", LoadSceneMode.Single);
			HVLcube.transform.position = HVLcubeBase.transform.position;
			HVLLoaded = 1;
		}

		if (Assembledist >= 0.1) {
			SceneManager.LoadScene ("Assemble Xray Room Oculus Touch", LoadSceneMode.Single);
			Assemblecube.transform.position = AssemblecubeBase.transform.position;
			HVLLoaded = 0;
		}


		if (ISLDist >= 0.1) {
			SceneManager.LoadScene ("Inverse Square Law Room", LoadSceneMode.Single);
			ISLcube.transform.position = ISLcubeBase.transform.position;
			HVLLoaded = 0;

		}

		if (Anatdist >= 0.1) {
			SceneManager.LoadScene ("HEE Anatomy", LoadSceneMode.Single);
			anatcube.transform.position = anatcubebase.transform.position;
			HVLLoaded = 0;
		}

		if (LBAdist >= 0.1) {
			SceneManager.LoadScene ("HEE Light Field Alignment", LoadSceneMode.Single);
			LBAcube.transform.position = LBAcubebase.transform.position;
			HVLLoaded = 0;
		}

		//Debug.Log (FaultDist);

		if (FaultDist >= 0.1) {
			PlayerPrefs.SetFloat ("FaultsActivated", 1.0f);
			PlayerPrefs.SetFloat ("AssembleCorrect1", 0.0f);
			PlayerPrefs.SetFloat ("AssembleCorrect2", 0.0f);
			PlayerPrefs.SetFloat ("HVLCorrect1", 0.0f);
			PlayerPrefs.SetFloat ("HVLCorrect2", 0.0f);
			PlayerPrefs.SetFloat ("DAPCorrect", 0.0f);
			PlayerPrefs.SetFloat ("PhantomsCorrect1", 0.0f);
			PlayerPrefs.SetFloat ("PhantomsCorrect2", 0.0f);
			Fault.transform.position = FaultBase.transform.position;
			Fault.transform.rotation = FaultBase.transform.rotation;
		}



		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1.0f) 
		{
			
			if (PlayerPrefs.GetFloat ("AssembleCorrect1") == 1 && PlayerPrefs.GetFloat ("AssembleCorrect2") == 1) {
				Assemblecube.GetComponent<MeshRenderer> ().material.color = Color.green;
			} else {
				Assemblecube.GetComponent<MeshRenderer> ().material.color = Color.red;
			}


			if (PlayerPrefs.GetFloat ("HVLCorrect1") == 1 && PlayerPrefs.GetFloat("HVLCorrect2") == 1) {
				HVLcube.GetComponent<MeshRenderer> ().material.color = Color.green;
			} else {
				HVLcube.GetComponent<MeshRenderer> ().material.color = Color.red;
			}


			if (PlayerPrefs.GetFloat ("DAPCorrect") == 1) {
				ISLcube.GetComponent<MeshRenderer> ().material.color = Color.green;

			} else {
				ISLcube.GetComponent<MeshRenderer> ().material.color = Color.red;
			}

			if (PlayerPrefs.GetFloat ("PhantomsCorrect1") == 1 && PlayerPrefs.GetFloat ("PhantomsCorrect2") == 1) {
				LBAcube.GetComponent<MeshRenderer> ().material.color = Color.green;
			} else {
				LBAcube.GetComponent<MeshRenderer> ().material.color = Color.red;
			}

			anatcube.GetComponent<MeshRenderer> ().material.color = Color.grey;
			anatcube.GetComponent<OVRGrabbable> ().enabled = false;
			anatcube.GetComponent<BoxCollider> ().enabled = false;

			float score = PlayerPrefs.GetFloat ("PhantomsCorrect1") + PlayerPrefs.GetFloat ("PhantomsCorrect2") + PlayerPrefs.GetFloat ("DAPCorrect") + PlayerPrefs.GetFloat ("HVLCorrect1") + PlayerPrefs.GetFloat ("HVLCorrect2") + PlayerPrefs.GetFloat ("AssembleCorrect2") + PlayerPrefs.GetFloat ("AssembleCorrect1") ;


			if (score == 7) 
			{
				anatcube.GetComponent<MeshRenderer> ().material.color = Color.green;
				anatcube.GetComponent<OVRGrabbable> ().enabled = true;
				anatcube.GetComponent<BoxCollider> ().enabled = true;

				PlayerPrefs.SetFloat ("FaultsActivated", 0.0f);
			}

		}


	}


}
