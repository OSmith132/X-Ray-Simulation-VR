using UnityEngine;

/// <summary>
/// Light up the panel handle when a registered hand is near it.
/// </summary>
public class HandleGlow : MonoBehaviour
{
	[Header("Hands (for table handle proximity highlight)")]

	[SerializeField, Tooltip("Drag both the left and right hand transforms in here.")] Transform[] handTransforms;




	GameObject Handle;
	Color OriginalHandleColor;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

		Handle = GameObject.Find("Vertical Control");
		OriginalHandleColor = Handle.GetComponent<Renderer>().material.color;
	}


	void Update()
	{

		if (Handle)
		{


			foreach (Transform hand in handTransforms) {


				if (Vector3.Distance(hand.position, Handle.transform.position) <= 0.25)
				{
					Handle.GetComponent<Renderer>().material.color = Color.blue;
					return; // Return to not override with later hand in list
				}
				else
				{
					Handle.GetComponent<Renderer>().material.color = OriginalHandleColor;
				}
			}




		}
	}

}
