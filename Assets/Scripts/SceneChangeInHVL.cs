using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeInHVL : MonoBehaviour {

	//this code is attached to Transport in HVL scene

	GameObject MenuCube;
	GameObject MenuCubeBase;
	float Menudist;

	// Use this for initialization
	void Start () {
		MenuCube = GameObject.Find ("Menu");
		MenuCubeBase = GameObject.Find ("Menubase");	
	}
	
	// Update is called once per frame
	void Update () {


		Menudist = Vector3.Distance (MenuCube.transform.position, MenuCubeBase.transform.position);

		if (Menudist >= 0.1) {
			SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
			MenuCube.transform.position = MenuCubeBase.transform.position;
		}
	}
}

