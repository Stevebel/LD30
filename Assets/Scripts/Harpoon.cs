using UnityEngine;
using System.Collections;

public class Harpoon : MonoBehaviour
{
	private SpringJoint2D joint;
	[SerializeField] float lifespan = 1;
	private float timeLeft;

	public bool tethered;

	void Awake()
	{
		timeLeft = lifespan;
		tethered = false;
	}

	void FixedUpdate()
	{
		if(joint == null)
			joint = GetComponent<SpringJoint2D>();

		if(joint != null)
		{
			if((rigidbody2D.GetRelativePoint(joint.anchor) - joint.connectedBody.rigidbody2D.GetRelativePoint(joint.connectedAnchor)).magnitude > joint.distance)
				joint.enabled = true;
			else
				joint.enabled = false;
		}

		if(!tethered)
			timeLeft -= Time.fixedDeltaTime;
		if(timeLeft <= 0)
			Destroy(gameObject);
	}
}
