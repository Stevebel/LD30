using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarpoonTarget : MonoBehaviour
{
	[SerializeField] int maxHarpoons = 1;
    [SerializeField] Tether tetherPrefab;
	[SerializeField] FlareController flarePrefab;
	[SerializeField] float damageCostMultiplier = 0.1f;

	public int currentHarpoons;
    public List<Anchor> attachedAnchors;

	public bool timeToDie;

	void Awake()
	{
		currentHarpoons = 0;
        attachedAnchors = new List<Anchor>();

        if (TargetLocator.targets == null)
        {
            TargetLocator.targets = new List<HarpoonTarget>();
        }
        TargetLocator.targets.Add(this);
		timeToDie = false;
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

                if (HarpoonGun.gun.Attach(this, collision.gameObject, anchor))
				{
                    currentHarpoons++;
                }else{
					Score.score.AddScore(damageCostMultiplier * -5f * Random.Range(0.8f,1.2f));
				}
            }
		}else{
			Score.score.AddScore (-collision.relativeVelocity.magnitude * Mathf.Sqrt(collision.rigidbody.mass) * damageCostMultiplier);
		}
	}

	void Die()
	{
		Destroy (gameObject);
	}

	void PrepareToDie()
	{
		FlareController flare = Instantiate (flarePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity) as FlareController;
		TargetLocator.targets.Remove (this);
		PlanetSpawner.spawner.Deregister(GetComponent<TargetPlanet>());
		Invoke ("Die", flare.particleSystem.duration);
	}

	void FixedUpdate()
	{
		if(TargetSecured() && !timeToDie)
		{
			timeToDie = true;
			Invoke ("PrepareToDie", 1f);
		}
	}
}
