using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Implementation of PlacementZone to allow basic addition and removal from stacks of any Stackable type T.
/// </summary>
public abstract class StackZone<T> : PlacementZone where T : Stackable
{
	[Header("Stack Layout")]
	public Transform stackOrigin;
	public Vector3 stackAxis = Vector3.up;
	public float sheetSpacing = 0.005f;
	public int maxCapacity = 5;

	[Header("Initial Stock")]
	public T sheetPrefab;
	public int initialCount = 0;

	protected readonly List<T> stack = new List<T>();

	public IReadOnlyList<T> Sheets => stack;
	public int Count => stack.Count;
	public bool IsFull => stack.Count >= maxCapacity;



	/// <summary>
	/// Spawns and seeds the initial stock of sheets into the pile on scene start.
	/// </summary>
	private void Start()
	{
		for (int i = 0; i < initialCount; i++)
		{
			T sheet = Instantiate(sheetPrefab, stackOrigin.position, stackOrigin.rotation);
			ConfigureSheet(sheet);
			SeedObject(sheet);
		}
	}



	/// <summary>
	/// Hook for subclasses to configure a freshly spawned sheet (e.g. assigning its accepted type) before it is seeded into the stack.
	/// </summary>
	/// <param name="sheet"></param>
	protected virtual void ConfigureSheet(T sheet) { }



	/// <summary>
	/// Implements PlacementZone.CanAccept by checking the object is of type T before deferring to the type-specific CanAccept(T) below.
	/// </summary>
	public override bool CanAccept(Stackable sheet) => sheet is T typed && CanAccept(typed);


	/// <summary>
	/// Type-specific rule implemented by subclasses (e.g. PileZone checks aLType, FrameZone checks capacity and aLType).
	/// </summary>
	protected abstract bool CanAccept(T sheet);



	/// <summary>
	/// Adds a Stackable to the stack, given the stack is not full.
	/// </summary>
	/// <param name="sheet"></param>
	public override void PlaceObject(Stackable sheet)
	{
		if (sheet is not T typed) return;

		if (!stack.Contains(typed))
			stack.Add(typed);


		var rb = typed.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;


		typed.SetCurrentZone(this);
		Restack();
	}




	/// <summary>
	/// Removes a Stackable from the stack.
	/// </summary>
	/// <param name="sheet"></param>
	public override void RemoveObject(Stackable sheet)
	{
		if (sheet is not T typed) return;

		if (stack.Remove(typed))
			Restack();
	}



	/// <summary>
	/// Seed the stack with a given number of Stackables.
	/// </summary>
	/// <param name="sheet"></param>
	public void SeedObject(T sheet)
	{
		if (stack.Contains(sheet)) return;
		stack.Add(sheet);

		var rb = sheet.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;

		sheet.SetCurrentZone(this);
		Restack();
	}



	/// <summary>
	/// Repositions all stacked objects along the stack axis and marks only the top one grabbable.
	/// </summary>
	protected void Restack()
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