using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Used to display the current distance between the X-Ray head and the table below
/// </summary>
public class DisplayPosition : MonoBehaviour {

	public Transform Screen;
	public Transform Table;
	public Text UIText;
	string LocationFromTable;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		LocationFromTable = Vector3.Distance (Screen.position, Table.position) .ToString("F2");
		UIText.text = LocationFromTable + "m";
	}
		
		

}
