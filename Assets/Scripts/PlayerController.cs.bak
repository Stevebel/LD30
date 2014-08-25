using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceCharacter))]
public class PlayerController : MonoBehaviour
{
	private SpaceCharacter character;

	public LayerMask playerLayer;

	public static PlayerController player;

	void Awake()
	{
		player = this;
		character = GetComponent<SpaceCharacter> ();
	}

	void FixedUpdate()
	{		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		character.Move(h, v);
	}
}
