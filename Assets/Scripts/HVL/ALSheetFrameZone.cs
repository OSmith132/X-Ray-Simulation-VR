/// <summary>
/// Concrete FrameZone for ALSheets. Unity can't add a generic MonoBehaviour directly to a GameObject, so this subclass is what gets attached in the Inspector.
/// </summary>
public class ALSheetFrameZone : FrameZone<ALSheet> { }