using UnityEngine;
using System.Collections;

public class HarpoonTarget : MonoBehaviour
{
	[SerializeField] int maxHarpoons = 1;

	private int currentHarpoons;

	void Awake()
	{
		currentHarpoons = 0;
	}

    public bool Hit(HarpoonGun harpoon)
    {
		if(currentHarpoons == maxHarpoons)
			return false;

		//Attach a harpoon to the target
		DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
		joint.distance = harpoon.cableLength;
		joint.maxDistanceOnly = true;
		joint.connectedBody = PlayerController.player.rigidbody2D;
		currentHarpoons++;
		return true;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(currentHarpoons < maxHarpoons && collision.gameObject.tag == "Harpoon")
		{
			DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
			joint.distance = HarpoonGun.gun.cableLength;
			joint.maxDistanceOnly = true;
			joint.connectedBody = PlayerController.player.rigidbody2D;

			Vector2 point = Vector2.zero; 
			foreach(ContactPoint2D contact in collision.contacts)
				point += contact.point;
			point /= collision.contacts.Length;
			joint.anchor = point - rigidbody2D.position;

			RaycastHit2D player = Physics2D.Raycast(point, PlayerController.player.rigidbody2D.position - point, Mathf.Infinity, PlayerController.player.playerLayer.value);
			joint.connectedAnchor = player.point - PlayerController.player.rigidbody2D.position;

			currentHarpoons++;
		}
	}
}
