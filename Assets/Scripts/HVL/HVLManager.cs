using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HVLManager : MonoBehaviour
{

	[Header("Beam / Geometry")]
	public Transform source;
	public Transform table;

	[Header("Frames")]
	public ALSheetFrameZone upperFrame;
	public ALSheetFrameZone lowerFrame;

	[Header("UI")]
	public TMP_Text doseText;
	public TMP_Text doseTextHVL;

	[Header("Attenuation Model")]
	public float muReference = 0.21f;
	public float kVReference = 70f;
	public int mAsReference = 10;

	private string sceneName;




	private void Start()
	{
		sceneName = SceneManager.GetActiveScene().name;
	}


	private void Update()
	{
		if (Input.GetButtonDown("Scan"))
			CalculateDose();
	}




	/// <returns>The total thickness of the ALSheets in the upper and lower frames.</returns>
	private float GetTotalThicknessMM()
	{
		return SumFrame(upperFrame) + SumFrame(lowerFrame);
	}



	/// <summary>
	/// Calculates the total thickness of the ALSheets in a given frame.
	/// </summary>
	/// <param name="frame"></param>
	/// <returns>The total thickness for the given frame.</returns>
	private float SumFrame(ALSheetFrameZone frame)
	{
		float sum = 0f;
		if (frame == null) return sum;
		foreach (var sheet in frame.Sheets)
			sum += sheet.thicknessMM;
		return sum;
	}



	/// <summary>
	/// Calculates the dose to the nearest 2dp and displays them on doseText and doseTextHVL.
	/// </summary>
	public void CalculateDose()
	{
		float alThicknessMM = GetTotalThicknessMM();
		float distTable = Vector3.Distance(source.position, table.position);

		float kVError = 1f;
		if (FaultsManager.FaultsActivated && sceneName == "HVL")
			kVError = 1.5f;



		float kV = XRayControlPanel.kV * kVError;
		float mukV = muReference * (Mathf.Pow(kVReference, 3) / Mathf.Pow(kV, 3));

		float dose1 = (1050f * Mathf.Exp(-mukV * alThicknessMM)) / (distTable * distTable);
		float dose2 = dose1 * XRayControlPanel.mAs / mAsReference;
		float dose3 = dose2 * (Mathf.Pow(kV, 2) / Mathf.Pow(kVReference * kVError, 2));

		float dose = dose3 * Random.Range(0.975f, 1.025f);

		doseText.text = $"Dose: {dose:F2}uGy";
		doseTextHVL.text = $"Dose: {dose:F2}uGy";
	}
}

