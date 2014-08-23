using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour {
    public float percent = 1.0f;
    public GameObject inside;
    private Transform insideTransform;
    // Use this for initialization
	void Start () {
        if (inside != null)
        {
            insideTransform = inside.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (insideTransform != null)
        {
            insideTransform.localScale = new Vector3(percent, 1f, 1f);
        }
	}
}
