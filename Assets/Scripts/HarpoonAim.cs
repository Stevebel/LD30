using UnityEngine;
using System.Collections;

public class HarpoonAim : MonoBehaviour {
    public Camera camera;
    public HarpoonGun harpoon;
	[SerializeField] Transform turret;

	// Use this for initialization
	void Start ()
	{
        //Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
        Screen.showCursor = false;
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

		float angle = Mathf.Atan2(transform.position.y - harpoon.transform.position.y, transform.position.x - harpoon.transform.position.x) * 180 / Mathf.PI;
		Quaternion rotation = Quaternion.Euler(0, 0, angle);
		turret.transform.rotation = rotation;

		harpoon.aim = new Vector2(mousePosition.x, mousePosition.y);

        if (Input.GetMouseButton(0))
        {
            harpoon.Shoot(false);
        }
        else if (Input.GetMouseButton(1))
        {
            harpoon.Shoot(true);
        }
	}
}
