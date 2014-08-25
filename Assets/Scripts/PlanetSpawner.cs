using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour
{
	[SerializeField] TargetPlanet planetPrefab;
	[SerializeField] float minRadius;
	[SerializeField] float maxRadius;
	[SerializeField] Transform origin;

	private List<TargetPlanet> planets;

	public static PlanetSpawner spawner;

	void Awake()
	{
		planets = new List<TargetPlanet>();
		if(spawner != this)
			spawner = this;
	}

	void SpawnPlanets(int numPlanets)
	{
		for(int i = 0; i < numPlanets; i++)
		{
			SpawnPlanet();
		}
	}

	void SpawnPlanet()
	{
		float xDistance = Random.Range (minRadius, maxRadius);
		int sign = Random.Range (0, 2);
		if(sign < 1)
			sign--;
		xDistance *= sign;
		float yDistance = Random.Range (minRadius, maxRadius);
		sign = Random.Range (0, 2);
		if(sign < 1)
			sign--;
		yDistance *= sign;

		//transform.position = new Vector3(xDistance, yDistance, 0) + origin.position;
		float angle = Random.Range (0, 2 * Mathf.PI);
		float distance = Random.Range (minRadius, maxRadius);
		transform.position = new Vector3(Mathf.Cos (angle) * distance, Mathf.Sin (angle) * distance, 0) + origin.position;
		Debug.Log (transform.position);
		planets.Add (Instantiate(planetPrefab, transform.position, transform.rotation) as TargetPlanet);
	}

	public void Deregister(TargetPlanet planet)
	{
		planets.Remove (planet);
	}
}
