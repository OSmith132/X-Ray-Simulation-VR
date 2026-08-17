using UnityEngine;



/// <summary>
/// Implementation of StackZone for piles of ALSheets that start full with a set number of sheets within.
/// </summary>
public class PileZone : StackZone<ALSheet>
{
	public ALType acceptedType;

	[Header("Initial Stock")]
	public ALSheet sheetPrefab;
	public int initialCount = 5;



	/// <summary>
	/// Spawns and seeds the initial stock of sheets into the pile on scene start.
	/// </summary>
	private void Start()
	{
		for (int i = 0; i < initialCount; i++)
		{
			ALSheet sheet = Instantiate(sheetPrefab, stackOrigin.position, stackOrigin.rotation);
			sheet.aLType = acceptedType;
			SeedObject(sheet);
		}
	}


	/// <param name="sheet"></param>
	/// <returns>Returns true if accepted type and not full, false otherwise.</returns>
	protected override bool CanAccept(ALSheet sheet)
	{
		if (IsFull) return false;
		return sheet.aLType == acceptedType;
	}
}