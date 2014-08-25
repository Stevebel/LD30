using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour //oh man should this be reused code
{
	[SerializeField] Rigidbody2D rockPrefab;
	[SerializeField] float minRadius;
	[SerializeField] float maxRadius;
	[SerializeField] Transform origin;
	[SerializeField] int numberOfRocks;
	[SerializeField] float maxSpeed;
	[SerializeField] float maxTorque;
	

	public static RockSpawner spawner;
	
	void Awake()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;

		//if(spawner != this)
		spawner = this;
		
		SpawnRocks (numberOfRocks);
	}
	
	void SpawnRocks(int numRocks)
	{
		for(int i = 0; i < numRocks; i++)
		{
			SpawnPlanet();
		}
	}
	
	void SpawnPlanet()
	{		
		float angle = Random.Range (0, 2 * Mathf.PI);
		float distance = Random.Range (minRadius, maxRadius);
		transform.position = new Vector3(Mathf.Cos (angle) * distance, Mathf.Sin (angle) * distance, 0) + origin.position;
		Rigidbody2D rock = Instantiate(rockPrefab, transform.position, transform.rotation) as Rigidbody2D;
		rock.velocity = new Vector2(Random.Range (-maxSpeed, maxSpeed), Random.Range (-maxSpeed, maxSpeed));
		rock.angularVelocity = Random.Range (-maxTorque, maxTorque);
	}
}
