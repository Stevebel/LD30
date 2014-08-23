using UnityEngine;
using System.Collections;

public class Harpoon : MonoBehaviour
{
	private SpringJoint2D joint;

	void FixedUpdate()
	{
		if(joint == null)
			joint = GetComponent<SpringJoint2D>();

		if(joint != null)
		{
			Debug.Log("joint");
			if((rigidbody2D.position - joint.connectedBody.position).magnitude > joint.distance)
				joint.enabled = true;
			else
				joint.enabled = false;
		}
	}
}
