using UnityEngine;


/// <summary>
/// Abstract class to structure placement zones the ALSheets can be placed in (pile or on rack).
/// </summary>
public abstract class PlacementZone : MonoBehaviour
{
	public abstract bool CanAccept(ALSheet sheet);
	public abstract void PlaceObject(ALSheet sheet);
	public abstract void RemoveObject(ALSheet sheet);
}