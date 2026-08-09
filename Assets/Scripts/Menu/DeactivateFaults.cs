using UnityEngine;

public class DeactivateFaults : MonoBehaviour
{
	public void Deactivate()
	{
		PlayerPrefs.SetFloat("FaultsActivated", 0f);
	}
}