using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour
{
	[SerializeField] TargetPlanet planetPrefab;
	[SerializeField] float minRadius;
	[SerializeField] float maxRadius;
	[SerializeField] Transform origin;
	[SerializeField] float captureReward = 50;
	[SerializeField] AudioClip[] captureSound;

	private List<TargetPlanet> planets;
	private int capturedCount;

	public static PlanetSpawner spawner;

	void Awake()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;

		planets = new List<TargetPlanet>();
		//if(spawner != this)
			spawner = this;

		SpawnPlanets (1);
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
		float angle = Random.Range (0, 2 * Mathf.PI);
		float distance = Random.Range (minRadius, maxRadius);
		transform.position = new Vector3(Mathf.Cos (angle) * distance, Mathf.Sin (angle) * distance, 0) + origin.position;
		planets.Add (Instantiate(planetPrefab, transform.position, transform.rotation) as TargetPlanet);
	}

	public void Deregister(TargetPlanet planet)
	{
		planets.Remove (planet);
		Score.score.AddScore (captureReward, planet.transform.position, 0);
		Camera.main.audio.clip = captureSound[Random.Range (0, captureSound.Length)];
		Camera.main.audio.Play ();

		capturedCount++;
		SpawnNeeded ();
	}
	private static float LOG_2 = Mathf.Log(2);
	void SpawnNeeded(){
		int countForLevel = (int)Mathf.Round(Mathf.Log (capturedCount + 1) / LOG_2) + 1;
		int needed = countForLevel - planets.Count;
		SpawnPlanets (needed);
	}
}