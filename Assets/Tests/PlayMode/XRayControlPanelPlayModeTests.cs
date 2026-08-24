using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class XRayControlPanelPlayModeTests
{
	XRayControlPanel panel;


	// Add text to an object
	static GameObject MakeText(string name)
	{
		var obj = new GameObject(name);
		obj.AddComponent<Text>();
		return obj;
	}


	// cubes come with a Renderer and Collider built in so good for reading colours
	static GameObject MakeRenderable(string name)
	{
		var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		obj.name = name;
		return obj;
	}



	// Before tessts
	[UnitySetUp]
	public IEnumerator SetUp()
	{
		XRayControlPanel.mAs = 10;
		XRayControlPanel.kV = 70;

		// text fields Start() always writes to
		foreach (var n in new[] { "mAsText", "kVText", "mAsText1", "kVText1" })
		{
			MakeText(n);
		}


		// need to read a Renderer from these
		foreach (var n in new[] {
			"a1", "Free Roam Button", "Vertical Button", "Panel Handle",
			"Collimator Vertical Out", "Prime", "Scan", "Scan Ready" })
		{
			MakeRenderable(n);
		}


		// only needs the Transform
		new GameObject("Col 1 (Near)");
		new GameObject("Col 2 (Far)");



		foreach (var n in new[] { "Click", "primeON" })
		{
			new GameObject(n).AddComponent<AudioSource>();
		}



		panel = new GameObject("XRayControlPanel").AddComponent<XRayControlPanel>();

		// Start() needs these serialized fields, no public setters on the real class
		TestReflectionUtils.SetPrivateField(panel, "handTransforms", new Transform[0]);



		// Add everything on the hand controller we need
		var handleControllerGO = new GameObject("HandleController");
		handleControllerGO.AddComponent<Rigidbody>(); // ConfigurableJoint requires a Rigidbody on the same object
		var joint = handleControllerGO.AddComponent<ConfigurableJoint>();

		var handleController = handleControllerGO.AddComponent<XRayHandleController>();
		TestReflectionUtils.SetPrivateField(handleController, "configurableJoint", joint);

		TestReflectionUtils.SetPrivateField(panel, "xrayHandleController", handleController);



		// To stop that annoying warning
		new GameObject("AudioListener").AddComponent<AudioListener>();

		yield return null;
	}





	// Ater tests
	[UnityTearDown]
	public IEnumerator TearDown()
	{
		foreach (var go in Object.FindObjectsOfType<GameObject>())
			Object.Destroy(go);
		yield return null;
	}




	[UnityTest]
	public IEnumerator PressFreeToggle_HighlightsOnlyFreeButton()
	{
		panel.PressVerticalToggle(); // start from the other state first
		yield return null;

		panel.PressFreeToggle();
		yield return null;

		var free = GameObject.Find("Free Roam Button").GetComponent<Renderer>().material.color;
		var vertical = GameObject.Find("Vertical Button").GetComponent<Renderer>().material.color;

		Assert.AreEqual(Color.green, free);
		Assert.AreNotEqual(Color.green, vertical);
	}

}