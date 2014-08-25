using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpaceCharacter))]
public class PlayerController : MonoBehaviour
{
	private SpaceCharacter character;

	public LayerMask playerLayer;

	[SerializeField] AudioSource music;

	public static PlayerController player;

	private static AudioSource musicCheck;

	void Awake()
	{
		player = this;
		character = GetComponent<SpaceCharacter> ();
		Random.seed = (int)Time.time;
		DontDestroyOnLoad(music);

		if(musicCheck == null)
			musicCheck = music;
		if(music != musicCheck)
			Destroy (music.gameObject);
	}

	void FixedUpdate()
	{		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		character.Move(h, v);
	}
}
