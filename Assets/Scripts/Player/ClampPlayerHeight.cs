using UnityEngine;

public class ClampPlayerHeight : MonoBehaviour
{
	private Transform playerRig;
	private float lockedY;

	void Start()
	{
		playerRig = transform;
		lockedY = playerRig.position.y;
	}

	void LateUpdate()
	{
		Vector3 pos = playerRig.position;
		if (Mathf.Abs(pos.y - lockedY) > 0.001f)
		{
			pos.y = lockedY;
			playerRig.position = pos;
		}
	}
}