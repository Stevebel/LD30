using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarpoonTarget : MonoBehaviour
{
	[SerializeField] int maxHarpoons = 1;
    [SerializeField] Tether tetherPrefab;

	public int currentHarpoons;

    public List<Anchor> attachedAnchors;
	void Awake()
	{
		currentHarpoons = 0;
        attachedAnchors = new List<Anchor>();

        if (TargetLocator.targets == null)
        {
            TargetLocator.targets = new List<HarpoonTarget>();
        }
        TargetLocator.targets.Add(this);
	}

    public void Detach()
    {
        currentHarpoons = 0;
    }
    public void AttachToAnchor(Anchor anchor)
    {
        attachedAnchors.Add(anchor);
    }
    bool TargetSecured()
    {
        return attachedAnchors.Count >= 3;
    }
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Harpoon")
		{
            
            if (currentHarpoons < maxHarpoons)
            {
                Vector2 collisionCenter = Vector2.zero;
                foreach (ContactPoint2D contact in collision.contacts)
                    collisionCenter += contact.point;
                collisionCenter /= collision.contacts.Length;
                Vector2 anchor = collisionCenter - rigidbody2D.position;

                if (HarpoonGun.gun.Attach(this, collision.gameObject, anchor)) {
                    currentHarpoons++;
                }
            }
		}
	}
}
