using UnityEngine;
using System.Collections;

public class FlareController : MonoBehaviour
{
	void Start()
	{
		Invoke ("StopLooping", particleSystem.duration * 50);
		Invoke("Die", particleSystem.duration * 100);
	}

	void StopLooping()
	{
		particleSystem.loop = false;
	}
	
	void Die()
	{
		Destroy (gameObject);
	}
}
