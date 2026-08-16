using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Implementation of PlacementZone to allow basic addition and removal from stacks of ALSheets.
/// </summary>
public abstract class StackZone : PlacementZone
{
	[Header("Stack Layout")]
	public Transform stackOrigin;
	public Vector3 stackAxis = Vector3.up;
	public float sheetSpacing = 0.004f;
	public int maxCapacity = 5;

	protected readonly List<ALSheet> stack = new List<ALSheet>();

	public IReadOnlyList<ALSheet> Sheets => stack;
	public int Count => stack.Count;
	public bool IsFull => stack.Count >= maxCapacity;





	public override void PlaceObject(ALSheet sheet)
	{
		if (!stack.Contains(sheet))
			stack.Add(sheet);



		var rb = sheet.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;


		sheet.SetCurrentZone(this); 
		Restack();
	}
	




	public override void RemoveObject(ALSheet sheet)
	{ 
		if (stack.Remove(sheet))
			Restack();
	}




	public void SeedObject(ALSheet sheet)
	{
		if (stack.Contains(sheet)) return;
		stack.Add(sheet);

		var rb = sheet.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;

		sheet.SetCurrentZone(this); 
		Restack();
	}




	protected virtual void Restack()
	{


		Vector3 axis = stackOrigin.TransformDirection(stackAxis.normalized);


		for (int i = 0; i < stack.Count; i++)
		{
			Vector3 pos = stackOrigin.position + axis * (sheetSpacing * i);
			stack[i].transform.SetPositionAndRotation(pos, stackOrigin.rotation);
			stack[i].SetGrabbable(i == stack.Count - 1);
		}
	}
}