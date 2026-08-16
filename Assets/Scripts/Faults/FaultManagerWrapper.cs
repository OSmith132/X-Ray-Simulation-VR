using UnityEngine;




public class FaultManagerWrapper : MonoBehaviour
{
	public static void ActivateFaults()
	{
		FaultsManager.ActivateFaults();
	}

	public static void DeactivateFaults()
	{
		FaultsManager.DeactivateFaults();
	}




	public static float SumScore()
	{
		return FaultsManager.SumScore();
	}




	
}