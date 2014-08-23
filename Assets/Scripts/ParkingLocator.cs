using UnityEngine;
using System.Collections;

public class ParkingLocator : MonoBehaviour {
    public Parking parking;
    private Transform _transform;
	// Use this for initialization
	void Start () {
        parking = FindObjectOfType<Parking>();
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        Transform parkingTransform = parking.transform;
        float angle = Mathf.Atan2(_transform.position.y - parkingTransform.position.y, _transform.position.x - parkingTransform.position.x) * 180/Mathf.PI + 90;
	    _transform.rotation = Quaternion.Euler(0,0,angle);
	}
}
