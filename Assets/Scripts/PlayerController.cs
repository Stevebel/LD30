using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceCharacter))]
public class PlayerController : MonoBehaviour
{
	private SpaceCharacter character;

	void FixedUpdate()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rotation = Quaternion.LookRotation (character.transform.position - mousePosition, Vector3.forward);
		character.Rotate (rotation);

		#if CROSS_PLATFORM_INPUT
			float h = CrossPlatformInput.GetAxis("Horizontal");
			float v = CrossPlatformInput.GetAxis("Vertical");
		#else
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
		#endif

		character.Move(h, v);
	}
}
