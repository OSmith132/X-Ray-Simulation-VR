using UnityEngine;

public class XRayHandleController : MonoBehaviour
{
	[SerializeField] ConfigurableJoint configurableJoint;
	[SerializeField] Transform targetPosVert; // world-space reference for returnToCentre
	[SerializeField] float speed = 1.5f;

	Vector3 targetPosVertLocal;

	float returnToCentre = 0;





	public void SetFreeMode()
	{
		configurableJoint.xMotion = ConfigurableJointMotion.Free;
		configurableJoint.zMotion = ConfigurableJointMotion.Free;
		configurableJoint.yMotion = ConfigurableJointMotion.Free;
	}

	public void SetVerticalMode()
	{

		// anchor the joint to the handle's current position
		Vector3 worldAnchor = configurableJoint.transform.TransformPoint(configurableJoint.anchor);

		if (configurableJoint.connectedBody != null)
		{
			configurableJoint.connectedAnchor =
				configurableJoint.connectedBody.transform.InverseTransformPoint(worldAnchor);
		}
		else
		{
			// No connected body: connectedAnchor is interpreted in world space.
			configurableJoint.connectedAnchor = worldAnchor;
		}


		configurableJoint.xMotion = ConfigurableJointMotion.Locked;
		configurableJoint.zMotion = ConfigurableJointMotion.Locked;
		configurableJoint.yMotion = ConfigurableJointMotion.Free;
	}





	// Uncomment if need to glide to centre for anything. Currently unused:  ================================

	//void Update() 
	//{

	//if (returnToCentre == 1)
	//{
	//	returnToCentre(ref returnToCentre);
	//}

	//}

	//void Awake()
	//{
	//targetPosVertLocal = transform.InverseTransformPoint(targetPosVert.position);
	//}


	//public void returnToCentre(ref float returnToCentre)
	//{

	//	configurableJoint.targetPosition = Vector3.MoveTowards(
	//		configurableJoint.targetPosition,
	//		targetPosVertLocal,
	//		speed * Time.deltaTime);

	//	if (Vector3.Distance(configurableJoint.targetPosition, targetPosVertLocal) < 0.001f)
	//	{
	//		returnToCentre = 0;
	//	}
	//}


}