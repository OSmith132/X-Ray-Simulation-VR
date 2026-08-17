using UnityEngine;



/// <summary>
/// Manages user scores and equipment faults across all scenes in the simulation.
/// </summary>
public class FaultManagerWrapper : MonoBehaviour
{

	/// <summary>
	/// Activate faults and reset all player scores to 0.
	/// </summary>
	public static void ActivateFaults()
	{
		FaultsManager.ActivateFaults();
	}


	/// <summary>
	/// Deactivate faults but leaves player scores.
	/// </summary>
	public static void DeactivateFaults()
	{
		FaultsManager.DeactivateFaults();
	}



	/// <returns>Sum of player scores across all scenes</returns>
	public static float SumScore()
	{
		return FaultsManager.SumScore();
	}




	
}