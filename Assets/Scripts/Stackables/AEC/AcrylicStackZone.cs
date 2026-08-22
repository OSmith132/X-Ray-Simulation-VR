/// <summary>
/// Implementation of StackZone for piles of AcrylicSheets that start full with a set number of sheets within.
/// </summary>
public class AcrylicStackZone : StackZone<AcrylicSheet>
{
	public AcrylicType acceptedType;



	/// <summary>
	/// Assigns the accepted acrylic type to a freshly spawned sheet before it is seeded into the pile.
	/// </summary>
	/// <param name="sheet"></param>
	protected override void ConfigureSheet(AcrylicSheet sheet)
	{
		sheet.acrylicType = acceptedType;
	}


	/// <param name="sheet"></param>
	/// <returns>Returns true if accepted type and not full, false otherwise.</returns>
	protected override bool CanAccept(AcrylicSheet sheet)
	{
		if (IsFull) return false;
		return sheet.acrylicType == acceptedType;
	}
}