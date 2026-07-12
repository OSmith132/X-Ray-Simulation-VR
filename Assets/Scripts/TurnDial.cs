using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDial : MonoBehaviour {

	public Transform outerLock;

	private Vector3 lastEuler = Vector3.zero;


	private void Update()
	{
		Vector3 currentEuler = transform.localRotation.eulerAngles;

		float delta = currentEuler.x - lastEuler.x;
		outerLock.localRotation = Quaternion.Euler(outerLock.localRotation.eulerAngles.x - delta, 0f, 0f);

		lastEuler = transform.localRotation.eulerAngles;
	}
}
