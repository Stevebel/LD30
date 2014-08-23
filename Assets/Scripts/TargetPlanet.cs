using UnityEngine;
using System.Collections;

public class TargetPlanet : MonoBehaviour
{
	[SerializeField] float mass = 1;

	void Awake()
	{
		rigidbody2D.mass = mass;
	}
}
