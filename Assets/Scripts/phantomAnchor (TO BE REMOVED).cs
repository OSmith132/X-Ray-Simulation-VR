//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class phantomAnchor : MonoBehaviour
//{

//	GameObject anchor;
//	public float phantomMoved = 0;

//	// Use this for initialization
//	void Start () 
//	{	

//		anchor = GameObject.Find ("PhantomBase");
		
//	}
	
//	// Update is called once per frame
//	void Update () 
//	{
//		float anchordist = Vector3.Distance (gameObject.transform.position, anchor.transform.position);

//		if (anchordist <= 0.1) {
//			gameObject.transform.position = anchor.transform.position;
//			gameObject.transform.rotation = anchor.transform.rotation;
//			phantomMoved = 0;
//		}

//		if (gameObject.transform.position != anchor.transform.position) {
//			phantomMoved = 1;
//		} 
			
//	}
//}
