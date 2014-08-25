using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Update()
	{
		transform.position = Camera.main.transform.position;
	}
}
