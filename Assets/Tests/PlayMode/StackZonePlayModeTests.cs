using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StackZonePlayModeTests
{
	// concrete Stackable (such as ALSheet or AcrylicSheet)
	private class TestSheet : Stackable { }

	// FrameZone<T> is abstract
	private class TestFrameZone : FrameZone<TestSheet> { }


	TestFrameZone frame;




	// Before tests
	[UnitySetUp]
	public IEnumerator SetUp()
	{
		var originGO = new GameObject("StackOrigin");

		frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();
		frame.stackOrigin = originGO.transform;
		frame.maxCapacity = 3;

		yield return null; // let Awake run before start placing sheets
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
	public IEnumerator PlaceObject_AddsSheetToStack()
	{
		var sheet = new GameObject("Sheet").AddComponent<TestSheet>();
		yield return null; // let the sheet's Awake run too


		frame.PlaceObject(sheet);
		yield return null;

		Assert.AreEqual(1, frame.Count);
		Assert.AreEqual(sheet, frame.Sheets[0]);
	}






	[UnityTest]
	public IEnumerator PlaceObject_MakesSheetFixed()
	{
		var sheet = new GameObject("Sheet").AddComponent<TestSheet>();
		yield return null;

		frame.PlaceObject(sheet);
		yield return null;


		var rigidBody = sheet.GetComponent<Rigidbody>();

		Assert.IsTrue(rigidBody.isKinematic);
		Assert.IsFalse(rigidBody.useGravity);
	}




	[UnityTest]
	public IEnumerator RemoveObject_TakesSheetOutOfStack()
	{
		var sheet = new GameObject("Sheet").AddComponent<TestSheet>();
		yield return null;

		frame.PlaceObject(sheet);
		yield return null;

		frame.RemoveObject(sheet);
		yield return null;


		Assert.AreEqual(0, frame.Count);

	}




	[UnityTest]
	public IEnumerator PlaceObject_OnlyTopSheetStaysGrabbable()
	{
		var sheetA = new GameObject("SheetA").AddComponent<TestSheet>();
		var sheetB = new GameObject("SheetB").AddComponent<TestSheet>();
		yield return null;

		frame.PlaceObject(sheetA);
		yield return null;
		frame.PlaceObject(sheetB);
		yield return null;


		Assert.IsFalse(sheetA.GetComponent<XRGrabInteractable>().enabled);
		Assert.IsTrue(sheetB.GetComponent<XRGrabInteractable>().enabled);
	}


}