using UnityEngine;
using System.Collections;

public class CableEnd : MonoBehaviour
{
	[SerializeField] public Rigidbody2D target;

	public void AddForce(Vector2 direction, float distance, float kcoeff)
	{
		Vector2 force = kcoeff * direction * distance;
		target.AddForce (force);
	}
}
