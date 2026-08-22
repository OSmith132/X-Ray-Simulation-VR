

public enum AcrylicType { twocm, onecm } // Add more sizes here

/// <summary>
/// Stackable aluminium sheet used for HVL. Grab/release/zone logic is in Stackable.
/// this class only adds the specific type and thickness.
/// </summary>
public class AcrylicSheet : Stackable
{
	public AcrylicType acrylicType;
}