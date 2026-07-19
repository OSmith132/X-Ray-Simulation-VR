using UnityEngine;

public class XRayHandleController : MonoBehaviour
{
	[SerializeField] ConfigurableJoint configurableJoint;
	[SerializeField] Transform targetPosVert; // world-space reference for return-to-centre
	[SerializeField] float speed = 1.5f;

	Vector3 targetPosVertLocal;



	[SerializeField] DetectTouch detectTouch;

	void Update()
	{
		float returnToCentre = detectTouch.returntocent;
		TickReturnToCentre(ref returnToCentre);
		detectTouch.returntocent = returnToCentre;
	}



	void Awake()
	{
		targetPosVertLocal = transform.InverseTransformPoint(targetPosVert.position);
	}

	public void SetFreeMode()
	{
		configurableJoint.xMotion = ConfigurableJointMotion.Free;
		configurableJoint.zMotion = ConfigurableJointMotion.Free;
		configurableJoint.yMotion = ConfigurableJointMotion.Free;
	}

	public void SetVerticalMode()
	{
		configurableJoint.xMotion = ConfigurableJointMotion.Locked;
		configurableJoint.zMotion = ConfigurableJointMotion.Locked;
		configurableJoint.yMotion = ConfigurableJointMotion.Free;
	}

	public void TickReturnToCentre(ref float returnToCentre)
	{
		if (returnToCentre != 1) return;

		configurableJoint.targetPosition = Vector3.MoveTowards(
			configurableJoint.targetPosition,
			targetPosVertLocal,
			speed * Time.deltaTime);

		if (Vector3.Distance(configurableJoint.targetPosition, targetPosVertLocal) < 0.001f)
		{
			returnToCentre = 0;
		}
	}
}