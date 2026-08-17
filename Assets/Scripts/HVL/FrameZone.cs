





/// <summary>
/// Implementation of StackZone to allow any Stackable of type T to be added up to capacity.
/// </summary>
public abstract class FrameZone<T> : StackZone<T> where T : Stackable
{
	protected override bool CanAccept(T sheet) => !IsFull;
}



