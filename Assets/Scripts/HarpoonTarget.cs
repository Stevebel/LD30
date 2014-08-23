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
}
