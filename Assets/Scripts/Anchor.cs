using UnityEngine;
using System.Collections;

public class Anchor : TetherCollider {

	// Use this for initialization
	void Start () {
	
	}

    override public void OnTetherCollision(Tether tether, int segment)
    {
        if (tether.joint.gameObject != gameObject && tether.joint.gameObject.GetComponent<HarpoonTarget>() != null)
        {
            HarpoonGun.gun.TakeTether(tether);

            int numSegments = tether.segments + 1;
            //Create joint
            DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
            joint.distance = tether.GetJointDistance() * ((float)(numSegments - segment) / numSegments);
            if (joint.distance < 1)
            {
                joint.distance = 1;
            }
            joint.maxDistanceOnly = true;
            joint.connectedBody = tether.joint.rigidbody2D;

            joint.connectedAnchor = tether.joint.anchor;

            Destroy(tether.joint);
            //Move tether
            tether.joint = joint;
            //tether.transform.parent = target.transform;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
