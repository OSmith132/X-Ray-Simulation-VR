using UnityEngine;



/// <summary>
/// Abstract class to structure placement zones stackable objects can be placed in (pile or on rack).
/// </summary>
public abstract class PlacementZone : MonoBehaviour
{
	public abstract bool CanAccept(Stackable sheet);
	public abstract void PlaceObject(Stackable sheet);
	public abstract void RemoveObject(Stackable sheet);
}