using UnityEngine;



/// <summary>
/// Implementation of StackZone for piles of ALSheets that start full with a set number of sheets within.
/// </summary>
public class ALStackZone : StackZone<ALSheet>
{
	public ALType acceptedType;



	/// <summary>
	/// Assigns the accepted aluminium type to a freshly spawned sheet before it is seeded into the pile.
	/// </summary>
	/// <param name="sheet"></param>
	protected override void ConfigureSheet(ALSheet sheet) => sheet.aLType = acceptedType;





	/// <param name="sheet"></param>
	/// <returns>Returns true if accepted type and not full, false otherwise.</returns>
	protected override bool CanAccept(ALSheet sheet)
	{
		if (IsFull) return false;
		return sheet.aLType == acceptedType;
	}
}