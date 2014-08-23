using UnityEngine;
using System.Collections;

public class TowCable : MonoBehaviour
{
	[SerializeField] private CableEnd hooker;
	[SerializeField] private CableEnd hookee;

	[SerializeField] float length;
	[SerializeField] float kcoeff;

	void FixedUpdate()
	{
		Vector2 direction = hooker.transform.position - hookee.transform.position;
		float distance = direction.magnitude;
		direction.Normalize ();

		if(distance > length)
		{
			hooker.AddForce(-direction, distance - length, kcoeff);
			hookee.AddForce(direction, distance - length, kcoeff);
		}
	}
}
