using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour
{
	[SerializeField] TargetPlanet planetPrefab;
	[SerializeField] float minRadius;
	[SerializeField] float maxRadius;

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
		int sign = Random.Range (0, 1);
		float yDistance = Random.Range (minRadius, maxRadius);

	}
}
