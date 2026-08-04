using UnityEngine;

public class ActivateFaults : MonoBehaviour
{
	public void ActivateFaultsAction()
	{
		PlayerPrefs.SetFloat("FaultsActivated", 1.0f);
		PlayerPrefs.SetFloat("AssembleCorrect1", 0.0f);
		PlayerPrefs.SetFloat("AssembleCorrect2", 0.0f);
		PlayerPrefs.SetFloat("HVLCorrect1", 0.0f);
		PlayerPrefs.SetFloat("HVLCorrect2", 0.0f);
		PlayerPrefs.SetFloat("DAPCorrect", 0.0f);
		PlayerPrefs.SetFloat("PhantomsCorrect1", 0.0f);
		PlayerPrefs.SetFloat("PhantomsCorrect2", 0.0f);
	}
}