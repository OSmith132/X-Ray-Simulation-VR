using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FrameZoneEditModeTests
{
	// concrete Stackable (such as ALSheet or AcrylicSheet)
	private class TestSheet : Stackable { }

	// a different Stackable type to above
	private class OtherSheet : Stackable { }

	// FrameZone<T> is abstract
	private class TestFrameZone : FrameZone<TestSheet> { }





	[Test]
	public void TotalThicknessMM_SumsAllSheetsInStack()
	{
		var frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();
		var stack = TestReflectionUtils.GetPrivateField<List<TestSheet>>(frame, "stack");


		AddSheet(stack, 1.5f);
		AddSheet(stack, 2f);
		AddSheet(stack, 0.5f);

		Assert.AreEqual(4f, frame.TotalThicknessMM, 0.0001f);

		Object.DestroyImmediate(frame.gameObject);
	}





	[Test]
	public void TotalThicknessMM_IsZero_WhenStackIsEmpty()
	{
		var frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();

		Assert.AreEqual(0f, frame.TotalThicknessMM);


		Object.DestroyImmediate(frame.gameObject);
	}




	[Test]
	public void CanAccept_RejectsSheet_WhenFrameIsFull()
	{
		var frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();
		frame.maxCapacity = 2;

		var stack = TestReflectionUtils.GetPrivateField<List<TestSheet>>(frame, "stack");

		AddSheet(stack, 1f);
		AddSheet(stack, 1f); // stack is now at capacity

		var extraSheet = new GameObject("ExtraSheet").AddComponent<TestSheet>();

		Assert.IsFalse(frame.CanAccept(extraSheet));


		Object.DestroyImmediate(frame.gameObject);
		Object.DestroyImmediate(extraSheet.gameObject);
	}






	[Test]
	public void CanAccept_AcceptsSheet_WhenNotFull()
	{
		var frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();
		frame.maxCapacity = 5;

		var sheet = new GameObject("Sheet").AddComponent<TestSheet>();

		Assert.IsTrue(frame.CanAccept(sheet));


		Object.DestroyImmediate(frame.gameObject);
		Object.DestroyImmediate(sheet.gameObject);
	}




	[Test]
	public void CanAccept_RejectsWrongSheetType()
	{
		var frame = new GameObject("TestFrameZone").AddComponent<TestFrameZone>();
		var otherSheet = new GameObject("OtherSheet").AddComponent<OtherSheet>();

		Assert.IsFalse(frame.CanAccept(otherSheet));

		Object.DestroyImmediate(frame.gameObject);
		Object.DestroyImmediate(otherSheet.gameObject);
	}





	// adds a sheet with a given thickness into the stack
	static void AddSheet(List<TestSheet> stack, float thicknessMM)
	{
		var sheet = new GameObject("Sheet").AddComponent<TestSheet>();
		sheet.thicknessMM = thicknessMM;
		stack.Add(sheet);
	}



}