using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ColourCubes : MonoBehaviour
{
	private GameObject DeactivateFaultCube;
	private GameObject FaultCube;
	private GameObject HVLcube;
	private GameObject Assemblecube;
	private GameObject ISLcube;
	private GameObject anatcube;
	private GameObject LBAcube;
	private GameObject AECcube;

	private Color blue;

	void Start()
	{
		FaultCube = transform.Find("Activate Faults/Cube").gameObject;
		DeactivateFaultCube = transform.Find("Deactivate Faults/Cube").gameObject;
		HVLcube = transform.Find("HVL/Cube").gameObject;
		Assemblecube = transform.Find("Build X-Ray Tube/Cube").gameObject;
		ISLcube = transform.Find("DAP Test/Cube").gameObject;
		anatcube = transform.Find("Anatomy/Cube").gameObject;
		LBAcube = transform.Find("Phantoms/Cube").gameObject;
		AECcube = transform.Find("AEC/Cube").gameObject;

		blue = new Color32(0, 149, 255, 255);
	}


	public void ActivateFaults()
	{
		Debug.Log("[ColourCubes] ActivateFaults called");
		FaultsManager.ActivateFaults();
	}

	public void DeactivateFaults()
	{
		FaultsManager.DeactivateFaults();
	}

	void Update()
	{
		//if (FaultsManager == null) { return; } // in case this scene loads before the manager

		//var faults = FaultsManager;

		if (FaultsManager.FaultsActivated)
		{
			FaultCube.GetComponent<MeshRenderer>().material.color = Color.grey;
			FaultCube.GetComponent<XRGrabInteractable>().enabled = false;
			FaultCube.GetComponent<BoxCollider>().enabled = false;


			DeactivateFaultCube.GetComponent<MeshRenderer>().material.color = blue;
			DeactivateFaultCube.GetComponent<XRGrabInteractable>().enabled = true;
			DeactivateFaultCube.GetComponent<BoxCollider>().enabled = true;



			Assemblecube.GetComponent<MeshRenderer>().material.color =
				(FaultsManager.AssembleCorrect == 1f) ? Color.green : Color.red;

			HVLcube.GetComponent<MeshRenderer>().material.color =
				(FaultsManager.HVLCorrect == 1f) ? Color.green : Color.red;

			ISLcube.GetComponent<MeshRenderer>().material.color =
				(FaultsManager.DAPCorrect == 1f) ? Color.green : Color.red;

			LBAcube.GetComponent<MeshRenderer>().material.color =
				(FaultsManager.PhantomsCorrect == 1f) ? Color.green : Color.red;

			anatcube.GetComponent<MeshRenderer>().material.color = Color.grey;
			anatcube.GetComponent<XRGrabInteractable>().enabled = false;
			anatcube.GetComponent<BoxCollider>().enabled = false;

			AECcube.GetComponent<MeshRenderer>().material.color = Color.grey;
			AECcube.GetComponent<XRGrabInteractable>().enabled = false;
			AECcube.GetComponent<BoxCollider>().enabled = false;


			// Only allow training on patient once all four tasks are completed.
			float score = FaultsManager.SumScore();

			if (score == 4f)
			{
				anatcube.GetComponent<MeshRenderer>().material.color = Color.green;
				anatcube.GetComponent<XRGrabInteractable>().enabled = true;
				anatcube.GetComponent<BoxCollider>().enabled = true;

				AECcube.GetComponent<MeshRenderer>().material.color = Color.green;
				AECcube.GetComponent<XRGrabInteractable>().enabled = true;
				AECcube.GetComponent<BoxCollider>().enabled = true;


				FaultsManager.DeactivateFaults();
			}
		}
		else
		{
			FaultCube.GetComponent<MeshRenderer>().material.color = Color.red;
			FaultCube.GetComponent<XRGrabInteractable>().enabled = true;
			FaultCube.GetComponent<BoxCollider>().enabled = true;

			DeactivateFaultCube.GetComponent<MeshRenderer>().material.color = Color.grey;
			DeactivateFaultCube.GetComponent<XRGrabInteractable>().enabled = false;
			DeactivateFaultCube.GetComponent<BoxCollider>().enabled = false;



			HVLcube.GetComponent<MeshRenderer>().material.color = blue;
			Assemblecube.GetComponent<MeshRenderer>().material.color = blue;
			ISLcube.GetComponent<MeshRenderer>().material.color = blue;
			LBAcube.GetComponent<MeshRenderer>().material.color = blue;
			anatcube.GetComponent<MeshRenderer>().material.color = blue;

			AECcube.GetComponent<MeshRenderer>().material.color = blue;

			anatcube.GetComponent<XRGrabInteractable>().enabled = true;
			anatcube.GetComponent<BoxCollider>().enabled = true;

			AECcube.GetComponent<XRGrabInteractable>().enabled = true;
			AECcube.GetComponent<BoxCollider>().enabled = true;
		}
	}
}