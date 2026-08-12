using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FaultScoreManager : MonoBehaviour
{

	private GameObject DeactivateFaultCube;
	private GameObject FaultCube;
	private GameObject HVLcube;
	private GameObject Assemblecube;
	private GameObject ISLcube;
	private GameObject anatcube;
	private GameObject LBAcube;

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

		blue = new Color32(0, 149, 255,255);
}

	void Update()
	{
		if (PlayerPrefs.GetFloat("FaultsActivated") == 1.0f)
		{

			FaultCube.GetComponent<MeshRenderer>().material.color = Color.grey;

			DeactivateFaultCube.GetComponent<MeshRenderer>().material.color = blue;
			DeactivateFaultCube.GetComponent<XRGrabInteractable>().enabled = true;
			DeactivateFaultCube.GetComponent<BoxCollider>().enabled = true;



			// Check if they got a perfect score on the MCQ
			Assemblecube.GetComponent<MeshRenderer>().material.color = (PlayerPrefs.GetFloat("AssembleCorrect") == 1f)
				? Color.green : Color.red;





			// TODO: make these consistent with the new 0f-1f system of scoring as above
			HVLcube.GetComponent<MeshRenderer>().material.color = (PlayerPrefs.GetFloat("HVLCorrect1") == 1 && PlayerPrefs.GetFloat("HVLCorrect2") == 1)
				? Color.green : Color.red;

			ISLcube.GetComponent<MeshRenderer>().material.color = (PlayerPrefs.GetFloat("DAPCorrect") == 1)
				? Color.green : Color.red;

			LBAcube.GetComponent<MeshRenderer>().material.color = (PlayerPrefs.GetFloat("PhantomsCorrect1") == 1 && PlayerPrefs.GetFloat("PhantomsCorrect2") == 1)
				? Color.green : Color.red;




			anatcube.GetComponent<MeshRenderer>().material.color = Color.grey;
			anatcube.GetComponent<XRGrabInteractable>().enabled = false;
			anatcube.GetComponent<BoxCollider>().enabled = false;




			// Only allow training on patient once basic training is completed.
			float score = PlayerPrefs.GetFloat("PhantomsCorrect")

						 // TODO: make these consistent with the new 0f-1f system of scoring as above
						 + PlayerPrefs.GetFloat("DAPCorrect")
						 + PlayerPrefs.GetFloat("HVLCorrect1") + PlayerPrefs.GetFloat("HVLCorrect2")
						 + PlayerPrefs.GetFloat("AssembleCorrect1") + PlayerPrefs.GetFloat("AssembleCorrect2");
						// ^^^


			if (score ==4)
			{
				anatcube.GetComponent<MeshRenderer>().material.color = Color.green;
				anatcube.GetComponent<XRGrabInteractable>().enabled = true;
				anatcube.GetComponent<BoxCollider>().enabled = true;
				PlayerPrefs.SetFloat("FaultsActivated", 0.0f);
			}
		}
		else {

			FaultCube.GetComponent<MeshRenderer>().material.color = Color.red;

			DeactivateFaultCube.GetComponent<MeshRenderer>().material.color = Color.grey;
			DeactivateFaultCube.GetComponent<XRGrabInteractable>().enabled = false;
			DeactivateFaultCube.GetComponent<BoxCollider>().enabled = false;

			HVLcube.GetComponent<MeshRenderer>().material.color = blue;
			Assemblecube.GetComponent<MeshRenderer>().material.color = blue;
			ISLcube.GetComponent<MeshRenderer>().material.color = blue;
			LBAcube.GetComponent<MeshRenderer>().material.color = blue;
			anatcube.GetComponent<MeshRenderer>().material.color = blue;

			anatcube.GetComponent<XRGrabInteractable>().enabled = true;
			anatcube.GetComponent<BoxCollider>().enabled = true;


		}
	}
}