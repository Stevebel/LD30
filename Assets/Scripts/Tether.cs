using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour {
    public AnchoredJoint2D joint;
    public int segments = 10;
    private LineRenderer line;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (joint != null)
        {
            line.SetVertexCount(segments + 1);

            Vector2 start = joint.connectedBody.position + joint.connectedAnchor;
            Vector2 end = joint.rigidbody2D.position + (rotate(joint.anchor, joint.rigidbody2D.rotation));
            Vector2 distance = (end - start);
            Vector2 direction = distance.normalized;
            Vector2 tangent = new Vector2(-direction.y, direction.x);

            float jointDistance = (joint is SpringJoint2D) ? (joint as SpringJoint2D).distance : (joint as DistanceJoint2D).distance;
            float difference = jointDistance * jointDistance - distance.magnitude * distance.magnitude;
            float height = difference <= 0 ? 0 : Mathf.Sqrt(difference) / 2;
            Vector2 current = start * 1;
            for (int i = 0; i <= segments; i++)
            {
                float pos = (i * 1f)/segments;
                current = Vector2.Lerp(start, end, pos);
                float heightPos = Mathf.Sin(pos * Mathf.PI);//1f - Mathf.Abs((pos) - .5f) * 2f;
                current = current + (tangent * (height * heightPos)); 
                line.SetPosition(i, new Vector3(current.x, current.y, 200f));
            }
        }
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
