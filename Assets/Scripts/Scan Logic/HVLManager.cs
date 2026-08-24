using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HVLManager : DoseManager
{

	/// <summary>
	/// Calculates the dose to the nearest 2dp and displays them on doseText and doseTextHVL.
	/// </summary>
	public override void CalculateDose()
	{
		float thicknessMM = GetTotalThicknessMM();
		float distTable = Vector3.Distance(source.position, table.position);

		float kVError = 1f;
		if (FaultsManager.FaultsActivated)
			kVError = 1.5f;



		float kV = XRayControlPanel.kV * kVError;
		float mukV = muReference * (Mathf.Pow(kVReference, 3) / Mathf.Pow(kV, 3));

		float dose1 = (1050f * Mathf.Exp(-mukV * thicknessMM)) / (distTable * distTable);
		float dose2 = dose1 * XRayControlPanel.mAs / mAsReference;
		float dose3 = dose2 * (Mathf.Pow(kV, 2) / Mathf.Pow(kVReference * kVError, 2));

		float dose = dose3 * Random.Range(0.975f, 1.025f);

		OutputTextTablet.text = $"Dose: {dose:F2}uGy";
		OutputTextMonitor.text = $"Dose: {dose:F2}uGy";
	}
}