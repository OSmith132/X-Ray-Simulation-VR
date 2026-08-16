using UnityEngine;



/// <summary>
/// Implementation of StackZone to allow any type of ALSheet to be added
/// </summary>
public class FrameZone : StackZone
{
	public override bool CanAccept(ALSheet sheet) => !IsFull;
}