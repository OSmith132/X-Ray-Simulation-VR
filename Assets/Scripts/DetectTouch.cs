using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DetectTouch : MonoBehaviour {

	//this code is attahced to righthandachor in OVRCameraRig

	float Menudist;
	float HVLdist;
	float Assembledist;
	float Maindist;
	float ResetDist;
	float traydist;
	float time;
	float DetectorSize;
	float incrament = 0.075f;
	float ImageLeft;
	float ImageRight;
	float ImageLower;
	float ImageUpper;
	float ISLDist;
	float NoErrorDose;
	float NoErrorDAP;
	float Left1m;
	float Right1m;
	float Lower1m;
	float Upper1m;
	float FOV;
	float SFOV;
	float OfficeDist;
	float ReturnDist;
	float SubmitAnsDist;
	float kVError;
	float DAPCumlative;


	private bool onoff;
	private bool onoff2;
	bool sceneLoader = false;
	float distTable;
	public static int mAs = 10;
	public static int kV = 70;
	float DAP;
	float Dose;

	//used public as these floats are called in other scripts
	public float controlF;
	public float controlV;
	public float returntocent;
	public float primedown;
	public float primeup;



	Text mAsText;
	Text kVText;
	Text mAsText1;
	Text kVText1;
	public Text DAPText;
	public Text DoseText;
	public Text DoseonMeter;
	public Text LFarea;
	GameObject DoseonMeter2gameobject;
	Text DoseonMeter2;


	Vector3 radiographStartSize;
	Vector3 TVXrayStartSize;
	Vector3 DetectorPos;
	Vector3 SourcePos;

	//main game objects
	public GameObject HVL;
	GameObject PhantomMoveScript;
	GameObject PrimeButton;
	GameObject ScanButton;
	GameObject ScanReady;
	GameObject Yind;
	GameObject arrow1;
	GameObject arrow2;
	GameObject arrow3;
	GameObject arrow4;
	GameObject arrow1s;
	GameObject arrow2s;
	GameObject arrow3s;
	GameObject arrow4s;
	GameObject xraySource;
	GameObject LeftX;
	GameObject RightX;
	GameObject LowerY;
	GameObject UpperY;
	GameObject Detector;
	GameObject Handle;
	GameObject HandleRef;
	GameObject LeftHand;
	GameObject Source;
	GameObject Table;
	GameObject click;
	GameObject clickL;
	//GameObject Hum;
	GameObject primesound;
	GameObject VerticalTable;
	GameObject FreeButton;
	GameObject VerticalButton;
	GameObject XrayHead;
	GameObject HVLcube;
	GameObject HVLcubeBase;
	GameObject Assemblecube;
	GameObject AssemblecubeBase;
	GameObject MainCube;
	GameObject MainCubeBase;
	GameObject Tray;
	GameObject TrayAnchor;
	GameObject radiograph;
	GameObject ColUp;
	GameObject ColDown;
	GameObject ColLeft;
	GameObject ColRight;
	GameObject Col1;
	GameObject Col2;
	GameObject Col3;
	GameObject Col4;
	GameObject ISLcube;
	GameObject ISLcubeBase;
	GameObject LargeIonisationChamber;
	GameObject MenuCube;
	GameObject MenuCubeBase;
	GameObject TVXray;
	GameObject xrayCam;
	GameObject body;
	GameObject bodyHollow;
	GameObject Reset;
	GameObject ResetRef;
	GameObject Office;
	GameObject OfficeRef;
	GameObject OVRCAM;
	GameObject OfficeLight1;
	GameObject OfficeLight2;
	GameObject ReturnScreen;
	GameObject ReturnScreenRef;
	GameObject SubmitAnswers;
	GameObject SubmitAnswersRef;
	GameObject DeadPixel;

	//Colours
	Color OriginalArrowColor;
	Color OriginalHandleColor;
	Color OriginalUnpressed;
	Color ScanRed;
	Color PrimeYellow;
	Color OriginalTable;
	Color OriginalColButton;


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

	// end

	void Start ()
	{

		//scene manager
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

		//Gameobject Finding
		mAsText = GameObject.Find ("mAsText").GetComponent<Text> ();
		kVText = GameObject.Find ("kVText").GetComponent<Text> ();
		mAsText1 = GameObject.Find ("mAsText1").GetComponent<Text> ();
		kVText1 = GameObject.Find ("kVText1").GetComponent<Text> ();
		radiograph = GameObject.Find ("RadiographImg");
		xraySource = GameObject.Find ("Collimator Guide Light");
		LeftX = GameObject.Find ("Col 3 (Left)");
		RightX = GameObject.Find ("Col 4 (Right)");
		LowerY = GameObject.Find ("Col 1 (Near)");
		UpperY = GameObject.Find ("Col 2 (Far)");
		Detector = GameObject.Find ("Detector");
		arrow1 = GameObject.Find ("a1");
		arrow2 = GameObject.Find ("a2");
		arrow3 = GameObject.Find ("a3");
		arrow4 = GameObject.Find ("a4");
		arrow1s = GameObject.Find ("a1s");
		arrow2s = GameObject.Find ("a2s");
		arrow3s = GameObject.Find ("a3s");
		arrow4s = GameObject.Find ("a4s");
		FreeButton = GameObject.Find ("Free Roam Button");
		VerticalButton = GameObject.Find ("Vertical Button");
		XrayHead = GameObject.Find ("PanelHandle");
		mAsText.text = string.Concat (mAs.ToString (), " mAs");
		kVText.text = string.Concat (kV.ToString (), " kV");
		Handle = GameObject.Find ("PanelHandle");
		HandleRef = GameObject.Find ("PanelRef");
		click = GameObject.Find ("Click");
		clickL = GameObject.Find ("ClickL");
		//Hum = GameObject.Find ("XRAYON");
		primesound = GameObject.Find ("primeON");
		PrimeButton = GameObject.Find ("Prime");
		ScanButton = GameObject.Find ("Scan");
		ScanReady = GameObject.Find ("Scan Ready");
		VerticalTable = GameObject.Find ("Vertical Control");
		HVLcube = GameObject.Find ("TransportHVL");
		HVLcubeBase = GameObject.Find ("Transport HVL base");
		Assemblecube = GameObject.Find ("Transport build");
		AssemblecubeBase = GameObject.Find ("Transport build base");
		MainCube = GameObject.Find ("Transport main");
		MainCubeBase = GameObject.Find ("Transport main base");
		Tray = GameObject.Find ("Handle");
		TrayAnchor = GameObject.Find ("Tray Anchor");
		PhantomMoveScript = GameObject.Find ("PhantomBase");
		LeftHand = GameObject.Find ("LeftHandAnchor");
		Table = GameObject.Find ("Table Ref");
		Source = GameObject.Find ("Source Position");
		ColUp = GameObject.Find ("Collimator Vertical Out");
		ColDown = GameObject.Find ("Collimator Vertical In");
		ColLeft = GameObject.Find ("Collimator Horizontal Out");
		ColRight = GameObject.Find ("Collimator Horizontal In");
		Col1 = GameObject.Find ("Col 1 (Near)");
		Col2 = GameObject.Find ("Col 2 (Far)");
		Col3 = GameObject.Find ("Col 3 (Left)");
		Col4 = GameObject.Find ("Col 4 (Right)");
		ISLcube = GameObject.Find ("TransportISL");
		ISLcubeBase = GameObject.Find ("Transport ISL base");
		LargeIonisationChamber = GameObject.Find ("Detector Visible");
		MenuCube = GameObject.Find ("Menu");
		MenuCubeBase = GameObject.Find ("Menubase");
		Reset = GameObject.Find ("Reset");
		ResetRef = GameObject.Find ("Reset base");
		Office = GameObject.Find ("Office");
		OfficeRef = GameObject.Find ("OfficeRef");
		OVRCAM = GameObject.Find ("OVRCameraRig");
		OfficeLight1 = GameObject.Find ("Office Light");
		OfficeLight2 = GameObject.Find ("Office Light 2");
		ReturnScreen = GameObject.Find ("ReturnScreen");
		ReturnScreenRef = GameObject.Find ("ReturnScreenRef");
		SubmitAnswers = GameObject.Find ("SubmitAns");
		SubmitAnswersRef = GameObject.Find ("SubmitAnsRef");

		//Initialisation if specific scenes are called
		if (sceneName == "HEE Anatomy") {

			body = GameObject.Find ("Body");
			bodyHollow = GameObject.Find ("BodyHollow");

			TVXray = GameObject.Find ("TV Xray");
			xrayCam = GameObject.Find ("XrayCam");

			TVXrayStartSize = TVXray.transform.localScale;
			TVXray.GetComponent<Renderer> ().enabled = false;
			SFOV = xrayCam.GetComponent<Camera> ().fieldOfView;
			bodyHollow.SetActive (false);

			controlF = 1;
			controlV = 0;

//			mAsRef = 10;
//			kVRef = 60;
//			muCorrFactor = 0.6f;
//			thickness = 1.5f;
//			fluxFogLim = 0.13f;
//			fluxSatLim = 0.95f;
		}
		else
		{
			controlF = 0;
			controlV = 1;
		}

		if (sceneName == "HEE Light Field Alignment") {


			TVXray = GameObject.Find ("TV Xray");
			xrayCam = GameObject.Find ("XrayCam");
			DeadPixel = GameObject.Find ("DeadPixel");
			DeadPixel.gameObject.SetActive(false);

			TVXrayStartSize = TVXray.transform.localScale;
			TVXray.GetComponent<Renderer> ().enabled = false;
			SFOV = xrayCam.GetComponent<Camera> ().fieldOfView;

//			mAsRef = 20;
//			kVRef = 70;
//			muCorrFactor = 0.6f;
//			thickness = 1f;
//			fluxFogLim = 0.1f;
//			fluxSatLim = 0.9f;
		}

		if (sceneName == "Inverse Square Law Room")
		{
			DoseonMeter2gameobject = GameObject.Find ("Dose on meter 2");
			DoseonMeter2 = DoseonMeter2gameobject.gameObject.GetComponent<Text> ();

		}



		//Get Renderer
		OriginalArrowColor = arrow1.GetComponent<Renderer> ().material.color;
		OriginalHandleColor = Handle.GetComponent<Renderer> ().material.color;
		OriginalUnpressed = FreeButton.GetComponent<Renderer> ().material.color;
		ScanRed = ScanButton.GetComponent<Renderer> ().material.color;
		PrimeYellow = PrimeButton.GetComponent<Renderer> ().material.color;
		OriginalTable = VerticalTable.GetComponent<Renderer> ().material.color;
		OriginalColButton = ColUp.GetComponent<Renderer> ().material.color;


		//Set initial mAs and kV text on touch screen panels
		mAsText.text = string.Concat (mAs.ToString (), " mAs");
		mAsText1.text = mAsText.text;
		kVText.text = string.Concat (kV.ToString (), " kV");
		kVText1.text = kVText.text;

		//Functions
		//SetLightGuideMax ();

		//Checks
		returntocent = 0;
		OfficeLight1.GetComponent<Light> ().enabled = false;
		OfficeLight2.GetComponent<Light> ().enabled = false;

		//Other

		//DR Physics engine code parameter values
		kVRef = 60;
		mAsRef = 10; //This is the arbitrary mAs the original image was acquired with and can therefore be adjusted to relative to this value
		fluxRef = 1080; //no. of photons per mAs (arbitrary value from trial and error)
		fluxFogLim = 4320; //Fog limit when film is white (based on no. of photons produced at 4mAs)
		fluxSatLim = 47200; //Saturtion limit when flim is black with this number of photons

	}


	void Update () 
	{

		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1.0f) 
		{
			if (sceneName == "HEE Light Field Alignment")
			{
				DeadPixel.gameObject.SetActive(true);
			}

		}
			

		//Cube menu left of shielding, allows you to transport between scenes by pulling cubes
		Menudist = Vector3.Distance (MenuCube.transform.position, MenuCubeBase.transform.position);
		if (Menudist >= 0.1) {
			SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
			MenuCube.transform.position = MenuCubeBase.transform.position;
		}

		//Reset Scene
		ResetDist = Vector3.Distance (Reset.transform.position, ResetRef.transform.position);
		if (ResetDist >= 0.1) 
		{
			if (sceneName == "HEE Anatomy")
			{
				SceneManager.LoadScene ("HEE Anatomy", LoadSceneMode.Single);
				Reset.transform.position = ResetRef.transform.position;
				Reset.transform.rotation = ResetRef.transform.rotation;

			}
			if (sceneName == "HEE Light Field Alignment")
			{
				SceneManager.LoadScene ("HEE Light Field Alignment", LoadSceneMode.Single);
				Reset.transform.position = ResetRef.transform.position;
				Reset.transform.rotation = ResetRef.transform.rotation;

			}
			if (sceneName == "HVL Xray Room Oculus Touch")
			{
				SceneManager.LoadScene ("HVL Xray Room Oculus Touch", LoadSceneMode.Single);
				Reset.transform.position = ResetRef.transform.position;
				Reset.transform.rotation = ResetRef.transform.rotation;

			}

			if (sceneName == "Inverse Square Law Room")
			{
				SceneManager.LoadScene ("Inverse Square Law Room", LoadSceneMode.Single);
				Reset.transform.position = ResetRef.transform.position;
				Reset.transform.rotation = ResetRef.transform.rotation;

			}
		}

		//Transport into Office space
		OfficeDist = Vector3.Distance (Office.transform.position, OfficeRef.transform.position);

		if (OfficeDist >= 0.1) 
		{
			OfficeLight1.GetComponent<Light> ().enabled = true;
			OfficeLight2.GetComponent<Light> ().enabled = true;
			OVRCAM.transform.position = new Vector3 (5.35f, 1.4f, 0.094f);
			Office.transform.position = OfficeRef.transform.position;
			Office.transform.rotation = OfficeRef.transform.rotation;
		}

		ReturnDist = Vector3.Distance (ReturnScreen.transform.position, ReturnScreenRef.transform.position);

		if (ReturnDist >= 0.1) 
		{
			OVRCAM.transform.position = new Vector3 (2.35f, 1.4f, 0.094f);
			ReturnScreen.transform.position = ReturnScreenRef.transform.position;
			ReturnScreen.transform.rotation = ReturnScreenRef.transform.rotation;
		}

		SubmitAnsDist = Vector3.Distance (SubmitAnswers.transform.position, SubmitAnswersRef.transform.position);

		if (SubmitAnsDist >= 0.1) {
			SceneManager.LoadScene ("HEE Menu", LoadSceneMode.Single);
			SubmitAnswers.transform.position = SubmitAnswersRef.transform.position;
		}


		traydist = Vector3.Distance (Tray.transform.position, TrayAnchor.transform.position);
		//Pull tray out from table
		if (traydist <= 0.1) {
			Tray.transform.position = TrayAnchor.transform.position;
			Tray.transform.rotation = TrayAnchor.transform.rotation;
		}



		//Light up handle when either left or right hand is near
		float distHandle = Vector3.Distance (this.gameObject.transform.position, HandleRef.transform.position);
		float distHandleL = Vector3.Distance (LeftHand.transform.position, HandleRef.transform.position);

		if (distHandle <= 0.1 || distHandleL <= 0.1) {
			Handle.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			Handle.GetComponent<Renderer>().material.color = OriginalHandleColor;
		}


		//Boolians to Impose a delay between prime and scan, giving a double press feel
		if (primedown == 1)
		{

			ScanReady.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.yellow);

			if (Time.time >= time) 
			{
				ScanButton.GetComponent<Collider> ().enabled = true;
				ScanReady.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.green);
			} 

		}
		if (primeup == 1) 
		{
			ScanReady.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
			primeup = 0;
		}

		DetectorPos = Detector.transform.position;
		SourcePos = xraySource.transform.position;

		Left1m = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x/2.0f))) / (SourcePos.y - LeftX.transform.position.y));
		Right1m = (((RightX.transform.position.z - (RightX.transform.lossyScale.x/2.0f)) - SourcePos.z) / (SourcePos.y - RightX.transform.position.y));
		Lower1m = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x/2.0f)) - SourcePos.x) / (SourcePos.y - LowerY.transform.position.y));
		Upper1m = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x/2.0f))) / (SourcePos.y - UpperY.transform.position.y));

		//Calculate area of light field at 1m
		float horizDist = Right1m + Left1m;
		float vertDist = Lower1m + Upper1m;

		LFarea.text = string.Concat (horizDist.ToString ("F2"), "x", vertDist.ToString ("F2"), " m"); 
	}


	public void TakeScan()
	{

		//Calculate area of light field at 1m
		float horizD = Right1m + Left1m;
		float vertD = Lower1m + Upper1m;

		//		FOV = vertDist * horizDist * 100;
		//		float adjFOV = SFOV + FOV; 

		//Calculate the FOV angle for the camera to match the light field
		float hFOV = 2.0f *(Mathf.Atan(0.5f*horizD) * Mathf.Rad2Deg);
		float vFOV = 2.0f *(Mathf.Atan(0.5f*vertD) * Mathf.Rad2Deg);

		//		xrayCam.GetComponent<Camera> ().fieldOfView = adjFOV;
		xrayCam.GetComponent<Camera> ().aspect = horizD / vertD;
		xrayCam.GetComponent<Camera> ().fieldOfView = vFOV;

		//Adjust the dimensions of the plane to match the dimensions of the x-ray image to prevent distortion
		if (horizD > vertD)
		{
			float AspectRatio = TVXrayStartSize.z * (vertD/ horizD);
			TVXray.transform.localScale = new Vector3 (TVXrayStartSize.x, TVXrayStartSize.y, AspectRatio);
		} else {
			float AspectRatio = TVXrayStartSize.x * (horizD / vertD);
			TVXray.transform.localScale = new Vector3 (AspectRatio, TVXrayStartSize.y, TVXrayStartSize.z);
		}
			
	}

	public void CalculateImage()
	{
		//Calculate parameters that don't change here so they are not calcuated for every loop below
		//Calcualte incident photon flux based on mAs
		float mAsAdjusted = ((float)mAs * 2f) + 10f;
		float flux0 = fluxRef * mAsAdjusted;

		//Get image from xray camera
		RenderTexture CamRenderTex = xrayCam.GetComponent<Camera>().targetTexture;
		RenderTexture.active = CamRenderTex;
		Texture2D xrayCamTex2D = new Texture2D(1024,1024,TextureFormat.ARGB32,false);
		xrayCamTex2D.ReadPixels (new Rect (0, 0, CamRenderTex.width, CamRenderTex.height), 0, 0, false);
		xrayCamTex2D.Apply ();
		Texture2D OrigImg = xrayCamTex2D;

		Color[] OrigImgPixels = OrigImg.GetPixels (0, 0, OrigImg.width, OrigImg.height);

		Color[] AdjImg = new Color[OrigImgPixels.Length];
		//Calculate attenuation coefficient values based on JWF original code to be between 0 - 0.4
		for (int ii = 0; ii < OrigImgPixels.Length; ii++)
		{
			//Calculate the new pixel values
			//Adjust the attenuation coefficients based on the set kV
			float muRefVal = (1 - OrigImgPixels [ii].grayscale) * 0.6f;
			float mukVAdj = muRefVal * (Mathf.Pow (kVRef, 3) / Mathf.Pow (kV, 3));

			//Calculate no. of photons transmitted through object based on adjusted mu values
			float fluxTransmitted = flux0 * Mathf.Exp(-mukVAdj);
			float ImgIntensityAdj =  Mathf.Exp(-mukVAdj); // d=1

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
			float noise = Random.Range (1 - noisemultiplier, 1 + noisemultiplier);
			float ImgIntensityInvNoise = ImgIntensityAdjInv * noise;

			AdjImg [ii] = new Color (ImgIntensityInvNoise,ImgIntensityInvNoise,ImgIntensityInvNoise, 1.0f);

		}

		//Create new texture and populate with the adjusted grayscale values
		Texture2D AdjImgTex = new Texture2D(OrigImg.width,OrigImg.height);
		AdjImgTex.SetPixels (AdjImg);
		AdjImgTex.Apply (true);

		TVXray.GetComponent<MeshRenderer> ().material.mainTexture = AdjImgTex;
	}

//	public void CalculateImage()
//	{
//		//Calculate parameters that don't change here so they are not calcuated for every loop below
//		//Calcualte incident photon flux based on mAs
//		Debug.Log(mAs + "    " + mAsRef);
//
//		float flux0 = (float)mAs / (float)mAsRef;
//
//
//		Debug.Log ("flux0 = " + flux0);
//		//Get image from xray camera
//		RenderTexture CamRenderTex = xrayCam.GetComponent<Camera>().targetTexture;
//		RenderTexture.active = CamRenderTex;
//		Texture2D xrayCamTex2D = new Texture2D(1024,1024,TextureFormat.ARGB32,false);
//		xrayCamTex2D.ReadPixels (new Rect (0, 0, CamRenderTex.width, CamRenderTex.height), 0, 0, false);
//		xrayCamTex2D.Apply ();
//		Texture2D OrigImg = xrayCamTex2D;
//
//		Color[] OrigImgPixels = OrigImg.GetPixels (0, 0, OrigImg.width, OrigImg.height);
//
//		Color[] AdjImg = new Color[OrigImgPixels.Length];
//		//Calculate attenuation coefficient values based on JWF original code to be between 0 - 0.4
//		for (int ii = 0; ii < OrigImgPixels.Length; ii++)
//		{
//			//Calculate the new pixel values
//			//Adjust the attenuation coefficients based on the set kV
//			float muRefVal = -Mathf.Log((OrigImgPixels [ii].grayscale * muCorrFactor));
//
//			//The log can produce very large numbers when image intensity = 1, account for this by limiting mu to a maximum value
//			if (muRefVal > 3)
//			{
//				muRefVal = 3;
//			}
//
//			//Old calculation of mu values
//			//float muRefVal = (1 - OrigImgPixels [ii].grayscale) * muCorrFactor;
//
//			float mukVAdj = muRefVal * (Mathf.Pow ((float)kVRef, 3) / Mathf.Pow ((float)kV, 3));
//
//			//Calculate no. of photons transmitted through object based on adjusted mu values
//			float fluxTransmitted = flux0 * Mathf.Exp(-mukVAdj * thickness);
//			//float ImgIntensityAdj =  Mathf.Exp(-mukVAdj); // d=1
//
//			if (ii == 500) {
//				Debug.Log ("muRefVal = " + muRefVal +"  muAdj = " + mukVAdj + "  fluxTransmitted = " + fluxTransmitted);
//			}
//
//			//If the flux isn't above the fog threshold set flux to 0 (white pixel)
//			if (fluxTransmitted < fluxFogLim)
//			{
//				fluxTransmitted = 0.00000001f;
//			}
//
//			//If flux is above the saturation level set flux to max flux and pixel to black
//			if (fluxTransmitted > fluxSatLim) 
//			{
//				fluxTransmitted = fluxSatLim;
//			}
//
//			//METHOD 1
//			//Adjust for mAs
//			//			float ImgIntensitymAs = ((((mAsVal - mAsRef)*DetectorResponseGamma) + mAsRef)/mAsRef) * ImgIntensityAdj;
//
//			//METHOD 2
//			float ImgIntensitymAs = fluxTransmitted; // fluxSatLim;
//
//			//Invert scale so darker when more photons transmitted
//			float ImgIntensityAdjInv = 1 - ImgIntensitymAs;
//
//			//Add perlin noise to pixels
//			//Calculate the amount of noise based on the photon flux/image intensity. Maximum noise is 70% (+/-35%)jitter in values
//			float noisemultiplier = ImgIntensityAdjInv * 0.3f; //the texture from the camera is inverted by the overlay inversioon screen so this differs from the 2D app code
//			float noise = Random.Range (1 - noisemultiplier, 1 + noisemultiplier);
//			float ImgIntensityInvNoise = ImgIntensityAdjInv * noise;
//
//			AdjImg [ii] = new Color (ImgIntensityInvNoise,ImgIntensityInvNoise,ImgIntensityInvNoise, 1.0f);
//
//		}
//
//		//Create new texture and populate with the adjusted grayscale values
//		Texture2D AdjImgTex = new Texture2D(OrigImg.width,OrigImg.height);
//		AdjImgTex.SetPixels (AdjImg);
//		AdjImgTex.Apply (true);
//
//		TVXray.GetComponent<MeshRenderer> ().material.mainTexture = AdjImgTex;
//	}



	void OnTriggerEnter (Collider touch)
	{
		if (touch.gameObject.name == "mAs_up")
		{
			if (mAs <= 69) 
			{
				mAs = mAs + 1;
				mAsText.text = string.Concat (mAs.ToString (), " mAs");
				mAsText1.text = mAsText.text;
				arrow3.GetComponent<Renderer> ().material.color = new Color (1,1,1,1);
				arrow3s.GetComponent<Renderer> ().material.color = Color.white;
				click.GetComponent<AudioSource> ().Play ();
			}
		}
		if (touch.gameObject.name == "mAs_down")
		{
			if (mAs >= 2) 
			{
				mAs = mAs - 1;
				mAsText.text = string.Concat (mAs.ToString (), " mAs");
				mAsText1.text = mAsText.text;
				arrow1.GetComponent<Renderer> ().material.color = new Color (1,1,1,1);
				arrow1s.GetComponent<Renderer> ().material.color = Color.white;
				clickL.GetComponent<AudioSource> ().Play ();
			}
		}

		if (touch.gameObject.name == "kV_up") 
		{
			if (kV <= 90) 
			{
				kV = kV + 1;
				kVText.text = string.Concat (kV.ToString (), " kV");
				kVText1.text = kVText.text;
				arrow4.GetComponent<Renderer> ().material.color = new Color (1,1,1,1);
				arrow4s.GetComponent<Renderer> ().material.color = Color.white;
				click.GetComponent<AudioSource> ().Play ();
			}
		}
		if (touch.gameObject.name == "kV_down") 
		{
			if (kV >= 30) 
			{
				kV = kV - 1;
				kVText.text = string.Concat (kV.ToString (), " kV");
				kVText1.text = kVText.text;
				arrow2.GetComponent<Renderer> ().material.color = new Color (1,1,1,1);
				arrow2s.GetComponent<Renderer> ().material.color = Color.white;
				clickL.GetComponent<AudioSource> ().Play ();
			}
		}

		if (touch.gameObject.name == "freetog") {
			controlF = 1;
			controlV = 0;
			click.GetComponent<AudioSource> ().Play ();

		}

		if (touch.gameObject.name == "verticaltog") {
			controlV = 1;
			controlF = 0;
			click.GetComponent<AudioSource> ().Play ();
		}

		if (touch.gameObject.name == "Scan") {
			ScanButton.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.green);
			click.GetComponent<AudioSource> ().Play ();
			primeup = 1;
			primedown = 0; 
	
			//Hum.GetComponent<AudioSource> ().Play ();

			if (sceneName == "HEE Anatomy" || sceneName == "HEE Light Field Alignment") 
			{
				TVXray.GetComponent<Renderer> ().enabled = true;
				xrayCam.GetComponent<Camera> ().enabled = true;
				CalculateImage ();
				CalculateDAP ();
				
			}

			if (sceneName == "HVL Xray Room Oculus Touch")
			{
				HVL.GetComponent<HVLanchors> ().CalcOutput ();
				CalculateDAP ();
			}

			if (sceneName == "Inverse Square Law Room")
			{
				CalculateDAP ();
			}


		} 


		if (touch.gameObject.name == "Prime") 
		{ 
			primedown = 1;
			PrimeButton.GetComponent<Renderer> ().material.color = new Color32( 254 , 161 , 0, 1 );
			click.GetComponent<AudioSource> ().Play ();
			primesound.GetComponent<AudioSource> ().Play ();
			ScanButton.GetComponent<Collider> ().enabled = false;
			time = Time.time + 0.8f;
			ScanButton.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);
	
			if (sceneName == "HEE Anatomy" || sceneName == "HEE Light Field Alignment") {
				TVXray.GetComponent<Renderer> ().enabled = false;
				TakeScan ();
			}


		}

		if (touch.gameObject.name == "Vertical Control") 
		{
			VerticalTable.GetComponent<Renderer> ().material.color = Color.blue;
		}

		if (touch.gameObject.name == "fixedpos") 
		{
			returntocent = 1;
		}

		if (touch.gameObject.name == "Collimator Vertical Out") 
		{
			Col1.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
			Col2.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
			ColUp.GetComponent<Renderer> ().material.color = Color.blue;

		}

		if (touch.gameObject.name == "Collimator Vertical In") 
		{
			Col1.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
			Col2.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
			ColDown.GetComponent<Renderer> ().material.color = Color.blue;
		}

		if (touch.gameObject.name == "Collimator Horizontal Out") 
		{
			Col3.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
			Col4.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
			ColLeft.GetComponent<Renderer> ().material.color = Color.blue;
		}

		if (touch.gameObject.name == "Collimator Horizontal In") 
		{
			Col3.transform.Translate(-incrament * Time.deltaTime, 0f, 0f);
			Col4.transform.Translate(incrament * Time.deltaTime, 0f, 0f);
			ColRight.GetComponent<Renderer> ().material.color = Color.blue;
		}

		if (touch.gameObject.name == "Transport") 
		{
			if (sceneName == "HEE Anatomy")
			{
				body.SetActive (true);
				bodyHollow.SetActive (false);
			}
		}

		if (touch.gameObject.name == "TransportReturn") 
		{
			if (sceneName == "HEE Anatomy")
			{
				body.SetActive (false);
				bodyHollow.SetActive (true);
				//TVXray.GetComponent<MeshRenderer> ().enabled = false;
			}
				
		}

	}

	void OnTriggerExit (Collider notouch)
	{
		if (notouch.gameObject.name == "mAs_up")
		{
			arrow3.GetComponent<Renderer> ().material.color = OriginalArrowColor;
			arrow3s.GetComponent<Renderer> ().material.color = Color.black;

		}
		if (notouch.gameObject.name == "mAs_down")
		{
			arrow1.GetComponent<Renderer> ().material.color = OriginalArrowColor;
			arrow1s.GetComponent<Renderer> ().material.color = Color.black;
		}

		if (notouch.gameObject.name == "kV_up") 
		{
			arrow4.GetComponent<Renderer> ().material.color = OriginalArrowColor;
			arrow4s.GetComponent<Renderer> ().material.color = Color.black;
		}
		if (notouch.gameObject.name == "kV_down") 
		{
			arrow2.GetComponent<Renderer> ().material.color = OriginalArrowColor;
			arrow2s.GetComponent<Renderer> ().material.color = Color.black;
		}
			
		if (notouch.gameObject.name == "Prime") 
		{
			PrimeButton.GetComponent<Renderer> ().material.color = PrimeYellow;
			primedown = 0;
		}

		if (notouch.gameObject.name == "Scan") 
		{
			ScanButton.GetComponent<Renderer> ().material.color = ScanRed;
		}

		if (notouch.gameObject.name == "Collimator Vertical Out") 
		{
			ColUp.GetComponent<Renderer> ().material.color = OriginalColButton;

		}

		if (notouch.gameObject.name == "Collimator Vertical In") 
		{
			ColDown.GetComponent<Renderer> ().material.color = OriginalColButton;
		}

		if (notouch.gameObject.name == "Collimator Horizontal Out") 
		{
			ColLeft.GetComponent<Renderer> ().material.color = OriginalColButton;
		}

		if (notouch.gameObject.name == "Collimator Horizontal In") 
		{
			ColRight.GetComponent<Renderer> ().material.color = OriginalColButton;
		}

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
		float Left1m = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x/2.0f))) / (SourcePos.y - LeftX.transform.position.y));
		float Right1m = (((RightX.transform.position.z - (RightX.transform.lossyScale.x/2.0f)) - SourcePos.z) / (SourcePos.y - RightX.transform.position.y));
		float Lower1m = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x/2.0f)) - SourcePos.x) / (SourcePos.y - LowerY.transform.position.y));
		float Upper1m = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x/2.0f))) / (SourcePos.y - UpperY.transform.position.y));

		//Calculate area of light field at 1m
		float horizDist = Right1m + Left1m;
		float vertDist = Lower1m + Upper1m;
		float areaAt1m = horizDist * vertDist;
		float areaAt1m_cm = areaAt1m * 100f * 100f;

		//Calculate DAP in Gycm^2
		//DAP = 0.0000791f * kV * kV * mAs * areaAt1m; //The correction factor 0.0000791 has a built in conversion from uGym^2 to Gycm^2 (i.e. the decimal was shifted two to the left, multiplied by 10^-2)

		// Calculate where the image is cut off by the light guides on the table surface/detector height
		ImageLeft = ((SourcePos.z - (LeftX.transform.position.z + (LeftX.transform.lossyScale.x/2.0f))) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - LeftX.transform.position.y);
		ImageRight = (((RightX.transform.position.z - (RightX.transform.lossyScale.x/2.0f)) - SourcePos.z) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - RightX.transform.position.y);
		ImageLower = (((LowerY.transform.position.x - (LowerY.transform.lossyScale.x/2.0f)) - SourcePos.x) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - LowerY.transform.position.y);
		ImageUpper = ((SourcePos.x - (UpperY.transform.position.x + (UpperY.transform.lossyScale.x/2.0f))) * (SourcePos.y - DetectorPos.y)) / (SourcePos.y - UpperY.transform.position.y);

		float horizDist2 = ImageRight + ImageLeft;
		float vertDist2 = ImageUpper + ImageLower;
		float area = horizDist2 * vertDist2;
		distTable = Vector3.Distance (SourcePos, DetectorPos);
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
			//Dose = ((0.00791f * kV * kV * mAs_adj4txt * (area/0.04f)) / (distTable * distTable));

		} else 
		{
			horizMin = ImageLeft;
			horizMax = ImageRight;
			//Dose = 0.00791f * kV * kV * mAs_adj4txt;
		}

		if (vertDist2 > LargeIonisationChamber.transform.lossyScale.x)
		{
			vertMin = LargeIonisationChamber.transform.lossyScale.x / 2;
			vertMax = LargeIonisationChamber.transform.lossyScale.x / 2;
		} else 
		{
			vertMin = ImageLower;
			vertMax = ImageUpper;
		}

		float AreaOverlap = (horizMax + horizMin) * (vertMax + vertMin);

		//NoErrorDose =  0.0000791f * kV * kV * mAs * (AreaOverlap/(distTable * distTable));
		NoErrorDose =  1.7f * Mathf.Pow(10,-4) * kV * kV * mAs * (AreaOverlap/(distTable * distTable));
		Dose = Random.Range (0.975f, 1.025f) * NoErrorDose + (NoErrorDose * 0.04f); //Added a small 4% offset in the DAP so not enough to cause fault but still enough to confuse people

		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1.0f) 
		{
			if (sceneName == "HVL Xray Room Oculus Touch") {
				kVError = 1.5f;
			} 
			else 
			{
				kVError = 1;
			}

		}
		else 
		{
			kVError = 1;
		}
				

		NoErrorDAP = 1.7f * Mathf.Pow (10, -8) * (kV * kVError) * (kV * kVError) * mAs * areaAt1m_cm;
		DAP = Random.Range (0.965f, 1.035f) * NoErrorDAP;

		DAPText.text = string.Concat ("DAP: ",DAP.ToString ("F2"), " Gycm\xB2" );

		if (PlayerPrefs.GetFloat ("FaultsActivated") == 1.0f) 
		{
			if (sceneName == "Inverse Square Law Room")
			{
				DAPCumlative += DAP;
				DAPText.text = string.Concat ("DAP: ",DAPCumlative.ToString ("F2"), " Gycm\xB2" );

			} 
		}

		DoseText.text = string.Concat ("Dose: ",Dose.ToString ("F2"), " Gycm\xB2");
		DoseonMeter.text = string.Concat (Dose.ToString ("F2"), " Gycm\xB2");

		if (sceneName == "Inverse Square Law Room")
		{
			DoseonMeter2.text = string.Concat (Dose.ToString ("F2"), " Gycm\xB2");

		}

	}


}
