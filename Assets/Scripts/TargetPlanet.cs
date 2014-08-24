using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPlanet : MonoBehaviour
{
	[SerializeField] float mass = 1;
	[SerializeField] int maxEnemies;
	[SerializeField] float cooldown;
	[SerializeField] EnemyController enemyPrefab;

	private float cooldownLeft;
	private int numEnemies;
	private List<EnemyController> enemies;

	void Awake()
	{
		rigidbody2D.mass = mass;
		enemies = new List<EnemyController>();
		cooldownLeft = cooldown;
		numEnemies = 0;
	}

	void FixedUpdate()
	{
		if(cooldownLeft > 0)
			cooldownLeft -= Time.fixedDeltaTime;

		if(cooldownLeft <= 0 && numEnemies < maxEnemies)
		{
			EnemyController enemy = Instantiate(enemyPrefab, transform.position, transform.rotation) as EnemyController;
			enemy.RegisterOwner(this);
			enemies.Add(enemy);
			numEnemies++;
			cooldownLeft = cooldown;
		}
	}

	public void Deregister(EnemyController enemy)
	{
		enemies.Remove(enemy);
		numEnemies--;
	}
}
