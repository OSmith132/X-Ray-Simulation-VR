using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class HVLManagerPlayModeTests
{
	HVLManager hvl;


	// Before tests
	[UnitySetUp]
	public IEnumerator SetUp()
	{
		XRayControlPanel.kV = 70;
		XRayControlPanel.mAs = 10;
		FaultManagerWrapper.DeactivateFaults();

		hvl = new GameObject("HVLManager").AddComponent<HVLManager>();

		hvl.source = new GameObject("Source").transform;
		hvl.table = new GameObject("Table").transform;
		hvl.source.position = new Vector3(0, 1, 0);
		hvl.table.position = new Vector3(0, 0, 0);

		hvl.frames = null; // no PlacementZone, so thickness just sums to 0

		hvl.OutputTextTablet = new GameObject("Tablet").AddComponent<TextMeshPro>();
		hvl.OutputTextMonitor = new GameObject("Monitor").AddComponent<TextMeshPro>();

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
	public IEnumerator CalculateDose_WritesDoseToBothDisplays()
	{
		hvl.CalculateDose();
		yield return null;

		Assert.IsTrue(hvl.OutputTextTablet.text.Contains("Dose:"));
		Assert.IsTrue(hvl.OutputTextMonitor.text.Contains("Dose:"));
	}




	[UnityTest]
	public IEnumerator CalculateDose_DoublingMAs_RoughlyDoublesDose()
	{
		// dose scales linearly with mAs, allow for the +/-2.5% random noise either side
		XRayControlPanel.mAs = 10;
		hvl.CalculateDose();
		float doseAt10 = ExtractDoseValue(hvl.OutputTextTablet.text);

		XRayControlPanel.mAs = 20;
		hvl.CalculateDose();
		float doseAt20 = ExtractDoseValue(hvl.OutputTextTablet.text);

		yield return null;

		Assert.Greater(doseAt20, doseAt10 * 1.8f); // - 10%
		Assert.Less(doseAt20, doseAt10 * 2.2f);    // + 20%
	}




	[UnityTest]
	public IEnumerator CalculateDose_HigherKV_ChangesDoseReading()
	{
		XRayControlPanel.kV = 50;
		hvl.CalculateDose();
		string doseAtLowKV = hvl.OutputTextTablet.text;

		XRayControlPanel.kV = 90;
		hvl.CalculateDose();
		string doseAtHighKV = hvl.OutputTextTablet.text;

		yield return null;

		Assert.AreNotEqual(doseAtLowKV, doseAtHighKV);
	}




	[UnityTest]
	public IEnumerator CalculateDose_FaultsActivated_ChangesReading() // REMOVE THIS TEST IF YOU CHANGED THE FAULT IN HLV SCENE
	{


		FaultManagerWrapper.DeactivateFaults();
		hvl.CalculateDose();
		string doseWithoutFault = hvl.OutputTextTablet.text;



		FaultManagerWrapper.ActivateFaults();
		hvl.CalculateDose();
		string doseWithFault = hvl.OutputTextTablet.text;



		yield return null;

		Assert.AreNotEqual(doseWithoutFault, doseWithFault);
	}



	// pulls the dose out of dose meter readings
	static float ExtractDoseValue(string text)
	{
		string numberPart = text.Replace("Dose: ", "").Replace("uGy", "");
		return float.Parse(numberPart);
	}


}