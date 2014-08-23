﻿using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {
    public ProgressBar cooldownBar;
    [Range(1f,60f)]public float cooldownSecs;
    public LayerMask targetableLayer;
    public Vector2 aim = new Vector2(0,0);
	public float cableLength;

	[SerializeField] Rigidbody2D harpoonPrefab;
	[SerializeField] float harpoonSpeed;

    private float cooldownRemaining;
    private GameObject[] targetable;
    private Transform _transform;

	public static HarpoonGun gun;

	// Use this for initialization
	void Start ()
	{
        _transform = transform;

		if(gun != this)
			gun = this;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            cooldownRemaining = cooldownSecs;

            float angle = Mathf.Atan2(_transform.position.y - aim.y, _transform.position.x - aim.x) * 180/Mathf.PI + 90;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D harpoon = Instantiate(harpoonPrefab, _transform.position, rotation) as Rigidbody2D;

            harpoon.velocity = harpoon.transform.up * harpoonSpeed;
            harpoon.mass = .001f;
        }
        /*
        if (CanShoot())
        {
            Debug.Log("Shoot at: " + aim.x +", "+aim.y);
            cooldownRemaining = cooldownSecs;

            RaycastHit2D targetHit = Physics2D.Raycast(new Vector2(_transform.position.x, _transform.position.y), aim, 1000f, targetableLayer);
            if (targetHit.collider != null)
            {
                GameObject targetObject = targetHit.collider.gameObject;
                HarpoonTarget target = targetObject.GetComponent<HarpoonTarget>();
                if (target != null)
                {
                    target.Hit(this);
                }
            }
        }*/
    }


    public bool CanShoot()
    {
        return cooldownRemaining <= 0.00001f;
    }

	// Update is called once per frame
	void FixedUpdate()
	{
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
            if (cooldownRemaining < 0)
            {
                cooldownRemaining = 0;
            }
        }
        
        float cooldownPercent = (cooldownSecs - cooldownRemaining) / cooldownSecs;

        cooldownBar.percent = cooldownPercent;
	}
}
