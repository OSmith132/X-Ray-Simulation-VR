using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnchorPoints : MonoBehaviour {

	//this code is attached to the Gameobject Anchors found in the Assemble Xray room scene, it controls almost everything that happens in the assemble xray room scene 

	float count;
	float count1;
	float count2;
	float count3;
	float count4;
	float count5;
	float count6;
	float count7;
	float count8;
	float count9;
	float TestDist;
	float BackDist;
	float TableDist;
	float OfficeDist;
	float ResetDist;
	float Menudist;
	float MenuFrontDist;
	float SubmitAnsDist;


	//controlling speed of anode spin
	float increase;
	float slowDown;


	private bool  onoff;

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
	public GameObject Grab;
	public Transform GrabT;
	public Transform GrabTI;

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

	public GameObject AnodeRef;
	public GameObject CathodeRef;
	public GameObject RotorRef;
	//public GameObject StatorRef;
	public GameObject CoilRef;
	public GameObject GlassRef;
	public GameObject PanelRef;
	public GameObject CollimatorRef;
	public GameObject CoverRef;
	public GameObject BeRef;

	GameObject OVRCAM;
	GameObject TransportText;
	GameObject Testcube;
	GameObject TestcubeRef;
	GameObject TestcubeNewRef;
	GameObject TestText;
	GameObject BackRoomCube;
	GameObject BackRoomCubeRef;
	GameObject FinalText;
	GameObject BacktoTable;
	GameObject BacktoTableRef;
	GameObject BacktoOffice;
	GameObject BacktoOfficeRef;
	GameObject Reset;
	GameObject ResetRef;
	GameObject MenuCube;
	GameObject MenuCubeBase;
	GameObject MenuFront;
	GameObject MenuFrontBase;
	GameObject SubmitAnswers;
	GameObject SubmitAnswersRef;

	GameObject AnodeSpinner;
	GameObject ElectronBeam;
	GameObject XrayLight;

	GameObject TubeTest;
	GameObject PanelTest;

	GameObject ColHousing;

	// Use this for initialization
	void Start () {

		OVRCAM = GameObject.Find ("OVRCameraRig");
		TransportText = GameObject.Find ("transport text");
		Testcube = GameObject.Find ("Test Grab");
		TestcubeRef = GameObject.Find ("Test Grab Ref");
		TestcubeNewRef = GameObject.Find ("Test Grab New Ref");
		TestText = GameObject.Find ("Test text");
		AnodeSpinner = GameObject.Find ("AnodeA");
		ElectronBeam = GameObject.Find ("Electron Beam");
		XrayLight = GameObject.Find ("Collimator Guide Light");
		BackRoomCube = GameObject.Find ("BackRoom");
		BackRoomCubeRef = GameObject.Find ("BackRoomRef");
		TubeTest = GameObject.Find ("Tube Cover A");
		PanelTest = GameObject.Find ("PanelHandleA");
		ColHousing = GameObject.Find ("ColCover");
		FinalText = GameObject.Find ("FinalText");
		BacktoTable = GameObject.Find ("Back to table");
		BacktoTableRef = GameObject.Find ("Back to table ref");
		BacktoOffice = GameObject.Find ("Back to office");
		BacktoOfficeRef = GameObject.Find ("Back to office ref");
		Reset = GameObject.Find ("Reset");
		ResetRef = GameObject.Find ("Reset base");
		MenuCube = GameObject.Find ("Menu");
		MenuCubeBase = GameObject.Find ("Menubase");
		MenuFront = GameObject.Find ("MenuF");
		MenuFrontBase = GameObject.Find ("MenubaseF");
		SubmitAnswers = GameObject.Find ("OfficeMenuCube");
		SubmitAnswersRef = GameObject.Find ("SubmitAnsRef");


	
		Completion.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		Grab.gameObject.SetActive(false);
		//Grab.gameObject.GetComponent<Collider> ().enabled = false;
		TransportText.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		Testcube.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		//Testcube.gameObject.GetComponent<Collider> ().enabled = false;
		TestText.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		AnodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		CathodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		RotorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		CoilLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		GlassLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		PanelLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		CollimatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		CoverLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		BeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		Instructions.gameObject.GetComponent<MeshRenderer> ().enabled = true;

	}
	
	// Update is called once per frame
	void Update () {

		float distTube = Vector3.Distance (Tube.position, TubeAnchor.position);
		float distGlass = Vector3.Distance (Glass.position, GlassAnchor.position);
		float distCoil = Vector3.Distance (Coil.position, CoilAnchor.position);
		float distAnode = Vector3.Distance (Anode.position, AnodeAnchor.position);
		float distCathode = Vector3.Distance (Cathode.position, CathodeAnchor.position);
		float distRotor = Vector3.Distance (Rotor.position, RotorAnchor.position);
		//float distStator = Vector3.Distance (Stator.position, StatorAnchor.position);
		float distCollimator = Vector3.Distance (Collimator.position, CollimatorAnchor.position);
		float distBe = Vector3.Distance (Be.position, BeAnchor.position);
		float distPanel = Vector3.Distance (Panel.position, PanelAnchor.position);
		float CubDist = Vector3.Distance (GrabT.position, GrabTI.position);
		TestDist = Vector3.Distance (Testcube.transform.position, TestcubeRef.transform.position);
		BackDist = Vector3.Distance (BackRoomCube.transform.position, BackRoomCubeRef.transform.position);
		TableDist = Vector3.Distance (BacktoTable.transform.position, BacktoTableRef.transform.position);
		OfficeDist = Vector3.Distance (BacktoOffice.transform.position, BacktoOfficeRef.transform.position);
		ResetDist = Vector3.Distance (Reset.transform.position, ResetRef.transform.position);
		Menudist = Vector3.Distance (MenuCube.transform.position, MenuCubeBase.transform.position);
		MenuFrontDist = Vector3.Distance (MenuFront.transform.position, MenuFrontBase.transform.position);
		SubmitAnsDist = Vector3.Distance (SubmitAnswers.transform.position, SubmitAnswersRef.transform.position);


		if (MenuFrontDist >= 0.1) {
			SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
			MenuFront.transform.position = MenuFrontBase.transform.position;
		}

		if (SubmitAnsDist >= 0.1) {
			SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
			SubmitAnswers.transform.position = SubmitAnswersRef.transform.position;
		}


		if (Input.GetButtonDown ("Label")) {
			onoff = !onoff;
			if (onoff)
			{
				AnodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				CathodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				RotorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				CoilLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				GlassLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				PanelLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				CollimatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				CoverLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				BeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			}
			else
			{
				AnodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				CathodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				RotorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				//StatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				CoilLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				GlassLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				PanelLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				CollimatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				CoverLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				BeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			}
		}

		if (distTube <= 0.05) {
			Tube.position = TubeAnchor.position;
			Tube.rotation = TubeAnchor.rotation;
			count = 1;
			CoverLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}


		if (distGlass <= 0.05) {
			Glass.position = GlassAnchor.position;
			Glass.rotation = GlassAnchor.rotation;
			count1 = 1;
			GlassLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (distCoil <= 0.05) {
			Coil.position = CoilAnchor.position;
			Coil.rotation = CoilAnchor.rotation;
			count2 = 1;
			CoilLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (distAnode <= 0.05) {
			Anode.position = AnodeAnchor.position;
			//Anode.rotation = AnodeAnchor.rotation;
			count3 = 1;
			AnodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		

		} 

		if (distCathode <= 0.05) {
			Cathode.position = CathodeAnchor.position;
			Cathode.rotation = CathodeAnchor.rotation;
			count4 = 1;
			CathodeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (distRotor <= 0.05) {
			Rotor.position = RotorAnchor.position;
			Rotor.rotation = RotorAnchor.rotation;
			count5 = 1;
			RotorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}
			

		if (distCollimator <= 0.05) {
			Collimator.position = CollimatorAnchor.position;
			Collimator.rotation = CollimatorAnchor.rotation;
			count7 = 1;
			CollimatorLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (distBe <= 0.05) {
			Be.position = BeAnchor.position;
			Be.rotation = BeAnchor.rotation;
			count8 = 1;
			BeLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (distPanel <= 0.05) {
			Panel.position = PanelAnchor.position;
			Panel.rotation = PanelAnchor.rotation;
			count9 = 1;
			PanelLabel.gameObject.GetComponent<MeshRenderer> ().enabled = false;

		}

		if (count==1 && count1==1 && count2==1 && count3==1 && count4==1 && count5==1 && count7==1 && count8==1 && count9==1) {
			Completion.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			Grab.gameObject.SetActive (true);
			//Grab.gameObject.GetComponent<Collider> ().enabled = true;
			Instructions.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			TransportText.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			Testcube.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			//Testcube.gameObject.GetComponent<Collider> ().enabled = true;
			TestText.gameObject.GetComponent<MeshRenderer> ().enabled = true;

			if (CubDist >= 0.1) 
			{
				OVRCAM.transform.position = new Vector3 (2.888f, 1.4f, 0.094f);
				GrabT.position = GrabTI.position;
				GrabT.rotation = GrabTI.rotation;
			}
			if (TableDist >= 0.1) 
			{
				OVRCAM.transform.position = new Vector3 (0.0075f, 1.4f, 0.094f);
				BacktoTable.transform.position = BacktoTableRef.transform.position;
				BacktoTable.transform.rotation = BacktoTableRef.transform.rotation;
			}
			if (OfficeDist >= 0.1) 
			{
				OVRCAM.transform.position = new Vector3 (5.35f, 1.4f, 0.094f);
				BacktoOffice.transform.position = BacktoOfficeRef.transform.position;
				BacktoOffice.transform.rotation = BacktoOfficeRef.transform.rotation;
			}
			if (ResetDist >= 0.1) 
			{
				SceneManager.LoadScene ("Assemble Xray Room Oculus Touch", LoadSceneMode.Single);
				Reset.transform.position = ResetRef.transform.position;
				Reset.transform.rotation = ResetRef.transform.rotation;
			}
			if (Menudist >= 0.1) {
				SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
				MenuCube.transform.position = MenuCubeBase.transform.position;
			}

			if (BackDist >= 0.1) 
			{
				OVRCAM.transform.position = new Vector3 (2.808f, 1.4f, 0.094f);
				BackRoomCube.transform.position = BackRoomCubeRef.transform.position;
				BackRoomCube.transform.rotation = BackRoomCubeRef.transform.rotation;
			}


			//pull test cube, will hide objects to allow viewing of anode spining etc
			if (TestDist >= 0.1)
			{
				
				ColHousing.gameObject.GetComponent<MeshRenderer> ().enabled = false;
				TubeTest.SetActive (false);
				PanelTest.SetActive (false);

				increase += (Time.deltaTime*10f);
				AnodeSpinner.transform.Rotate (Vector3.up, increase);

				if (increase >= 200)
				{
					increase = 200;
				}

				ElectronBeam.GetComponent<Light>().intensity = 500;
				XrayLight.GetComponent<Light> ().intensity = 10;

				Testcube.transform.position = TestcubeNewRef.transform.position;
				Testcube.transform.rotation = TestcubeNewRef.transform.rotation;
				Testcube.GetComponent<MeshRenderer> ().material.color = Color.green;

					
			}
		
		}
	}
		
		
		

//	void OnCollisionEnter (Collision touch){
//		if (touch.gameObject.name == "Grab Me") {
//			SceneManager.LoadScene ("HEE Xray Room Oculus Touch", LoadSceneMode.Single);
//		}
//	}
}


