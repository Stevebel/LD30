using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour {
    public AnchoredJoint2D joint;
    public int segments = 10;
    public LineRenderer line;
    public Vector2 startPoint;
    public Vector2 endPoint;
    private static TetherCollider[] colliders;

	// Use this for initialization
	void Start ()
	{
        line = GetComponent<LineRenderer>();
	}

    public float GetJointDistance()
    {
        return (joint is SpringJoint2D) ? (joint as SpringJoint2D).distance : (joint as DistanceJoint2D).distance;
    }

	// Update is called once per frame
	void FixedUpdate ()
	{
        if (joint != null)
        {
            line.SetVertexCount(segments + 1);
            Vector3[] positions = new Vector3[segments + 1];

            Vector2 start = joint.connectedBody.GetRelativePoint(joint.connectedAnchor * joint.connectedBody.transform.lossyScale.x);
            //Vector2 end = joint.rigidbody2D.position + (rotate(joint.anchor, joint.rigidbody2D.rotation));
			Vector2 end = joint.rigidbody2D.GetRelativePoint(joint.anchor * joint.transform.lossyScale.x);
            Vector2 distance = (end - start);
            Vector2 direction = distance.normalized;
            Vector2 tangent = new Vector2(-direction.y, direction.x);

            float jointDistance = GetJointDistance();
            float difference = jointDistance * jointDistance - distance.magnitude * distance.magnitude;
            float height = difference <= 0 ? 0 : Mathf.Sqrt(difference) / 2;
            Vector2 current = start * 1;
            for (int i = 0; i <= segments; i++)
            {
                float pos = (i * 1f)/segments;
                current = Vector2.Lerp(start, end, pos);
                float heightPos = Mathf.Sin(pos * Mathf.PI);//1f - Mathf.Abs((pos) - .5f) * 2f;
                current = current + (tangent * (height * heightPos)); 
                
                Vector3 position = positions[i] = new Vector3(current.x, current.y, 200f);
                line.SetPosition(i, position);
                
            }

            //Do collision
            if (colliders == null)
            {
                colliders = GameObject.FindObjectsOfType<TetherCollider>();
            }
            int centerIndex = (int)Mathf.Floor((segments + 1) / 2f);
            Vector3 center = positions[centerIndex] * 1f;
            center.z = 0;
            float tetherRadius = distance.magnitude;

            foreach (TetherCollider collider in colliders)
            {
                //First do an easy check if the collider is very roughly in the bounds of the tether
                Collider2D otherCollider = collider.collider2D;
                
                float radius = otherCollider.bounds.extents.magnitude + tetherRadius;
                if ((otherCollider.bounds.center - center).magnitude <= radius)
                {
                    //Check if any segment overlaps
                    for (int i = 0; i <= segments; i++)
                    {
                        if (otherCollider.OverlapPoint(positions[i]))
                        {
                            collider.OnTetherCollision(this, i);
                        }
                    }
                }
            }
            /*
			foreach(TetherDestroyer destroyer in TetherDestroyer.destroyers)
			{
				Collider2D otherCollider = destroyer.collider2D;
				
				float radius = otherCollider.bounds.extents.magnitude + tetherRadius;
				if ((otherCollider.bounds.center - center).magnitude <= radius)
				{
					//Check if any segment overlaps
					for (int i = 0; i <= segments; i++)
					{
						if (otherCollider.OverlapPoint(positions[i]))
						{
							destroyer.OnTetherCollision(this, i);
						}
					}
				}
			}
            */
			//Set start and end
            startPoint = positions[0];
            endPoint = positions[segments];
		}
	}

    void LateUpdate()
    {
        colliders = null;
    }
    Vector2 rotate(Vector2 v, float angle)
    {
        angle *= Mathf.PI / 180;
        float x = v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle);
        float y = v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle);
        v.x = x;
        v.y = y;
        return v;
    }
}
