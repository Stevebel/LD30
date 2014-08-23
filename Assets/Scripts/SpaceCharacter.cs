using UnityEngine;
using System.Collections;

public class SpaceCharacter : MonoBehaviour
{
	[SerializeField] float acceleration;
	[SerializeField] float maxSpeed;
	[SerializeField] bool forceMove;
	[SerializeField] float mass = 1;

	void Awake()
	{
		rigidbody2D.mass = mass;
	}

	public void Move(float horiz, float vert)
	{
		if(forceMove)
			ForceMove (horiz, vert);
		else
			VelMove (horiz, vert);
	}

	public void VelMove(float horiz, float vert)
	{
		rigidbody2D.velocity = new Vector2 (horiz * maxSpeed, vert * maxSpeed);
	}

	public void ForceMove(float horiz, float vert)
	{
		rigidbody2D.AddForce (new Vector2 (horiz * acceleration, vert * acceleration));
		Vector2 velocity = rigidbody2D.velocity;
		velocity.x = Mathf.Clamp (velocity.x, -maxSpeed, maxSpeed);
		velocity.y = Mathf.Clamp (velocity.y, -maxSpeed, maxSpeed);
		//rigidbody2D.velocity = velocity;
	}

	public void Rotate(Quaternion rotation)
	{
		transform.rotation = rotation;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
		rigidbody2D.angularVelocity = 0;
	}
}
