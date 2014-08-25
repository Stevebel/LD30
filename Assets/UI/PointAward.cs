using UnityEngine;
using System.Collections;

public class PointAward : MonoBehaviour
{
	[SerializeField] float lifespan = .5f;

	void Start()
	{
		Invoke("Die", lifespan);
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
