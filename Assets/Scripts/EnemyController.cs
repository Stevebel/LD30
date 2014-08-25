using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] Rigidbody2D enemyLaserPrefab;
	[SerializeField] float laserSpeed;
	[SerializeField] int maxHealth;
	[SerializeField] float orbitDistance;
	[SerializeField] float orbitAngle;
	[SerializeField] float cooldown;
	[SerializeField] float fireRange;
	[SerializeField] GameObject explosionPrefab;

	private float cooldownLeft = 0;
	private int clockwise;
	private TargetPlanet owner;

	private int currentHealth;

	void Awake()
	{
		currentHealth = maxHealth;
		clockwise = Random.Range(0, 2);
		if(clockwise == 0)
			clockwise--;
	}

	void Move(Vector2 direction)
	{
		rigidbody2D.velocity = direction.normalized * speed;
	}

	void Shoot()
	{
		Rigidbody2D laser = Instantiate(enemyLaserPrefab, transform.position, transform.rotation) as Rigidbody2D;
		laser.velocity = laser.transform.up * laserSpeed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Harpoon")
			currentHealth--;

		if(currentHealth <= 0)
		{
			owner.Deregister(this);
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		PlayerController player = PlayerController.player;
		Vector2 towardsPlayer = player.rigidbody2D.position - rigidbody2D.position;

		Vector3 target = -towardsPlayer.normalized * orbitDistance + player.rigidbody2D.position;
		Vector3 current = transform.position;
		transform.position = target;
		transform.RotateAround(player.transform.position, Vector3.forward, orbitAngle * clockwise);
		target = transform.position;
		transform.position = current;
		Move((Vector2)target - rigidbody2D.position);

		float angle = Mathf.Atan2(rigidbody2D.position.y - player.rigidbody2D.position.y, rigidbody2D.position.x - player.rigidbody2D.position.x) * 180 / Mathf.PI + 90;
		transform.eulerAngles = new Vector3(0, 0, angle);

		if(cooldownLeft > 0)
			cooldownLeft -= Time.fixedDeltaTime;
		if(towardsPlayer.magnitude < fireRange && cooldownLeft <= 0)
		{
			Shoot();
			cooldownLeft = cooldown;
		}
	}

	public void RegisterOwner(TargetPlanet planet)
	{
		owner = planet;
	}
}
