using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LimitMovement : MonoBehaviour
{
	// limit the movement to this box
	public Vector3 minLocalPosition = new Vector3(-0.5f, 0f, -0.2f);
	public Vector3 maxLocalPosition = new Vector3(0.5f, 1.2f, 0.2f);

	private Rigidbody rigidBody;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 localPos = transform.localPosition;

		localPos.x = Mathf.Clamp(localPos.x, minLocalPosition.x, maxLocalPosition.x);
		localPos.y = Mathf.Clamp(localPos.y, minLocalPosition.y, maxLocalPosition.y);
		localPos.z = Mathf.Clamp(localPos.z, minLocalPosition.z, maxLocalPosition.z);

		rigidBody.MovePosition(transform.parent.TransformPoint(localPos));
	}
}