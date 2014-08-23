using UnityEngine;
using System.Collections;

public class TargetPlanet : MonoBehaviour
{
	[SerializeField] private Transform hooker;
	[SerializeField] float acceleration;
	[SerializeField] float maxSpeed;
	[SerializeField] float mass = 1;

	void Awake()
	{
		rigidbody2D.mass = mass;
	}

	void FixedUpdate()
	{
		if(hooker != null)
		{
			Vector2 direction = hooker.position - transform.position;
			float distance = direction.magnitude;
			direction.Normalize ();
			rigidbody2D.AddForce (direction * acceleration * distance);
			Vector2 velocity = rigidbody2D.velocity;
			velocity.x = Mathf.Clamp (velocity.x, -maxSpeed, maxSpeed);
			velocity.y = Mathf.Clamp (velocity.y, -maxSpeed, maxSpeed);
			rigidbody2D.velocity = velocity;
		}
	}
}
