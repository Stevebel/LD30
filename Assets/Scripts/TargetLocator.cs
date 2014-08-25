using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetLocator : MonoBehaviour {
	public static List<HarpoonTarget> targets;
    private Transform _transform; 
    private SpriteRenderer sprite;
	// Use this for initialization
	void Awake () {
        _transform = transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
		targets = new List<HarpoonTarget>();
	}
	
	// Update is called once per frame
	void Update () {
        if (targets.Count > 0)
        {
            HarpoonTarget closest = null;
            float closestDist = float.MaxValue;
            foreach (HarpoonTarget target in targets)
            {
                float dist = (target.transform.position - _transform.position).magnitude;
                if (dist < closestDist)
                {
                    closest = target;
                    closestDist = dist;
                }
            }
            Transform targetTransform = closest.transform;
            float angle = Mathf.Atan2(_transform.position.y - targetTransform.position.y, _transform.position.x - targetTransform.position.x) * 180 / Mathf.PI + 90;
            _transform.rotation = Quaternion.Euler(0, 0, angle);

            sprite.enabled = !HarpoonGun.gun.AttachedToTarget();
        }
        else
        {
            sprite.enabled = false;
        }
	}
}
