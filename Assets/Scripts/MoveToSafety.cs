using UnityEngine;

public class MoveToSafety : MonoBehaviour
{

	Vector3 TransportIntPos;
	Vector3 TransportShieldPosReturn;

	public GameObject ShieldedPos;
	public GameObject ScanningPos;
	public GameObject TransportPos;
	public Transform TransportPosAnchor;

	public GameObject TransportShieldPos;
	public Transform TransportShieldPosAnchor;

	[SerializeField] Transform rightHandTransform; // the right controller under XR Origin

	void Start()
	{
		TransportIntPos = TransportPos.transform.position;
		TransportShieldPosReturn = TransportShieldPos.transform.position;
	}

	void Update()
	{

		float distTransport = Vector3.Distance(TransportPos.transform.position, TransportPosAnchor.position);
		float distTransportShield = Vector3.Distance(TransportShieldPos.transform.position, TransportShieldPosAnchor.position);
		float distHandTransport = Vector3.Distance(rightHandTransform.position, TransportPos.transform.position);
		float distHandTransportShield = Vector3.Distance(rightHandTransform.position, TransportShieldPos.transform.position);


		if (distTransport >= 0.2)
		{
			gameObject.transform.position = new Vector3 (2.35f, 1.4f, 0.094f);
			TransportPos.transform.position = TransportIntPos;

		}

		if (distTransportShield >= 0.2) {

			gameObject.transform.position = new Vector3 (-0.1f, 1.4f, 0.094f);
			TransportShieldPos.transform.position = TransportShieldPosReturn;

		}

		if (distHandTransport < 0.3f)
		{

			Renderer TransportPosRend = TransportPos.GetComponent<Renderer> ();
			float EmissionVal1 = ((1 - (distHandTransport / 0.3f)) * 0.4f);

			Color TransportPosEmissionCol = Color.white * Mathf.LinearToGammaSpace (EmissionVal1);
			TransportPosRend.material.SetColor ("_EmissionColor", TransportPosEmissionCol);
		}
		if (distHandTransportShield < 0.3f)
		{
			Renderer TransportShieldPosRend = TransportShieldPos.GetComponent<Renderer> ();
			float EmissionVal2 = ((1 -(distHandTransportShield / 0.3f)) * 0.4f) ;

			Color TransportShieldPosEmissionCol = Color.white * Mathf.LinearToGammaSpace (EmissionVal2);
			TransportShieldPosRend.material.SetColor ("_EmissionColor", TransportShieldPosEmissionCol);
		}
	}
}
