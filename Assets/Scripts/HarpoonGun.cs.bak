using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarpoonGun : MonoBehaviour {
    public ProgressBar cooldownBar;
    [Range(0.1f,60f)]public float cooldownSecs;
    public LayerMask targetableLayer;
    public Vector2 aim = new Vector2(0,0);
	public float cableLength;

	[SerializeField] Rigidbody2D harpoonPrefab;
	[SerializeField] float harpoonSpeed;

    [SerializeField] Tether tetherPrefab;

    private float cooldownRemaining;
    private GameObject[] targetable;
    private List<HarpoonTarget> currentlyAttached;
    private List<Tether> tethers;
    private Transform _transform;
	[SerializeField] SpriteRenderer harpoonSprite;

	public static HarpoonGun gun;

	// Use this for initialization
	void Start ()
	{
        _transform = transform;
        currentlyAttached = new List<HarpoonTarget>();
        tethers = new List<Tether>();

		if(gun != this)
			gun = this;
    }

    public void Shoot(bool withTether)
    {
        if (CanShoot())
        {
            cooldownRemaining = cooldownSecs;

            float angle = Mathf.Atan2(_transform.position.y - aim.y, _transform.position.x - aim.x) * 180 / Mathf.PI + 90;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D harpoon = Instantiate(harpoonPrefab, _transform.position, rotation) as Rigidbody2D;

            harpoon.velocity = harpoon.transform.up * harpoonSpeed;
            Vector2 oppositeForce = harpoon.velocity * harpoon.mass * (withTether ? -10f : -1f);
            PlayerController.player.rigidbody2D.AddForce(oppositeForce, ForceMode2D.Impulse);
            harpoon.velocity += PlayerController.player.rigidbody2D.velocity;
            harpoon.mass = 10f;

            if (withTether)
            {
                //Create joint
                SpringJoint2D joint = harpoon.gameObject.AddComponent<SpringJoint2D>();
                joint.anchor = new Vector2(0, -0.6f);
                joint.distance = cableLength;
                joint.frequency = 2f;
                joint.dampingRatio = 1f;
                joint.connectedBody = rigidbody2D;

                //Add tether
                Tether tether = Instantiate(tetherPrefab) as Tether;
                tether.joint = joint;
                tether.transform.parent = harpoon.transform;
                tethers.Add(tether);

				harpoon.gameObject.GetComponent<Harpoon>().tethered = true;
            }
        }
    }

    public bool Attach(HarpoonTarget target, GameObject harpoon, Vector2 anchor)
    {
        Tether tether = harpoon.GetComponentInChildren<Tether>();
        if (tether != null)
        {
            //Create joint
            DistanceJoint2D joint = target.gameObject.AddComponent<DistanceJoint2D>();
            joint.distance = cableLength;
            joint.maxDistanceOnly = true;
            joint.connectedBody = rigidbody2D;

            joint.anchor = anchor;

            //Move tether
            tether.joint = joint;
            tether.transform.parent = target.transform;

            Destroy(harpoon);

            currentlyAttached.Add(target);
            return true;
        }
        return false;
    }

    public bool CanShoot()
    {
        return cooldownRemaining <= 0.00001f;
    }

    public bool AttachedToTarget()
    {
        return currentlyAttached.Count > 0;
    }

    public bool TakeTether(Tether tether)
    {
        for(int i=0; i < currentlyAttached.Count; i++)
        {
            HarpoonTarget target = currentlyAttached[i];
            if (tether == target.GetComponentInChildren<Tether>())
            {
                Debug.Log("Grabbed");
                currentlyAttached.RemoveAt(i);
                break;
            }
        }
        return tethers.Remove(tether);
    }

    void ReleaseHarpoons()
    {
        foreach (HarpoonTarget target in currentlyAttached)
        {
            target.Detach();
        }
        currentlyAttached.Clear();

        foreach (Tether tether in tethers)
        {
            DestroyTether(tether);
        }
        tethers.Clear();
    }

    private void DestroyTether(Tether tether)
    {
        tether.joint.enabled = false;
        if (tether.joint != null)
        {
            Harpoon harpoon = tether.joint.gameObject.GetComponent<Harpoon>();
            if (harpoon != null)
                harpoon.tethered = false;
            Destroy(tether.joint);
        }
        Destroy(tether.gameObject);
    }

    void Update()
    {
        if (Input.GetButton("ReleaseHarpoon"))
        {
            ReleaseHarpoons();
        }
    }

	void FixedUpdate()
	{
		if (cooldownRemaining > 0)
		{
			harpoonSprite.enabled = false;
			cooldownRemaining -= Time.deltaTime;
			if (cooldownRemaining < 0)
			{
				cooldownRemaining = 0;
			}
		}
		else
			harpoonSprite.enabled = true;
		
		float cooldownPercent = (cooldownSecs - cooldownRemaining) / cooldownSecs;
		
		cooldownBar.percent = cooldownPercent;
		
		Vector2 direction = new Vector2(aim.x, aim.y);
		Rigidbody2D playerbody = PlayerController.player.rigidbody2D;
		float angle = Vector2.Angle(direction - playerbody.position, rigidbody2D.position - playerbody.position);
		if(angle > 60)
		{
			Vector3 cross = Vector3.Cross(direction - playerbody.position, rigidbody2D.position - playerbody.position);
			float sign = Mathf.Sign(cross.z);
			
			transform.RotateAround(PlayerController.player.transform.position, Vector3.forward, (60 - angle) * sign);
		}

        //Break attached tethers if they're stretched too far
        for (int i = currentlyAttached.Count - 1; i >= 0; i--)
        {
            Tether[] attTethers = currentlyAttached[i].GetComponentsInChildren<Tether>();
            foreach (Tether tether in attTethers)
            {
                if (tether.joint.connectedBody.gameObject == gameObject)
                {
                    float distance = (tether.endPoint - tether.startPoint).magnitude;
                    if (distance > tether.GetJointDistance() * 1.1)
                    {
                        currentlyAttached[i].Detach();
                        DestroyTether(tether);
                        tethers.Remove(tether);
                        currentlyAttached.RemoveAt(i);
                    }
                    break;
                }
            }
        }
	}
}
