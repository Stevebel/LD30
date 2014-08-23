using UnityEngine;
using System.Collections;

public class HarpoonTarget : MonoBehaviour
{
	[SerializeField] int maxHarpoons = 1;
    [SerializeField]
    Tether tetherPrefab;

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
		if(collision.gameObject.tag == "Harpoon")
		{
            //Destroy harpoon
            Destroy(collision.gameObject);
            if (currentHarpoons < maxHarpoons)
            {
                //Create joint
                DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
                joint.distance = HarpoonGun.gun.cableLength;
                joint.maxDistanceOnly = true;
                joint.connectedBody = HarpoonGun.gun.rigidbody2D;

                Vector2 collisionCenter = Vector2.zero;
                foreach (ContactPoint2D contact in collision.contacts)
                    collisionCenter += contact.point;
                collisionCenter /= collision.contacts.Length;
                joint.anchor = collisionCenter - rigidbody2D.position;

                //Draw tether
                Tether tether = Instantiate(tetherPrefab) as Tether;
                tether.joint = joint;



                //RaycastHit2D player = Physics2D.Raycast(collisionCenter, PlayerController.player.rigidbody2D.position - collisionCenter, Mathf.Infinity, PlayerController.player.playerLayer.value);
                //joint.connectedAnchor = player.point - PlayerController.player.rigidbody2D.position;

                currentHarpoons++;
            }
		}
	}
}
