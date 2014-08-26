using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
	[SerializeField] AudioClip[] explosionSound;

	void Start()
	{
		Camera.main.audio.clip = explosionSound[Random.Range (0, explosionSound.Length)];
		Camera.main.audio.Play ();
		Invoke("Die", particleSystem.duration);
	}

	void Die()
	{
		Destroy (gameObject);
	}
}
