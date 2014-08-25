using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
	void Start()
	{
		Invoke("Die", particleSystem.duration);
	}

	void Die()
	{
		Destroy (gameObject);
	}
}
