using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
// Reads the hits reported by a CameraCollisionDetector each frame and pushes the rig away from them through the CharacterController
// so the camera can't keep moving into a wall.
/// </summary>
public class CameraCollisionHandler : MonoBehaviour
{
	[SerializeField, Tooltip("The HeadCollisionDetector doing the raycasting")]	private CameraCollisionDetector _cameraDetector;
	[SerializeField, Tooltip("Character controller on the XR Origin that actually gets moved")]	private CharacterController _rigController;
	[SerializeField, Tooltip("How strongly the rig gets pushed away from a detected wall")]	public float pushStrength = 0.3f;




	private Vector3 CalculatePushDirection(List<RaycastHit> hits)
	{
		Vector3 combinedNormal = Vector3.zero;
		foreach (RaycastHit hit in hits)
		{
			// Flatten to XZ so the push only moves the rig sideways, not up or down
			combinedNormal += new Vector3(hit.normal.x, 0, hit.normal.z); ;
		}
		return combinedNormal;
	}




	private void Update()
	{
		if (_cameraDetector.WallHits.Count <= 0)
		{
			return;
		}


		Vector3 pushDirection = CalculatePushDirection(_cameraDetector.WallHits);

		Debug.DrawRay(transform.position, pushDirection.normalized, Color.magenta);

		_rigController.Move(pushDirection.normalized * pushStrength * Time.deltaTime);
	}
}