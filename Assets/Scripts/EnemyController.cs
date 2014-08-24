using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] Rigidbody2D enemyLaserPrefab;
	[SerializeField] float laserSpeed;
	[SerializeField] int maxHealth;

	private int currentHealth;

	void Awake()
	{
		currentHealth = maxHealth;
	}

	void Move(Vector2 direction)
	{
		rigidbody2D.velocity = direction.normalized * speed;
	}

	void Shoot()
	{
		Rigidbody2D laser = Instantiate(enemyLaserPrefab, transform.position, transform.rotation) as Rigidbody2D;
		laser.velocity = laser.transform.forward * laserSpeed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Harpoon")
			currentHealth--;

		if(currentHealth <= 0)
			Destroy(gameObject);
	}
}
