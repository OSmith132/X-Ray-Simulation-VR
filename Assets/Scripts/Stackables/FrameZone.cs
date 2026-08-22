/// <summary>
/// Implementation of StackZone to allow any Stackable of type T to be added up to capacity.
/// </summary>
public abstract class FrameZone<T> : StackZone<T> where T : Stackable
{
	protected override bool CanAccept(T sheet) => !IsFull;

	/// <summary>
	/// Sums the thickness of all sheets currently in this frame, used by DoseManager to compute total attenuation across mixed frame types.
	/// </summary>
	public override float TotalThicknessMM
	{
		get
		{
			float sum = 0f;
			foreach (var sheet in Sheets)
				sum += sheet.thicknessMM;
			return sum;
		}
	}
}