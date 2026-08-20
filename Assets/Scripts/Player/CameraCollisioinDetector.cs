using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
// Uses rays forward and on both sides from its own transform to detect what is being hit and how far away it is
/// </summary>

public class CameraCollisionDetector : MonoBehaviour
{

	[SerializeField, Range(0, 0.5f), Tooltip("How often the detection fires (seconds)")] private float _scanInterval = 0.05f;
	[SerializeField, Tooltip("Distance each ray checks ahead for a collider")] private float _rayDistance = 0.2f;
	public List<RaycastHit> WallHits { get; private set; }


	private float _scanTimer = 0;



	private List<RaycastHit> ScanForObstacles (Vector3 position, float distance)
	{
		List<RaycastHit> hits = new();

		// Forward and both sides, covers the directions a head can lean towards
		List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };

		RaycastHit hit;
		foreach (var dir in directions)
		{
			if (Physics.Raycast(position, dir, out hit, distance))
			{
				hits.Add(hit);
			}
		}
		return hits;
	}





	private void Start()
	{
		WallHits = ScanForObstacles(transform.position, _rayDistance);
	}




	void Update()
	{
		_scanTimer += Time.deltaTime;
		if (_scanTimer > _scanInterval)
		{
			_scanTimer = 0;
			WallHits = ScanForObstacles(transform.position, _rayDistance);
		}
	}


}