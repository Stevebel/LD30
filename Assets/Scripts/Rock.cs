using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour
{
	[SerializeField] int maxHealth;
	[SerializeField] GameObject explosionPrefab;
	[SerializeField] AudioClip[] explosionSound;

	private int currentHealth;

	void Awake()
	{
		currentHealth = maxHealth;
        rigidbody2D.rotation = Random.Range(0, 359);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Harpoon")
        {
            currentHealth--;
            Destroy(collision.gameObject);
        }

		if(currentHealth <= 0)
		{
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			Camera.main.audio.clip = explosionSound[Random.Range (0, explosionSound.Length)];
			Camera.main.audio.Play ();
			Destroy(gameObject);
		}
	}
}
