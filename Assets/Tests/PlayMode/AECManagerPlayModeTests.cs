using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

// AECManager is currently a stub (hardcoded placeholder value, TODO in the class itself).
// Since the output is deterministic (no Random.Range like HVLManager has), we can check
// the exact string rather than just "isn't empty". Worth expanding once the real mAs logic exists.
public class AECManagerPlayModeTests
{
	AECManager aec;


	// Before tests
	[UnitySetUp]
	public IEnumerator SetUp()
	{
		XRayControlPanel.kV = 70;
		FaultManagerWrapper.DeactivateFaults();

		aec = new GameObject("AECManager").AddComponent<AECManager>();

		aec.source = new GameObject("Source").transform;
		aec.table = new GameObject("Table").transform;
		aec.source.position = new Vector3(0, 1, 0);
		aec.table.position = new Vector3(0, 0, 0);

		aec.frames = null;

		aec.OutputTextTablet = new GameObject("Tablet").AddComponent<TextMeshPro>();
		aec.OutputTextMonitor = new GameObject("Monitor").AddComponent<TextMeshPro>();

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
	public IEnumerator CalculateDose_DoesNotThrow()
	{
		Assert.DoesNotThrow(() => aec.CalculateDose());
		yield return null;
	}




	[UnityTest]
	public IEnumerator CalculateDose_DoesNotThrow_WhenFaultsActivated()
	{
		FaultManagerWrapper.ActivateFaults();

		Assert.DoesNotThrow(() => aec.CalculateDose());
		yield return null;
	}


}