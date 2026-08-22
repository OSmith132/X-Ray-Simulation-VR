using UnityEngine;

/// <summary>
/// Dose manager for AEC (Automatic Exposure Control).
/// </summary>
public class AECManager : DoseManager
{


	public override void CalculateDose()
	{

		// TODO: implement mAs calculations here and display them on the passed in displays! Check DoseManager for more details.

		// To use in calculations
		float thicknessMM = GetTotalThicknessMM();
		float distTable = Vector3.Distance(source.position, table.position);
		Debug.Log($"Acrylic thickness  = {thicknessMM}mm");
		Debug.Log($"Distance to sensor = {distTable}m");


		// for when faults are active
		if (FaultsManager.FaultsActivated) { // Fault goes here }
			
		// How to get the XRay values
		float kV = XRayControlPanel.kV;
		

		Debug.Log("Calculating mAs...");



		float value = 12.34f;
		OutputTextTablet.text = $"value: {value:F2}uGy";
		OutputTextMonitor.text = $"Value: {value:F2}uGy";
	}



}