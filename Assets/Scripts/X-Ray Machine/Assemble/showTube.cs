using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTube : MonoBehaviour

{
	//public GameObject TubeButton;
	//Color OGTubeButtonColor;

	Text mAsText;
	Text kVText;
	Text freeroam;
	Text vertfix;

	public GameObject screen;
	//public GameObject Be_window;
	//public GameObject Be_window_smaller;
	public GameObject Beam_connection;
	public GameObject Wire_Connection;
	public GameObject Tube_Cover;
	public GameObject Col_Housing;
//	public GameObject Col_L;
//	public GameObject Col_R;
	public GameObject Panel;
	public GameObject Panel_handle;
	public GameObject a1;
	public GameObject a2;
	public GameObject a3;
	public GameObject a4;
	public GameObject Freebut;
	public GameObject Vertbut;


	public Text PositionText;



    
	MeshRenderer screen_mesh;
	//MeshRenderer Be_window_mesh;
	//MeshRenderer Be_window_smaller_mesh;
	MeshRenderer Beam_connection_mesh;
	MeshRenderer Wire_conncetion_mesh;
	MeshRenderer Tube_Cover_mesh;
	MeshRenderer Col_Housing_mesh;
//	MeshRenderer Col_L_mesh;
//	MeshRenderer Col_R_mesh;
	MeshRenderer Panel_mesh;
	MeshRenderer Panel_handle_mesh;
	MeshRenderer a1m;
	MeshRenderer a2m;
	MeshRenderer a3m;
	MeshRenderer a4m;
	MeshRenderer Freebutt;
	MeshRenderer Vertbutt;

	// Use this for initialization
	void Start () 
	{




		screen_mesh = screen.GetComponent<MeshRenderer> ();
		//Be_window_mesh = Be_window.GetComponent<MeshRenderer> ();
		//Be_window_smaller_mesh = Be_window_smaller.GetComponent<MeshRenderer> ();
		Beam_connection_mesh = Beam_connection.GetComponent<MeshRenderer> ();
		Wire_conncetion_mesh = Wire_Connection.GetComponent<MeshRenderer> ();
		Tube_Cover_mesh = Tube_Cover.GetComponent<MeshRenderer> ();
		Col_Housing_mesh = Col_Housing.GetComponent<MeshRenderer> ();
//		Col_L_mesh = Col_L.GetComponent<MeshRenderer> ();
//		Col_R_mesh = Col_R.GetComponent<MeshRenderer> ();
		Panel_mesh = Panel.GetComponent<MeshRenderer> ();
		Panel_handle_mesh = Panel_handle.GetComponent<MeshRenderer> ();
		a1m = a1.GetComponent<MeshRenderer> ();
		a2m = a2.GetComponent<MeshRenderer> ();
		a3m = a3.GetComponent<MeshRenderer> ();
		a4m = a4.GetComponent<MeshRenderer> ();
		Freebutt = Freebut.GetComponent<MeshRenderer> ();
		Vertbutt = Vertbut.GetComponent<MeshRenderer> ();

		//OGTubeButtonColor = TubeButton.GetComponent<Renderer> ().material.color;

		mAsText = GameObject.Find ("mAsText").GetComponent<Text> ();
		kVText = GameObject.Find ("kVText").GetComponent<Text> ();
		freeroam = GameObject.Find ("Free Roam").GetComponent<Text> ();
		vertfix = GameObject.Find ("Vertical").GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Tube")){
			Hide ();	
		}
		
	}

	public void Hide()
	{
		if (Beam_connection_mesh.enabled == true)
		{
			screen_mesh.enabled = false;
			//Be_window_mesh.enabled = false;
			//Be_window_smaller_mesh.enabled = false;
			Beam_connection_mesh.enabled = false;
			Wire_conncetion_mesh.enabled = false;
			Tube_Cover_mesh.enabled = false;
			Col_Housing_mesh.enabled = false;
//			Col_L_mesh.enabled = false;
//			Col_R_mesh.enabled = false;
			Panel_mesh.enabled = false;
			Panel_handle_mesh.enabled = false;
			a1m.enabled = false;
			a2m.enabled = false;
			a3m.enabled = false;
			a4m.enabled = false;
			Freebutt.enabled = false;
			Vertbutt.enabled = false;

			//TubeButton.GetComponent<Renderer> ().material.color = new Color (0.75f, 0.1f, 0.1f, 0.25f);

			vertfix.enabled = false;
			freeroam.enabled = false;
			mAsText.enabled = false;
			kVText.enabled = false;
			PositionText.enabled = false;

		}
		else
		{

			screen_mesh.enabled = true;
			//Be_window_mesh.enabled = true;
			//Be_window_smaller_mesh.enabled = true;
			Beam_connection_mesh.enabled = true;
			Wire_conncetion_mesh.enabled = true;
			Tube_Cover_mesh.enabled = true;
			Col_Housing_mesh.enabled = true;
//			Col_L_mesh.enabled = true;
//			Col_R_mesh.enabled = true;
			Panel_mesh.enabled = true;
			Panel_handle_mesh.enabled = true;
			a1m.enabled = true;
			a2m.enabled = true;
			a3m.enabled = true;
			a4m.enabled = true;
			Freebutt.enabled = true;
			Vertbutt.enabled = true;

			//TubeButton.GetComponent<Renderer> ().material.color = OGTubeButtonColor;

			vertfix.enabled = true;
			freeroam.enabled = true;
			mAsText.enabled = true;
			kVText.enabled = true;
			PositionText.enabled = true;


        }

	}
}
