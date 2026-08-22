using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Base class for dose managers (HVL, AEC etc.) sharing beam geometry, frame thickness summation, and UI display.
/// </summary>
public abstract class DoseManager : MonoBehaviour
{

	[Header("Beam / Geometry")]
	public Transform source;
	public Transform table;

	[Header("Frames")]
	public List<PlacementZone> frames;

	[Header("UI")]
	public TMP_Text OutputTextTablet;
	public TMP_Text OutputTextMonitor;

	[Header("Attenuation Model")]
	public float muReference = 0.21f;
	public float kVReference = 70f;
	public int mAsReference = 10;

	protected string sceneName;




	private void Start()
	{
		sceneName = SceneManager.GetActiveScene().name;
	}







	/// <returns>The total thickness of the sheets across all assigned frames.</returns>
	protected float GetTotalThicknessMM()
	{
		float sum = 0f;
		if (frames == null) return sum;
		foreach (var frame in frames)
			sum += frame.TotalThicknessMM;
		return sum;
	}



	/// <summary>
	/// Calculates the dose and displays it on OutputTextTablet and OutputTextMonitor.
	/// </summary>
	public abstract void CalculateDose();
}