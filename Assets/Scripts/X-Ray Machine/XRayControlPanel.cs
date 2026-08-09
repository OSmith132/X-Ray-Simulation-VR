using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Single source of truth for the x-ray panel: mAs/kV readouts, click sounds,
/// the panel handle highlight, the free-roam/fixed-vertical toggle, and the
/// collimator width/height adjustment buttons.
///
/// This is a singleton that lives once in the scene (e.g. on the panel
/// itself, or a dedicated manager object) - NOT on either hand. Each hand's
/// Poke Point instead carries an XrayPanelPoke script, which just forwards
/// touch events here via Instance. That way both hands can operate every
/// button without duplicating any of the Find() lookups or UI state.
/// - Oliver
/// </summary>
public class XRayControlPanel : MonoBehaviour
{
	public static XRayControlPanel Instance { get; private set; }

	[Header("Hands (for panel handle proximity highlight)")]
	
	[SerializeField, Tooltip("Drag both the left and right hand transforms in here.")] Transform[] handTransforms;

	[Header("Panel Handle"), Tooltip("The X-Ray panel handle ")]
	[SerializeField] XRayHandleController xrayHandleController;

	// Static so DetectTouch (and anything else) can still read the current
	// exposure settings.
	public static int mAs = 10;
	public static int kV = 70;

	// Was public on DetectTouch before - if other scripts referenced
	// DetectTouch.controlFreeMode, repoint them at XrayControlPanel.Instance.controlFreeMode.
	//public bool controlFreeMode;

	Text mAsText;
	Text kVText;
	Text mAsText1;
	Text kVText1;

	GameObject arrow1;
	GameObject arrow2;
	GameObject arrow3;
	GameObject arrow4;
	GameObject arrow1s;
	GameObject arrow2s;
	GameObject arrow3s;
	GameObject arrow4s;

	GameObject click;
	GameObject clickL;

	GameObject FreeButton;
	GameObject VerticalButton;

	GameObject Handle;
	GameObject HandleRef;
	GameObject XrayHead;

	GameObject ColUp;
	GameObject ColDown;
	GameObject ColLeft;
	GameObject ColRight;
	GameObject Col1;
	GameObject Col2;
	GameObject Col3;
	GameObject Col4;
	float incrament = 0.075f;

	Color OriginalArrowColor;
	Color OriginalHandleColor;
	Color OriginalUnpressed;
	Color OriginalColButton;

	void Awake()
	{

		// Ensure only one of these in the scene
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}



	void Start()
	{
		mAsText = GameObject.Find("mAsText").GetComponent<Text>();
		kVText = GameObject.Find("kVText").GetComponent<Text>();
		mAsText1 = GameObject.Find("mAsText1").GetComponent<Text>();
		kVText1 = GameObject.Find("kVText1").GetComponent<Text>();

		arrow1 = GameObject.Find("a1");
		arrow2 = GameObject.Find("a2");
		arrow3 = GameObject.Find("a3");
		arrow4 = GameObject.Find("a4");
		arrow1s = GameObject.Find("a1s");
		arrow2s = GameObject.Find("a2s");
		arrow3s = GameObject.Find("a3s");
		arrow4s = GameObject.Find("a4s");

		click = GameObject.Find("Click");
		clickL = GameObject.Find("ClickL");

		FreeButton = GameObject.Find("Free Roam Button");
		VerticalButton = GameObject.Find("Vertical Button");

		Handle = GameObject.Find("Panel Handle");
		HandleRef = GameObject.Find("PanelRef");
		XrayHead = GameObject.Find("PanelHandle");

		ColUp = GameObject.Find("Collimator Vertical Out");
		ColDown = GameObject.Find("Collimator Vertical In");
		ColLeft = GameObject.Find("Collimator Horizontal Out");
		ColRight = GameObject.Find("Collimator Horizontal In");
		Col1 = GameObject.Find("Col 1 (Near)");
		Col2 = GameObject.Find("Col 2 (Far)");
		Col3 = GameObject.Find("Col 3 (Left)");
		Col4 = GameObject.Find("Col 4 (Right)");

		mAsText.text = string.Concat(mAs.ToString(), " mAs");
		mAsText1.text = mAsText.text;
		kVText.text = string.Concat(kV.ToString(), " kV");
		kVText1.text = kVText.text;

		OriginalArrowColor = arrow1.GetComponent<Renderer>().material.color;
		OriginalHandleColor = Handle.GetComponent<Renderer>().material.color;
		OriginalUnpressed = FreeButton.GetComponent<Renderer>().material.color;
		OriginalColButton = ColUp.GetComponent<Renderer>().material.color;
	}





	void Update()
	{
		if (Handle)
		{
			// Light up the panel handle when ANY registered hand is near it,
			// rather than checking one hardcoded hand transform.
			float minDist = float.MaxValue;
			for (int i = 0; i < handTransforms.Length; i++)
			{
				if (handTransforms[i] == null) continue;
				float d = Vector3.Distance(handTransforms[i].position, Handle.transform.position);
				if (d < minDist) minDist = d;
			}

			Handle.GetComponent<Renderer>().material.color =
				(minDist <= 0.25f) ? Color.white : OriginalHandleColor;
		}
	}




	// ----- mAs -----

	public void PressMAsUp()
	{
		if (mAs < 70)
		{
			mAs = mAs + 1;
			mAsText.text = string.Concat(mAs.ToString(), " mAs");
			mAsText1.text = mAsText.text;
			arrow3.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
			arrow3s.GetComponent<Renderer>().material.color = Color.white;
			click.GetComponent<AudioSource>().Play();
		}
	}

	public void ReleaseMAsUp()
	{
		arrow3.GetComponent<Renderer>().material.color = OriginalArrowColor;
		arrow3s.GetComponent<Renderer>().material.color = Color.black;
	}


	public void PressMAsDown()
	{
		if (mAs >= 2)
		{
			mAs = mAs - 1;
			mAsText.text = string.Concat(mAs.ToString(), " mAs");
			mAsText1.text = mAsText.text;
			arrow1.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
			arrow1s.GetComponent<Renderer>().material.color = Color.white;
			clickL.GetComponent<AudioSource>().Play();
		}
	}

	public void ReleaseMAsDown()
	{
		arrow1.GetComponent<Renderer>().material.color = OriginalArrowColor;
		arrow1s.GetComponent<Renderer>().material.color = Color.black;
	}




	// ----- kV -----

	public void PressKVUp()
	{
		if (kV <= 90)
		{
			kV = kV + 1;
			kVText.text = string.Concat(kV.ToString(), " kV");
			kVText1.text = kVText.text;
			arrow4.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
			arrow4s.GetComponent<Renderer>().material.color = Color.white;
			click.GetComponent<AudioSource>().Play();
		}
	}


	public void ReleaseKVUp()
	{
		arrow4.GetComponent<Renderer>().material.color = OriginalArrowColor;
		arrow4s.GetComponent<Renderer>().material.color = Color.black;
	}



	public void PressKVDown()
	{
		if (kV >= 30)
		{
			kV = kV - 1;
			kVText.text = string.Concat(kV.ToString(), " kV");
			kVText1.text = kVText.text;
			arrow2.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
			arrow2s.GetComponent<Renderer>().material.color = Color.white;
			clickL.GetComponent<AudioSource>().Play();
		}
	}

	public void ReleaseKVDown()
	{
		arrow2.GetComponent<Renderer>().material.color = OriginalArrowColor;
		arrow2s.GetComponent<Renderer>().material.color = Color.black;
	}




	// ----- Free roam / fixed vertical toggle -----

	public void PressFreeToggle()
	{
		xrayHandleController.SetFreeMode();
		click.GetComponent<AudioSource>().Play();

		VerticalButton.GetComponent<Renderer>().material.color = OriginalUnpressed;
		FreeButton.GetComponent<Renderer>().material.color = Color.green;

	}


	public void PressVerticalToggle()
	{
		xrayHandleController.SetVerticalMode();
		click.GetComponent<AudioSource>().Play();

		VerticalButton.GetComponent<Renderer>().material.color = Color.green;
		FreeButton.GetComponent<Renderer>().material.color = OriginalUnpressed;
	}





	// --- Collimator width/height ---

	public void PressCollimatorVerticalOut()
	{
		Col1.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
		Col2.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
		ColUp.GetComponent<Renderer>().material.color = Color.grey;
	}


	public void ReleaseCollimatorVerticalOut()
	{
		ColUp.GetComponent<Renderer>().material.color = OriginalColButton;
	}



	public void PressCollimatorVerticalIn()
	{
		Col1.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
		Col2.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
		ColDown.GetComponent<Renderer>().material.color = Color.grey;
	}


	public void ReleaseCollimatorVerticalIn()
	{
		ColDown.GetComponent<Renderer>().material.color = OriginalColButton;
	}


	public void PressCollimatorHorizontalOut()
	{
		Col3.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
		Col4.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
		ColLeft.GetComponent<Renderer>().material.color = Color.grey;
	}


	public void ReleaseCollimatorHorizontalOut()
	{
		ColLeft.GetComponent<Renderer>().material.color = OriginalColButton;
	}


	public void PressCollimatorHorizontalIn()
	{
		Col3.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
		Col4.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
		ColRight.GetComponent<Renderer>().material.color = Color.grey;
	}


	public void ReleaseCollimatorHorizontalIn()
	{
		ColRight.GetComponent<Renderer>().material.color = OriginalColButton;
	}
}