using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



/// <summary>
/// Contains all the logic and processing for taking a scan.
/// 
/// Also currently contains legacy code (comments) from `DetectTouch.cs` that can be removed.
/// </summary>
public class XRayScanner : MonoBehaviour
{

	float DetectorSize;
	float ImageLeft;
	float ImageRight;
	float ImageLower;
	float ImageUpper;
	float NoErrorDose;
	float NoErrorDAP;
	float Left1m;
	float Right1m;
	float Lower1m;
	float Upper1m;

	float kVError;
	float DAPCumlative;

	float distTable;

	//XRayControlPanel.mAs;
	//XRayControlPanel.kV;
	
	float DAP;
	float Dose;

	public TMP_Text DAPText;
	public TMP_Text DoseText;
	public TMP_Text DoseOnHVLMeter;
	public TMP_Text LFarea;
	GameObject DoseonMeter2gameobject;
	TMP_Text DoseOnHVLMeter2;

	Vector3 radiographStartSize;
	Vector3 TVXrayStartSize;
	Vector3 DetectorPos;
	Vector3 SourcePos;

	public GameObject HVL;
	GameObject xraySource;
	GameObject LeftX;
	GameObject RightX;
	GameObject LowerY;
	GameObject UpperY;
	GameObject Detector;
	GameObject radiograph;
	GameObject LargeIonisationChamber;

	GameObject TVXray;
	GameObject xrayCam;
	GameObject body;

	Scene currentScene;
	string sceneName;


	//DR x-ray image production parameters
	int kVRef;
	int mAsRef;
	float muCorrFactor;
	float thickness;
	float fluxRef;
	float fluxFogLim;
	float fluxSatLim;





	// Primes the scanner to take a scan
	public void Prime()
	{
		if (sceneName == "HEE Anatomy" || sceneName == "HEE Light Field Alignment")
		{
			TVXray.GetComponent<Renderer>().enabled = false;
			TakeScan();
		}

	}



	// Takes a scan
	public void Scan()
	{

		if (sceneName == "HEE Anatomy" || sceneName == "HEE Light Field Alignment")
		{
			TVXray.GetComponent<Renderer>().enabled = true;
			xrayCam.GetComponent<Camera>().enabled = true;
			CalculateImage();
			CalculateDAP();
		}


		if (sceneName == "HVL Xray Room Oculus Touch")
		{
			CalculateDAP();
		}


		if (sceneName == "Inverse Square Law Room")
		{
			CalculateDAP();
		}

	}






	void Start()
	{

		//scene manager
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;

		radiograph = GameObject.Find("RadiographImg");
		xraySource = GameObject.Find("Collimator Guide Light");
		LeftX = GameObject.Find("Col 3 (Left)");
		RightX = GameObject.Find("Col 4 (Right)");
		LowerY = GameObject.Find("Col 1 (Near)");
		UpperY = GameObject.Find("Col 2 (Far)");
		Detector = GameObject.Find("Detector");

		LargeIonisationChamber = GameObject.Find("Detector Visible");

		//Initialisation if specific scenes are called
		if (sceneName == "HEE Anatomy")
		{

			body = GameObject.Find("Body");
			//bodyHollow = GameObject.Find("BodyHollow");

			TVXray = GameObject.Find("TV Xray");
			xrayCam = GameObject.Find("XrayCam");

			TVXrayStartSize = TVXray.transform.localScale;
			TVXray.GetComponent<Renderer>().enabled = false;
			//bodyHollow.SetActive(false);

		}


		if (sceneName == "HEE Light Field Alignment")
		{


			TVXray = GameObject.Find("TV Xray");
			xrayCam = GameObject.Find("XrayCam");

			TVXrayStartSize = TVXray.transform.localScale;
			TVXray.GetComponent<Renderer>().enabled = false;
		}

		if (sceneName == "Inverse Square Law Room")
		{
			DoseonMeter2gameobject = GameObject.Find("Dose on meter 2");
			DoseOnHVLMeter2 = DoseonMeter2gameobject.gameObject.GetComponent<TMP_Text>();

		}



		//Other

		//DR Physics engine code parameter values
		kVRef = 60;
		mAsRef = 10; //This is the arbitrary mAs the original image was acquired with and can therefore be adjusted to relative to this value
		fluxRef = 1080; //no. of photons per mAs (arbitrary value from trial and error)
		fluxFogLim = 4320; //Fog limit when film is white (based on no. of photons produced at 4mAs)
		fluxSatLim = 47200; //Saturtion limit when flim is black with this number of photons

	}


	void Update()
	{




		// Ensure source is defined in Unity Engine
		if (xraySource == null)
		{
			return;
		}


		SourcePos = xraySource.transform.position;

		Left1m = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x / 2.0f))) / (SourcePos.y - LeftX.transform.position.y));
		Right1m = (((RightX.transform.position.z - (RightX.transform.lossyScale.x / 2.0f)) - SourcePos.z) / (SourcePos.y - RightX.transform.position.y));
		Lower1m = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x / 2.0f)) - SourcePos.x) / (SourcePos.y - LowerY.transform.position.y));
		Upper1m = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x / 2.0f))) / (SourcePos.y - UpperY.transform.position.y));

		//Calculate area of light field at 1m
		float horizDist = Right1m + Left1m;
		float vertDist = Lower1m + Upper1m;

		LFarea.text = string.Concat(horizDist.ToString("F2"), "x", vertDist.ToString("F2"), " m");
	}



	public void TakeScan()
	{

		//Calculate area of light field at 1m
		float horizD = Right1m + Left1m;
		float vertD = Lower1m + Upper1m;


		//Calculate the FOV angle for the camera to match the light field
		float hFOV = 2.0f * (Mathf.Atan(0.5f * horizD) * Mathf.Rad2Deg);
		float vFOV = 2.0f * (Mathf.Atan(0.5f * vertD) * Mathf.Rad2Deg);

		//		xrayCam.GetComponent<Camera> ().fieldOfView = adjFOV;
		xrayCam.GetComponent<Camera>().aspect = horizD / vertD;
		xrayCam.GetComponent<Camera>().fieldOfView = vFOV;

		//Adjust the dimensions of the plane to match the dimensions of the x-ray image to prevent distortion
		if (horizD > vertD)
		{
			float AspectRatio = TVXrayStartSize.z * (vertD / horizD);
			TVXray.transform.localScale = new Vector3(TVXrayStartSize.x, TVXrayStartSize.y, AspectRatio);
		}
		else
		{
			float AspectRatio = TVXrayStartSize.x * (horizD / vertD);
			TVXray.transform.localScale = new Vector3(AspectRatio, TVXrayStartSize.y, TVXrayStartSize.z);
		}

	}



	public void CalculateImage()
	{
		//Calculate parameters that don't change here so they are not calcuated for every loop below
		//Calcualte incident photon flux based on mAs
		float mAsAdjusted = ((float)XRayControlPanel.mAs * 2f) + 10f;
		float flux0 = fluxRef * mAsAdjusted;

		//Get image from xray camera
		RenderTexture CamRenderTex = xrayCam.GetComponent<Camera>().targetTexture;
		RenderTexture.active = CamRenderTex;
		Texture2D xrayCamTex2D = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
		xrayCamTex2D.ReadPixels(new Rect(0, 0, CamRenderTex.width, CamRenderTex.height), 0, 0, false);
		xrayCamTex2D.Apply();
		Texture2D OrigImg = xrayCamTex2D;

		Color[] OrigImgPixels = OrigImg.GetPixels(0, 0, OrigImg.width, OrigImg.height);

		Color[] AdjImg = new Color[OrigImgPixels.Length];
		//Calculate attenuation coefficient values based on JWF original code to be between 0 - 0.4
		for (int ii = 0; ii < OrigImgPixels.Length; ii++)
		{
			//Calculate the new pixel values
			//Adjust the attenuation coefficients based on the set kV
			float muRefVal = (1 - OrigImgPixels[ii].grayscale) * 0.6f;
			float mukVAdj = muRefVal * (Mathf.Pow(kVRef, 3) / Mathf.Pow(XRayControlPanel.kV, 3));

			//Calculate no. of photons transmitted through object based on adjusted mu values
			float fluxTransmitted = flux0 * Mathf.Exp(-mukVAdj);
			float ImgIntensityAdj = Mathf.Exp(-mukVAdj); // d=1

			//If the flux isn't above the fog threshold set flux to 0 (white pixel)
			if (fluxTransmitted < fluxFogLim)
			{
				fluxTransmitted = 0.00000001f;
			}

			//If flux is above the saturation level set flux to max flux and pixel to black
			if (fluxTransmitted > fluxSatLim)
			{
				fluxTransmitted = fluxSatLim;
			}


			//METHOD 1
			//Adjust for mAs
			//			float ImgIntensitymAs = ((((mAsVal - mAsRef)*DetectorResponseGamma) + mAsRef)/mAsRef) * ImgIntensityAdj;

			//METHOD 2
			float ImgIntensitymAs = fluxTransmitted / fluxSatLim;

			//Invert scale so darker when more photons transmitted
			float ImgIntensityAdjInv = 1 - ImgIntensitymAs;

			//Add perlin noise to pixels
			//Calculate the amount of noise based on the photon flux/image intensity. Maximum noise is 70% (+/-35%)jitter in values
			float noisemultiplier = ImgIntensityAdj * 0.2f; //the texture from the camera is inverted by the overlay inversioon screen so this differs from the 2D app code
			float noise = Random.Range(1 - noisemultiplier, 1 + noisemultiplier);
			float ImgIntensityInvNoise = ImgIntensityAdjInv * noise;

			AdjImg[ii] = new Color(ImgIntensityInvNoise, ImgIntensityInvNoise, ImgIntensityInvNoise, 1.0f);

		}

		//Create new texture and populate with the adjusted grayscale values
		Texture2D AdjImgTex = new Texture2D(OrigImg.width, OrigImg.height);
		AdjImgTex.SetPixels(AdjImg);
		AdjImgTex.Apply(true);

		TVXray.GetComponent<MeshRenderer>().material.mainTexture = AdjImgTex;
	}





	public void SetLightGuideMax()
	{
		// Find position of the light guides so that the light field covers the whole detector
		// the calculation of detector size assumes the detector is square and in the centre of the x-ray field
		// Also assumes the scale of the original size of the cubes (i.e. light plates and detector) is 1 = 1m, this wouldn't necessarily be true for imported 3DSMax objects
		// There is a small error that needs fixing where the centre of the detector is used to calculate distances when really it should be it's surface...
		// I have fixed this by making the detector extremely thin.
		// JAS: USE THIS CODE TO SET AND LIMIT THE LIGHT GUIDE POSITION SO IT NEVER GOES BEYOND THE DETECTOR EDGE


		DetectorSize = Detector.transform.lossyScale.x;
		radiographStartSize = radiograph.transform.localScale;
		DetectorPos = Detector.transform.position;
		SourcePos = xraySource.transform.position;

		//JAS: THERE'S A SMALL SYSTEMATIC DISAGREEMENT BETWEEN THE LIGHT FIELD CUT OFF AND THE WHERE THE IMAGE IS CROPPED - I WILL FIX THIS LATER, IT JUST ABOUT WORKS WELL ENOUGH FOR NOW
		float HorizMaxDist = ((SourcePos.y - RightX.transform.position.y) / (SourcePos.y - DetectorPos.y)) * (DetectorSize / 2.0f);
		float VertMaxDist = (((SourcePos.y - LowerY.transform.position.y) / (SourcePos.y - DetectorPos.y)) * (DetectorSize / 2.0f));
		//		float MaxLeftCutoffPos = SourcePos.z - HorizMaxDist - (0.04298109f / 2.0f);
		//		float MaxRightCutoffPos = SourcePos.z + HorizMaxDist + (0.0414169f / 2.0f);
		//		float MaxLowerCutoffPos = SourcePos.x + VertMaxDist + (0.04977055f / 2.0f);
		//		float MaxUpperCutoffPos = SourcePos.x - VertMaxDist - (0.04126761f / 2.0f);
		float MaxLeftCutoffPos = SourcePos.z - HorizMaxDist - (LeftX.transform.lossyScale.x / 2.0f);
		float MaxRightCutoffPos = SourcePos.z + HorizMaxDist + (RightX.transform.lossyScale.x / 2.0f);
		float MaxLowerCutoffPos = SourcePos.x + HorizMaxDist + (LowerY.transform.lossyScale.x / 2.0f);
		float MaxUpperCutoffPos = SourcePos.x - HorizMaxDist - (UpperY.transform.lossyScale.x / 2.0f);


		LeftX.transform.position = new Vector3(LeftX.transform.position.x, LeftX.transform.position.y, MaxLeftCutoffPos);
		RightX.transform.position = new Vector3(RightX.transform.position.x, RightX.transform.position.y, MaxRightCutoffPos);
		LowerY.transform.position = new Vector3(MaxLowerCutoffPos, LowerY.transform.position.y, LowerY.transform.position.z);
		UpperY.transform.position = new Vector3(MaxUpperCutoffPos, UpperY.transform.position.y, UpperY.transform.position.z);
	}



	void CalculateDAP()
	{
		//Calculate light field extension in each direction at 1m from source
		float Left1m = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x / 2.0f))) / (SourcePos.y - LeftX.transform.position.y));
		float Right1m = (((RightX.transform.position.z - (RightX.transform.lossyScale.x / 2.0f)) - SourcePos.z) / (SourcePos.y - RightX.transform.position.y));
		float Lower1m = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x / 2.0f)) - SourcePos.x) / (SourcePos.y - LowerY.transform.position.y));
		float Upper1m = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x / 2.0f))) / (SourcePos.y - UpperY.transform.position.y));

		//Calculate area of light field at 1m
		float horizDist = Right1m + Left1m;
		float vertDist = Lower1m + Upper1m;
		float areaAt1m = horizDist * vertDist;
		float areaAt1m_cm = areaAt1m * 100f * 100f;

		//Calculate DAP in Gycm^2
		//DAP = 0.0000791f * XRayControlPanel.kV * XRayControlPanel.kV * XRayControlPanel.mAs * areaAt1m; //The correction factor 0.0000791 has a built in conversion from uGym^2 to Gycm^2 (i.e. the decimal was shifted two to the left, multiplied by 10^-2)

		// Calculate where the image is cut off by the light guides on the table surface/detector height
		ImageLeft = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x / 2.0f))) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - LeftX.transform.position.y);
		ImageRight = (((RightX.transform.position.z - (RightX.transform.lossyScale.x / 2.0f)) - SourcePos.z) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - RightX.transform.position.y);
		ImageLower = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x / 2.0f)) - SourcePos.x) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - LowerY.transform.position.y);
		ImageUpper = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x / 2.0f))) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - UpperY.transform.position.y);

		float horizDist2 = ImageRight + ImageLeft;
		float vertDist2 = ImageUpper + ImageLower;
		float area = horizDist2 * vertDist2;
		distTable = Vector3.Distance(SourcePos, DetectorPos);
		//Debug.Log (area + " h " + horizDist2 + " v " + vertDist2);
		//Debug.Log ("ImgLower = " + ImageLower + " ImageUpper = " + ImageUpper);

		//Calculate the area of light field overlapping the detector
		float horizMin;
		float horizMax;
		float vertMin;
		float vertMax;

		if (horizDist2 > LargeIonisationChamber.transform.lossyScale.z)
		{
			horizMin = LargeIonisationChamber.transform.lossyScale.z / 2;
			horizMax = LargeIonisationChamber.transform.lossyScale.z / 2;
			//area = 0.04f;
			//Dose = ((0.00791f * XRayControlPanel.kV * XRayControlPanel.kV * mAs_adj4txt * (area/0.04f)) / (distTable * distTable));

		}
		else
		{
			horizMin = ImageLeft;
			horizMax = ImageRight;
			//Dose = 0.00791f * XRayControlPanel.kV * XRayControlPanel.kV * mAs_adj4txt;
		}

		if (vertDist2 > LargeIonisationChamber.transform.lossyScale.x)
		{
			vertMin = LargeIonisationChamber.transform.lossyScale.x / 2;
			vertMax = LargeIonisationChamber.transform.lossyScale.x / 2;
		}
		else
		{
			vertMin = ImageLower;
			vertMax = ImageUpper;
		}

		float AreaOverlap = (horizMax + horizMin) * (vertMax + vertMin);













		if (sceneName == "HVL Xray Room Oculus Touch")
		{
			kVError = FaultsManager.FaultsActivated ? 1.5f : 1f;
			HVL.GetComponent<HVLManager>().CalculateDose();
		}
		else
		{
			kVError = 1;
			NoErrorDose = 1.7f * Mathf.Pow(10, -4) * XRayControlPanel.kV * XRayControlPanel.kV * XRayControlPanel.mAs * (AreaOverlap / (distTable * distTable));
			Dose = Random.Range(0.975f, 1.025f) * NoErrorDose + (NoErrorDose * 0.04f);

			DoseText.text = string.Concat("Dose: ", Dose.ToString("F2"), " Gycm\xB2");
			DoseOnHVLMeter.text = string.Concat(Dose.ToString("F2"), " Gycm\xB2");
		}




		NoErrorDAP = 1.7f * Mathf.Pow(10, -8) * (XRayControlPanel.kV * kVError) * (XRayControlPanel.kV * kVError) * XRayControlPanel.mAs * areaAt1m_cm;
		DAP = Random.Range(0.965f, 1.035f) * NoErrorDAP;

		DAPText.text = string.Concat("DAP: ", DAP.ToString("F2"), " Gycm\xB2");

		











		if (sceneName == "Inverse Square Law Room")
		{


			if (FaultsManager.FaultsActivated)
			{
				DAPCumlative += DAP;
				DAPText.text = string.Concat("DAP: ", DAPCumlative.ToString("F2"), " Gycm\xB2");
			}


			DoseOnHVLMeter2.text = string.Concat(Dose.ToString("F2"), " Gycm\xB2");

		}





	}
}