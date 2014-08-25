using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetherDestroyer : TetherCollider
{
	public static List<TetherDestroyer> destroyers;
	

	void Awake()
	{
	}

	void FixedUpdate()
	{
		Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
		if(viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
			Destroy(gameObject);
	}

	override public void OnTetherCollision(Tether tether, int segment)
	{
		if(tether.joint.gameObject.tag == "Anchor")
			return;
		HarpoonGun.gun.TakeTether(tether);
		HarpoonTarget target = tether.joint.gameObject.GetComponent<HarpoonTarget>();
		if(target != null)
			target.currentHarpoons--;
		Harpoon harpoon = tether.joint.gameObject.GetComponent<Harpoon>();
		if(harpoon != null)
			harpoon.tethered = false;
		Destroy(tether.joint);
		Destroy(tether.gameObject);
	}

	~TetherDestroyer()
	{
		destroyers.Remove(this);
	}
}
