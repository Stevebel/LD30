using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetherDestroyer : TetherCollider
{
	public static List<TetherDestroyer> destroyers;

	private Renderer childRenderer;

	void Start()
	{
		childRenderer = GetComponentInChildren<Renderer>();
	}

	void FixedUpdate()
	{
		if(!childRenderer.isVisible)
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

	void OnDestroy()
	{
		destroyers.Remove(this);
	}
}
