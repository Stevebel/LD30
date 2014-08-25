using UnityEngine;
using System.Collections.Generic;

public class Anchor : TetherCollider
{
	private List<Rigidbody2D> targets;

	void Awake()
	{
		targets = new List<Rigidbody2D>();
	}

    override public void OnTetherCollision(Tether tether, int segment)
    {
        HarpoonTarget harpoonTarget = tether.joint.gameObject.GetComponent<HarpoonTarget>();
        if (harpoonTarget != null)
        {
			Rigidbody2D target = tether.joint.gameObject.rigidbody2D;
			if(!targets.Contains(target))
			{
	            HarpoonGun.gun.TakeTether(tether);

	            int numSegments = tether.segments + 1;
	            //Create joint
				SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
				joint.distance = tether.GetJointDistance() * ((float)(numSegments - segment) / numSegments);
	            if (joint.distance < 2)
	            {
                    joint.distance = 2;
	            }
				joint.dampingRatio = 1;
	            joint.connectedBody = tether.joint.rigidbody2D;

	            joint.connectedAnchor = tether.joint.anchor;

	            Destroy(tether.joint);
	            //Move tether
	            tether.joint = joint;
	            //tether.transform.parent = target.transform;
                harpoonTarget.AttachToAnchor(this);
				targets.Add(target);
			}
        }
    }
}
