using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
	[SerializeField] AudioClip[] explosionSound;

	void Start()
	{
		audio.clip = explosionSound[Random.Range (0, explosionSound.Length)];
		audio.Play ();
		Invoke("Die", particleSystem.duration);
	}

	void Die()
	{
		Destroy (gameObject);
	}
}
