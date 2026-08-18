using UnityEngine;

public class ToggleMCQ : MonoBehaviour
{


    void Start()
    {
        // Only show MCQ questions if there is a fault to find.
		gameObject.SetActive(FaultsManager.FaultsActivated);

    }


}
