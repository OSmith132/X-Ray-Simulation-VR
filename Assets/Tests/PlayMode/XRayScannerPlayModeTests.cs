using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class XRayScannerPlayModeTests
{
	XRayScanner scanner;


	// Add a transform at a given world position
	static Transform MakeAt(string name, Vector3 position, float scale = 1f)
	{
		var obj = new GameObject(name);
		obj.transform.position = position;
		obj.transform.localScale = Vector3.one * scale;
		return obj.transform;
	}



	// Before tests
	[UnitySetUp]
	public IEnumerator SetUp()
	{
		XRayControlPanel.kV = 70;
		XRayControlPanel.mAs = 10;

		// objects Start() looks up by name
		new GameObject("RadiographImg");
		MakeAt("Collimator Guide Light", new Vector3(0, 2, 0));
		MakeAt("Col 3 (Left)", new Vector3(0, 1, -0.5f), 0.05f);
		MakeAt("Col 4 (Right)", new Vector3(0, 1, 0.5f), 0.05f);
		MakeAt("Col 1 (Near)", new Vector3(-0.5f, 1, 0), 0.05f);
		MakeAt("Col 2 (Far)", new Vector3(0.5f, 1, 0), 0.05f);
		MakeAt("Detector", new Vector3(0, 0, 0));
		// "Detector Visible" left out as LargeIonisationChamber can be null in calculateDAP()


		scanner = new GameObject("XRayScanner").AddComponent<XRayScanner>();




		var lightToggleObject = new GameObject("LightToggle");
		lightToggleObject.AddComponent<Light>();
		var lightToggle = lightToggleObject.AddComponent<LightToggle>();

		TestReflectionUtils.SetPrivateField(scanner, "lightToggle", lightToggle);




		// public fields, can just assign directly
		scanner.DAPText = new GameObject("DAPText").AddComponent<TextMeshPro>();
		scanner.DoseText = new GameObject("DoseText").AddComponent<TextMeshPro>();
		scanner.LFarea = new GameObject("LFarea").AddComponent<TextMeshPro>();

		// to stop that annoying warning
		new GameObject("AudioListener").AddComponent<AudioListener>();

		yield return null;
	}



	// After tests
	[UnityTearDown]
	public IEnumerator TearDown()
	{
		foreach (var go in Object.FindObjectsOfType<GameObject>())
			Object.Destroy(go);
		yield return null;
	}




	[UnityTest]
	public IEnumerator Scan_WritesNonZeroDAP()
	{
		scanner.Scan();
		yield return null;

		Assert.AreNotEqual("DAP: 0.00 cGycm\xB2", scanner.DAPText.text); // ensure its actually being calculated as its not by default in new scenes
	}




	// same as above as dose or whatever else is being calculated isn't always calculated in new scenes.
	[UnityTest]
	public IEnumerator Scan_HigherKV_ChangesDoseReading()
	{
		XRayControlPanel.kV = 50;
		scanner.Scan();
		yield return null;
		string doseAtLowKV = scanner.DoseText.text;

		XRayControlPanel.kV = 90;
		scanner.Scan();
		yield return null;
		string doseAtHighKV = scanner.DoseText.text;

		Assert.AreNotEqual(doseAtLowKV, doseAtHighKV);
	}






}