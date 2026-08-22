using UnityEngine;



/// <summary>
/// Abstract class to structure placement zones stackable objects can be placed in (initial pile or on rack beneath scanner).
/// </summary>
public abstract class PlacementZone : MonoBehaviour
{
	public abstract bool CanAccept(Stackable sheet);
	public abstract void PlaceObject(Stackable sheet);
	public abstract void RemoveObject(Stackable sheet);

	/// <summary>
	/// Total attenuating thickness of sheets currently held in this zone. Defaults to 0; overridden by zones that hold attenuating sheets (e.g. FrameZone).
	/// </summary>
	public virtual float TotalThicknessMM => 0f;
}