using UnityEngine;

/// <summary>
/// Handles the the test cube demo in the Assemble X-ray Room scene. This script is attached directly to the test cube.

/// </summary>
public class AssembleTestCube : MonoBehaviour
{


	GameObject AnodeSpinner;
	GameObject ElectronBeam;
	GameObject XrayLight;

	GameObject TubeTest;
	GameObject PanelTest;

	GameObject ColHousing;

	float increase;
	bool revealed;

	void Start()
	{

		AnodeSpinner = GameObject.Find("AnodeA");
		ElectronBeam = GameObject.Find("Electron Beam");
		XrayLight = GameObject.Find("Collimator Guide Light");

		TubeTest = GameObject.Find("Tube Cover A");
		PanelTest = GameObject.Find("PanelHandleA");

		ColHousing = GameObject.Find("ColCover");
	}

	void Update()
	{

		if (revealed)
		{
			// Keep spinning the anode faster the longer it's revealed, capped at 200.
			increase += (Time.deltaTime * 10f);
			AnodeSpinner.transform.Rotate(Vector3.up, increase);

			if (increase >= 200)
			{
				increase = 200;
			}
		}
	}



	public void Reveal()
	{
		if (revealed) return;
		revealed = true;

		ColHousing.gameObject.GetComponent<MeshRenderer>().enabled = false;
		TubeTest.SetActive(false);
		PanelTest.SetActive(false);

		ElectronBeam.GetComponent<Light>().intensity = 500;
		XrayLight.GetComponent<Light>().intensity = 10;


		GetComponent<MeshRenderer>().material.color = Color.green;
	}
}