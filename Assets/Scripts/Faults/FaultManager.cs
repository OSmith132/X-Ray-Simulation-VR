using UnityEngine;

public static class FaultsManager
{
	public static bool FaultsActivated { get; private set; } = false;

	public static float AssembleCorrect { get; private set; } = 0f;
	public static float HVLCorrect { get; private set; } = 0f;
	public static float DAPCorrect { get; private set; } = 0f;
	public static float PhantomsCorrect { get; private set; } = 0f;

	public static void SetAssembleCorrect(float value) => AssembleCorrect = value;
	public static void SetHVLCorrect(float value) => HVLCorrect = value;
	public static void SetDAPCorrect(float value) => DAPCorrect = value;
	public static void SetPhantomsCorrect(float value) => PhantomsCorrect = value;

	public static void ActivateFaults()
	{
		FaultsActivated = true;

		AssembleCorrect = 0f;
		HVLCorrect = 0f;
		DAPCorrect = 0f;
		PhantomsCorrect = 0f;
	}

	public static void DeactivateFaults()
	{
		FaultsActivated = false;
	}

	public static float SumScore()
	{
		return AssembleCorrect + HVLCorrect + DAPCorrect + PhantomsCorrect;
	}
}