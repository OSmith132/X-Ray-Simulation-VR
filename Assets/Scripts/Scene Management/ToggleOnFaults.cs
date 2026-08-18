using UnityEngine;

public class ToggleOnFaults : MonoBehaviour
{


    void Start()
    {
        // Only show MCQ questions if there is a fault to find.
		gameObject.SetActive(FaultsManager.FaultsActivated);

    }


}
