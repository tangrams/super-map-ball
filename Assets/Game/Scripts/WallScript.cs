using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	public GameObject Character;

	public float NudgeFactor = 0.8f;

	private Bounds bbox;

	void Start()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			var wallSide = transform.GetChild(i);
			var meshCollider = wallSide.GetComponent<MeshCollider>() as MeshCollider;
			bbox.Encapsulate(meshCollider.bounds);
		}
	}

	void Update()
	{
		if (!bbox.Contains(Character.transform.position))
		{
			Vector3 distance = Character.transform.position - bbox.center;

			float scale = Character.transform.localScale.x;
			distance -= new Vector3(scale, scale, scale) * 0.5f;

			bool outsideUpperBounds = Mathf.Abs(Character.transform.position.y + scale) > bbox.extents.y;
			bool outsideSideBounds = Mathf.Abs(distance.x) > bbox.extents.x || Mathf.Abs(distance.z) > bbox.extents.z;

			if (outsideSideBounds && outsideUpperBounds || outsideSideBounds)
			{
				var rigibBody = Character.GetComponent<Rigidbody>() as Rigidbody;
				Vector3 force = (bbox.center - Character.transform.position).normalized;
				rigibBody.AddForce(force * NudgeFactor * rigibBody.velocity.magnitude, ForceMode.Impulse);
			}
		}
	}
}
