using UnityEngine;
using System.Collections;

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
    private Transform _transform;

	public static HarpoonGun gun;

	private float playerSize;

	// Use this for initialization
	void Start ()
	{
        _transform = transform;

		if(gun != this)
			gun = this;

		playerSize = PlayerController.player.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            cooldownRemaining = cooldownSecs;

            float angle = Mathf.Atan2(_transform.position.y - aim.y, _transform.position.x - aim.x) * 180 / Mathf.PI + 90;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D harpoon = Instantiate(harpoonPrefab, _transform.position, rotation) as Rigidbody2D;

            harpoon.velocity = harpoon.transform.up * harpoonSpeed;
            harpoon.mass = .001f;

            //Create joint
            SpringJoint2D joint = harpoon.gameObject.AddComponent<SpringJoint2D>();
			joint.anchor = new Vector2(0, -0.6f);
            joint.distance = cableLength;
            //joint.maxDistanceOnly = true;
            joint.connectedBody = rigidbody2D;

            //joint.anchor = collisionCenter - rigidbody2D.position;

            //Draw tether
            Tether tether = Instantiate(tetherPrefab) as Tether;
            tether.joint = joint;
            tether.transform.parent = harpoon.transform;
        }
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

		Vector2 direction = new Vector2(aim.x, aim.y).normalized;
		Rigidbody2D playerbody = PlayerController.player.rigidbody2D;
		float angle = Vector2.Angle(playerbody.GetRelativePoint(direction), playerbody.GetRelativePoint(rigidbody2D.position));
		Debug.Log(angle);
		if(angle > 90)
		{
			Vector3 cross = Vector3.Cross(playerbody.GetRelativePoint(direction), playerbody.GetRelativePoint(rigidbody2D.position));
			float sign = Mathf.Sign(cross.z);

			transform.RotateAround(PlayerController.player.transform.position, Vector3.forward, (90 - angle) * sign);
		}
	}
}
