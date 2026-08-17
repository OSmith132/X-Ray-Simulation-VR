


public enum ALType { OneMM, PointTwoMM }

/// <summary>
/// Stackable aluminium sheet used for HVL. Grab/release/zone logic is in Stackable.
/// this class only adds the specific type and thickness.
/// </summary>
public class ALSheet : Stackable
{
	public ALType aLType;
	public float thicknessMM = 1f;
}