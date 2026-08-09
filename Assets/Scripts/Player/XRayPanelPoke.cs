using UnityEngine;

/// <summary>
/// Attach one of these to each hand's Poke Point. Forwards touch events to the XrayControlPanel manager, 
/// so either hand can operate any panel button without being explicitly referenced in DetectTouch.cs
/// </summary>
public class XRayPanelPoke : MonoBehaviour
{

	void OnTriggerEnter(Collider touch)
	{
		var panel = XRayControlPanel.Instance;
		if (panel == null) return;


		switch (touch.gameObject.name)
		{
			case "mAs_up": panel.PressMAsUp(); break;
			case "mAs_down": panel.PressMAsDown(); break;
			case "kV_up": panel.PressKVUp(); break;
			case "kV_down": panel.PressKVDown(); break;
			case "freetog": panel.PressFreeToggle(); break;
			case "verticaltog": panel.PressVerticalToggle(); break;
			case "Collimator Vertical Out": panel.PressCollimatorVerticalOut(); break;
			case "Collimator Vertical In": panel.PressCollimatorVerticalIn(); break;
			case "Collimator Horizontal Out": panel.PressCollimatorHorizontalOut(); break;
			case "Collimator Horizontal In": panel.PressCollimatorHorizontalIn(); break;
		}
	}



	void OnTriggerExit(Collider notouch)
	{
		var panel = XRayControlPanel.Instance;
		if (panel == null) return;


		switch (notouch.gameObject.name)
		{
			case "mAs_up": panel.ReleaseMAsUp(); break;
			case "mAs_down": panel.ReleaseMAsDown(); break;
			case "kV_up": panel.ReleaseKVUp(); break;
			case "kV_down": panel.ReleaseKVDown(); break;
			case "Collimator Vertical Out": panel.ReleaseCollimatorVerticalOut(); break;
			case "Collimator Vertical In": panel.ReleaseCollimatorVerticalIn(); break;
			case "Collimator Horizontal Out": panel.ReleaseCollimatorHorizontalOut(); break;
			case "Collimator Horizontal In": panel.ReleaseCollimatorHorizontalIn(); break;
		}
	}
}