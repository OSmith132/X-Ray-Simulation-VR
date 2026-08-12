using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaultController : MonoBehaviour {

	// THIS SCRIPT IS ATTACHED TO OVRCAMERARIG in all scenes and controls faults throughout the whole project using playerprefs, these are also called in MenuControl.cs

	float OfficeCubeAdist;
	float OfficeCubeBdist;
	float OfficeCubeCdist;
	float OfficeCubeDdist;
	float OfficeCubeEdist;
	float OfficeCubeFdist;
	float OfficeCube2Adist;
	float OfficeCube2Bdist;
	float OfficeCube2Cdist;
	float OfficeCube2Ddist;
	float OfficeCube2Edist;
	float FaultDist;


	float score1;
	float score2;
	float score3;
	float score4;


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

	//Fault cubes
	GameObject OfficeCubeA;
	GameObject OfficeCubeARef;
	GameObject OfficeCubeB;
	GameObject OfficeCubeBRef;
	GameObject OfficeCubeC;
	GameObject OfficeCubeCRef;
	GameObject OfficeCubeD;
	GameObject OfficeCubeDRef;
	GameObject OfficeCubeE;
	GameObject OfficeCubeERef;
	GameObject OfficeCubeF;
	GameObject OfficeCubeFRef;
	GameObject OfficeCube2A;
	GameObject OfficeCube2ARef;
	GameObject OfficeCube2B;
	GameObject OfficeCube2BRef;
	GameObject OfficeCube2C;
	GameObject OfficeCube2CRef;
	GameObject OfficeCube2D;
	GameObject OfficeCube2DRef;
	GameObject OfficeCube2E;
	GameObject OfficeCube2ERef;
	GameObject FaultObjectAppear;

	GameObject Fault;
	GameObject FaultBase;

	Scene currentScene;
	string sceneName;

	void Start () 
	{
		

		//scene manager
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;


		FaultObjectAppear = GameObject.Find ("FaultObjectAppear");


		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1) {

			//cheat to hide MCQ related objects
			FaultObjectAppear.transform.position = new Vector3 (0, 0, 0);
		} else 
		{
			FaultObjectAppear.transform.position = new Vector3 (-100f, -100f, 0);
		}






		//if (sceneName == "HEE Menu") 
		//{
		//	HVLcube = GameObject.Find ("TransportHVL");
		//	HVLcubeBase = GameObject.Find ("Transport HVL base");
		//	Assemblecube = GameObject.Find ("Transport build");
		//	AssemblecubeBase = GameObject.Find ("Transport build base");
		//	ISLcube = GameObject.Find ("TransportISL");
		//	ISLcubeBase = GameObject.Find ("Transport ISL base");
		//	anatcube = GameObject.Find ("Anatomy");
		//	anatcubebase = GameObject.Find ("Anatomy base");
		//	LBAcube = GameObject.Find ("LBA");
		//	LBAcubebase = GameObject.Find ("LBA base");
		//	Fault = GameObject.Find ("Faults");
		//	FaultBase = GameObject.Find ("Faults base");
		//}



		//if (sceneName == "HVL Xray Room Oculus Touch" || sceneName == "Inverse Square Law Room" || sceneName == "Assemble Xray Room Oculus Touch")
		//{
		//	OfficeCubeA = GameObject.Find ("OfficeA");
		//	OfficeCubeARef = GameObject.Find ("OfficeRefA");
		//	OfficeCubeB = GameObject.Find ("OfficeB");
		//	OfficeCubeBRef = GameObject.Find ("OfficeRefB");

		//}


		//if (sceneName == "HEE Light Field Alignment") {

		//	OfficeCubeA = GameObject.Find ("OfficeA");
		//	OfficeCubeARef = GameObject.Find ("OfficeRefA");
		//	OfficeCubeB = GameObject.Find ("OfficeB");
		//	OfficeCubeBRef = GameObject.Find ("OfficeRefB");
		//	OfficeCube2A = GameObject.Find ("Office2A");
		//	OfficeCube2ARef = GameObject.Find ("OfficeRef2A");
		//	OfficeCube2B = GameObject.Find ("Office2B");
		//	OfficeCube2BRef = GameObject.Find ("OfficeRef2B");
		//	OfficeCube2C = GameObject.Find ("Office2C");
		//	OfficeCube2CRef = GameObject.Find ("OfficeRef2C");
		//	OfficeCube2D = GameObject.Find ("Office2D");
		//	OfficeCube2DRef = GameObject.Find ("OfficeRef2D");
		//	OfficeCube2E = GameObject.Find ("Office2E");
		//	OfficeCube2ERef = GameObject.Find ("OfficeRef2E");
		//}

		//if (sceneName == "Assemble Xray Room Oculus Touch")
		//{
		//	OfficeCubeC = GameObject.Find ("OfficeC");
		//	OfficeCubeCRef = GameObject.Find ("OfficeRefC");
		//	OfficeCubeD = GameObject.Find ("OfficeD");
		//	OfficeCubeDRef = GameObject.Find ("OfficeRefD");
		//	OfficeCubeE = GameObject.Find ("OfficeE");
		//	OfficeCubeERef = GameObject.Find ("OfficeRefE");
		//	OfficeCube2A = GameObject.Find ("Office2A");
		//	OfficeCube2ARef = GameObject.Find ("OfficeRef2A");
		//	OfficeCube2B = GameObject.Find ("Office2B");
		//	OfficeCube2BRef = GameObject.Find ("OfficeRef2B");
		//	OfficeCube2C = GameObject.Find ("Office2C");
		//	OfficeCube2CRef = GameObject.Find ("OfficeRef2C");

		//}

		//if (sceneName == "HVL Xray Room Oculus Touch")
		//{
		//	OfficeCubeC = GameObject.Find ("OfficeC");
		//	OfficeCubeCRef = GameObject.Find ("OfficeRefC");
		//	OfficeCubeD = GameObject.Find ("OfficeD");
		//	OfficeCubeDRef = GameObject.Find ("OfficeRefD");
		//	OfficeCubeE = GameObject.Find ("OfficeE");
		//	OfficeCubeERef = GameObject.Find ("OfficeRefE");

		//}

		//if (sceneName == "Inverse Square Law Room")
		//{
		//	OfficeCubeC = GameObject.Find ("OfficeC");
		//	OfficeCubeCRef = GameObject.Find ("OfficeRefC");
		//	OfficeCubeD = GameObject.Find ("OfficeD");
		//	OfficeCubeDRef = GameObject.Find ("OfficeRefD");
		//	OfficeCubeE = GameObject.Find ("OfficeE");
		//	OfficeCubeERef = GameObject.Find ("OfficeRefE");
		//	OfficeCubeF = GameObject.Find ("OfficeF");
		//	OfficeCubeFRef = GameObject.Find ("OfficeRefF");

		//}



	}
	
	void Update () 
	{

		//MCQ cube distances

		//if (sceneName == "HVL Xray Room Oculus Touch" || sceneName == "Inverse Square Law Room" || sceneName == "Assemble Xray Room Oculus Touch")
		//{
		//	OfficeCubeAdist = Vector3.Distance (OfficeCubeA.transform.position, OfficeCubeARef.transform.position);
		//	OfficeCubeBdist = Vector3.Distance (OfficeCubeB.transform.position, OfficeCubeBRef.transform.position);

		//	if (OfficeCubeAdist >= 0.1) {
		//		OfficeCubeA.transform.position = OfficeCubeARef.transform.position;
		//		OfficeCubeA.transform.rotation = OfficeCubeARef.transform.rotation;
		//		OfficeCubeA.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}

		//	if (OfficeCubeBdist >= 0.1) {
		//		OfficeCubeB.transform.position = OfficeCubeBRef.transform.position;
		//		OfficeCubeB.transform.rotation = OfficeCubeBRef.transform.rotation;
		//		OfficeCubeB.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}

		//}







		//if (sceneName == "HEE Light Field Alignment") {

		//	OfficeCubeAdist = Vector3.Distance (OfficeCubeA.transform.position, OfficeCubeARef.transform.position);
		//	OfficeCubeBdist = Vector3.Distance (OfficeCubeB.transform.position, OfficeCubeBRef.transform.position);
		//	OfficeCube2Adist = Vector3.Distance (OfficeCube2A.transform.position, OfficeCube2ARef.transform.position);
		//	OfficeCube2Bdist = Vector3.Distance (OfficeCube2B.transform.position, OfficeCube2BRef.transform.position);
		//	OfficeCube2Cdist = Vector3.Distance (OfficeCube2C.transform.position, OfficeCube2CRef.transform.position);
		//	OfficeCube2Ddist = Vector3.Distance (OfficeCube2D.transform.position, OfficeCube2DRef.transform.position);
		//	OfficeCube2Edist = Vector3.Distance (OfficeCube2E.transform.position, OfficeCube2ERef.transform.position);

		//	if (OfficeCubeAdist >= 0.1) {
		//		OfficeCubeA.transform.position = OfficeCubeARef.transform.position;
		//		OfficeCubeA.transform.rotation = OfficeCubeARef.transform.rotation;
		//		OfficeCubeA.GetComponent<MeshRenderer> ().material.color = Color.green;


		//		//correct answer
		//		PlayerPrefs.SetFloat("PhantomsCorrect1",1.0f);
		//	}

		//	if (OfficeCubeBdist >= 0.1) {
		//		OfficeCubeB.transform.position = OfficeCubeBRef.transform.position;
		//		OfficeCubeB.transform.rotation = OfficeCubeBRef.transform.rotation;
		//		OfficeCubeB.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}

		//	if (OfficeCube2Adist >= 0.1) {
		//		OfficeCube2A.transform.position = OfficeCube2ARef.transform.position;
		//		OfficeCube2A.transform.rotation = OfficeCube2ARef.transform.rotation;
		//		OfficeCube2A.GetComponent<MeshRenderer> ().material.color = Color.red;


		//	}
		//	if (OfficeCube2Bdist >= 0.1) {
		//		OfficeCube2B.transform.position = OfficeCube2BRef.transform.position;
		//		OfficeCube2B.transform.rotation = OfficeCube2BRef.transform.rotation;
		//		OfficeCube2B.GetComponent<MeshRenderer> ().material.color = Color.green;



		//		//correct answer
		//		PlayerPrefs.SetFloat("PhantomsCorrect2",1.0f);

		//	}
		//	if (OfficeCube2Cdist >= 0.1) {
		//		OfficeCube2C.transform.position = OfficeCube2CRef.transform.position;
		//		OfficeCube2C.transform.rotation = OfficeCube2CRef.transform.rotation;
		//		OfficeCube2C.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}

		//	if (OfficeCube2Ddist >= 0.1) {
		//		OfficeCube2D.transform.position = OfficeCube2DRef.transform.position;
		//		OfficeCube2D.transform.rotation = OfficeCube2DRef.transform.rotation;
		//		OfficeCube2D.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}
		//	if (OfficeCube2Edist >= 0.1) {
		//		OfficeCube2E.transform.position = OfficeCube2ERef.transform.position;
		//		OfficeCube2E.transform.rotation = OfficeCube2ERef.transform.rotation;
		//		OfficeCube2E.GetComponent<MeshRenderer> ().material.color = Color.red;


		//	}


		//}






		//if (sceneName == "Assemble Xray Room Oculus Touch")
		//{

		//	OfficeCubeCdist = Vector3.Distance (OfficeCubeC.transform.position, OfficeCubeCRef.transform.position);
		//	OfficeCubeDdist = Vector3.Distance (OfficeCubeD.transform.position, OfficeCubeDRef.transform.position);

		//	OfficeCube2Adist = Vector3.Distance (OfficeCube2A.transform.position, OfficeCube2ARef.transform.position);
		//	OfficeCube2Bdist = Vector3.Distance (OfficeCube2B.transform.position, OfficeCube2BRef.transform.position);
		//	OfficeCube2Cdist = Vector3.Distance (OfficeCube2C.transform.position, OfficeCube2CRef.transform.position);


		//	if (OfficeCubeCdist >= 0.1) {
		//		OfficeCubeC.transform.position = OfficeCubeCRef.transform.position;
		//		OfficeCubeC.transform.rotation = OfficeCubeCRef.transform.rotation;
		//		OfficeCubeC.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}

		//	if (OfficeCubeDdist >= 0.1) {
		//		OfficeCubeD.transform.position = OfficeCubeDRef.transform.position;
		//		OfficeCubeD.transform.rotation = OfficeCubeDRef.transform.rotation;
		//		OfficeCubeD.GetComponent<MeshRenderer> ().material.color = Color.green;


		//		//correct answer
		//		PlayerPrefs.SetFloat("AssembleCorrect1",1.0f);
		//	}


		//	if (OfficeCube2Adist >= 0.1) {
		//		OfficeCube2A.transform.position = OfficeCube2ARef.transform.position;
		//		OfficeCube2A.transform.rotation = OfficeCube2ARef.transform.rotation;
		//		OfficeCube2A.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}
		//	if (OfficeCube2Bdist >= 0.1) {
		//		OfficeCube2B.transform.position = OfficeCube2BRef.transform.position;
		//		OfficeCube2B.transform.rotation = OfficeCube2BRef.transform.rotation;
		//		OfficeCube2B.GetComponent<MeshRenderer> ().material.color = Color.green;


		//		//corect answer
		//		PlayerPrefs.SetFloat("AssembleCorrect2",1.0f);

		//	}
		//	if (OfficeCube2Cdist >= 0.1) {
		//		OfficeCube2C.transform.position = OfficeCube2CRef.transform.position;
		//		OfficeCube2C.transform.rotation = OfficeCube2CRef.transform.rotation;
		//		OfficeCube2C.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}


		//}









		//if (sceneName == "HVL Xray Room Oculus Touch")
		//{
		//	OfficeCubeCdist = Vector3.Distance (OfficeCubeC.transform.position, OfficeCubeCRef.transform.position);
		//	OfficeCubeDdist = Vector3.Distance (OfficeCubeD.transform.position, OfficeCubeDRef.transform.position);
		//	OfficeCubeEdist = Vector3.Distance (OfficeCubeE.transform.position, OfficeCubeERef.transform.position);

		//	if (OfficeCubeCdist >= 0.1) {
		//		OfficeCubeC.transform.position = OfficeCubeCRef.transform.position;
		//		OfficeCubeC.transform.rotation = OfficeCubeCRef.transform.rotation;
		//		OfficeCubeC.GetComponent<MeshRenderer> ().material.color = Color.green;

		//		//correct answer
		//		PlayerPrefs.SetFloat("HVLCorrect2",1.0f);

		//	}

		//	if (OfficeCubeDdist >= 0.1) {
		//		OfficeCubeD.transform.position = OfficeCubeDRef.transform.position;
		//		OfficeCubeD.transform.rotation = OfficeCubeDRef.transform.rotation;
		//		OfficeCubeD.GetComponent<MeshRenderer> ().material.color = Color.green;

		//		//correct answer
		//		PlayerPrefs.SetFloat("HVLCorrect1",1.0f);

		//	}
		//	if (OfficeCubeEdist >= 0.1) {
		//		OfficeCubeE.transform.position = OfficeCubeERef.transform.position;
		//		OfficeCubeE.transform.rotation = OfficeCubeERef.transform.rotation;
		//		OfficeCubeE.GetComponent<MeshRenderer> ().material.color = Color.red;



		//	}

		//}









		//if (sceneName == "Inverse Square Law Room")
		//{
		//	OfficeCubeCdist = Vector3.Distance (OfficeCubeC.transform.position, OfficeCubeCRef.transform.position);
		//	OfficeCubeDdist = Vector3.Distance (OfficeCubeD.transform.position, OfficeCubeDRef.transform.position);
		//	OfficeCubeEdist = Vector3.Distance (OfficeCubeE.transform.position, OfficeCubeERef.transform.position);
		//	OfficeCubeFdist = Vector3.Distance (OfficeCubeF.transform.position, OfficeCubeFRef.transform.position);


		//	if (OfficeCubeCdist >= 0.1) {
		//		OfficeCubeC.transform.position = OfficeCubeCRef.transform.position;
		//		OfficeCubeC.transform.rotation = OfficeCubeCRef.transform.rotation;
		//		OfficeCubeC.GetComponent<MeshRenderer> ().material.color = Color.green;


		//		//correct answer
		//		PlayerPrefs.SetFloat("DAPCorrect",1.0f);

		//	}

		//	if (OfficeCubeDdist >= 0.1) {
		//		OfficeCubeD.transform.position = OfficeCubeDRef.transform.position;
		//		OfficeCubeD.transform.rotation = OfficeCubeDRef.transform.rotation;
		//		OfficeCubeD.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}
		//	if (OfficeCubeEdist >= 0.1) {
		//		OfficeCubeE.transform.position = OfficeCubeERef.transform.position;
		//		OfficeCubeE.transform.rotation = OfficeCubeERef.transform.rotation;
		//		OfficeCubeE.GetComponent<MeshRenderer> ().material.color = Color.red;

		//	}
		//	if (OfficeCubeFdist >= 0.1) {
		//		OfficeCubeF.transform.position = OfficeCubeFRef.transform.position;
		//		OfficeCubeF.transform.rotation = OfficeCubeFRef.transform.rotation;
		//		OfficeCubeF.GetComponent<MeshRenderer> ().material.color = Color.red;


		//	}
		//}


	}





	void OnApplicationQuit()
	{
		PlayerPrefs.SetFloat ("FaultsActivated", 0.0f);
		PlayerPrefs.SetFloat ("AssembleCorrect1", 0.0f);
		PlayerPrefs.SetFloat ("AssembleCorrect2", 0.0f);
		PlayerPrefs.SetFloat ("HVLCorrect1", 0.0f);
		PlayerPrefs.SetFloat ("HVLCorrect2", 0.0f);
		PlayerPrefs.SetFloat ("DAPCorrect", 0.0f);
		PlayerPrefs.SetFloat ("PhantomsCorrect1", 0.0f);
		PlayerPrefs.SetFloat ("PhantomsCorrect2", 0.0f);
	}




}

