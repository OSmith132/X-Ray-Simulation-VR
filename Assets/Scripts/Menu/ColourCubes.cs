using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FaultScoreManager : MonoBehaviour
{
	private GameObject HVLcube;
	private GameObject Assemblecube;
	private GameObject ISLcube;
	private GameObject anatcube;
	private GameObject LBAcube;

	void Start()
	{
		HVLcube = transform.Find("HVL/Cube").gameObject;
		Assemblecube = transform.Find("Build X-Ray Tube/Cube").gameObject;
		ISLcube = transform.Find("DAP Test/Cube").gameObject;
		anatcube = transform.Find("Anatomy/Cube").gameObject;
		LBAcube = transform.Find("Phantoms/Cube").gameObject;
	}

	void Update()
	{
		if (PlayerPrefs.GetFloat("FaultsActivated") == 1.0f)
		{
			Assemblecube.GetComponent<MeshRenderer>().material.color =
				(PlayerPrefs.GetFloat("AssembleCorrect1") == 1 && PlayerPrefs.GetFloat("AssembleCorrect2") == 1)
				? Color.green : Color.red;

			HVLcube.GetComponent<MeshRenderer>().material.color =
				(PlayerPrefs.GetFloat("HVLCorrect1") == 1 && PlayerPrefs.GetFloat("HVLCorrect2") == 1)
				? Color.green : Color.red;

			ISLcube.GetComponent<MeshRenderer>().material.color =
				(PlayerPrefs.GetFloat("DAPCorrect") == 1) ? Color.green : Color.red;

			LBAcube.GetComponent<MeshRenderer>().material.color =
				(PlayerPrefs.GetFloat("PhantomsCorrect1") == 1 && PlayerPrefs.GetFloat("PhantomsCorrect2") == 1)
				? Color.green : Color.red;

			anatcube.GetComponent<MeshRenderer>().material.color = Color.grey;
			anatcube.GetComponent<XRGrabInteractable>().enabled = false;
			anatcube.GetComponent<BoxCollider>().enabled = false;

			float score = PlayerPrefs.GetFloat("PhantomsCorrect1") + PlayerPrefs.GetFloat("PhantomsCorrect2")
						 + PlayerPrefs.GetFloat("DAPCorrect")
						 + PlayerPrefs.GetFloat("HVLCorrect1") + PlayerPrefs.GetFloat("HVLCorrect2")
						 + PlayerPrefs.GetFloat("AssembleCorrect1") + PlayerPrefs.GetFloat("AssembleCorrect2");

			if (score == 7)
			{
				anatcube.GetComponent<MeshRenderer>().material.color = Color.green;
				anatcube.GetComponent<XRGrabInteractable>().enabled = true;
				anatcube.GetComponent<BoxCollider>().enabled = true;
				PlayerPrefs.SetFloat("FaultsActivated", 0.0f);
			}
		}
	}
}