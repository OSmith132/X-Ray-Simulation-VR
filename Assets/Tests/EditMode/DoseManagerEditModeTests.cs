using NUnit.Framework;
using UnityEngine;

public class DoseManagerEditModeTests
{
	// GetTotalThicknessMM is protected on an abstract class, so we need a
	// small test-only subclass to expose it and give CalculateDose a body
	private class TestDoseManager : DoseManager
	{
		public override void CalculateDose() { }

		public float ThicknessForTest => GetTotalThicknessMM();
	}




	[Test]
	public void GetTotalThicknessMM_ReturnsZero_WhenFramesIsNull()
	{
		var manager = new GameObject("TestDoseManager").AddComponent<TestDoseManager>();
		manager.frames = null;

		Assert.AreEqual(0f, manager.ThicknessForTest);

		Object.DestroyImmediate(manager.gameObject);
	}





	[Test]
	public void GetTotalThicknessMM_ReturnsZero_WhenFramesIsEmpty()
	{
		var manager = new GameObject("TestDoseManager").AddComponent<TestDoseManager>();
		manager.frames = new System.Collections.Generic.List<PlacementZone>();

		Assert.AreEqual(0f, manager.ThicknessForTest);

		Object.DestroyImmediate(manager.gameObject);
	}





}