using UnityEngine;



/// <summary>
/// Implementation of StackZone for piles of objects that start full with a set number of ALSheets within. 
/// </summary>
public class PileZone : StackZone
{
	public ALType acceptedType;

	[Header("Initial Stock")]
	public ALSheet sheetPrefab;
	public int initialCount = 5;

	private void Start()
	{
		for (int i = 0; i < initialCount; i++)
		{
			ALSheet sheet = Instantiate(sheetPrefab, stackOrigin.position, stackOrigin.rotation);
			sheet.aLType = acceptedType;
			SeedObject(sheet);
		}
	}

	public override bool CanAccept(ALSheet sheet)
	{
		if (IsFull) return false;
		return sheet.aLType == acceptedType;
	}
}