using UnityEngine;
using System.Collections;

public class HarpoonAim : MonoBehaviour {
    public Camera camera;
    public HarpoonGun harpoon;
	// Use this for initialization
	void Start () {
        //Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
        Screen.showCursor = false;
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

        if (Input.GetMouseButton(0))
        {
            harpoon.aim = new Vector2(mousePosition.x, mousePosition.y);
            harpoon.Shoot();
        }
	}
}
