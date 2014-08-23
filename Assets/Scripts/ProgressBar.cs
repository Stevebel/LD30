using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour {
    public float percent = 1.0f;
    public GameObject inside;
    public Sprite fullSprite;
    public Sprite partialSprite;
    private Transform insideTransform;
    private SpriteRenderer insideSprite;
    // Use this for initialization
	void Start () {
        if (inside != null)
        {
            insideTransform = inside.transform;
            insideSprite = inside.GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (insideTransform != null)
        {
            insideTransform.localScale = new Vector3(percent, 1f, 1f);
        }
        if (insideSprite != null) {
            if (fullSprite != null && percent > 0.9999f)
            {
                insideSprite.sprite = fullSprite;
            }
            else if (partialSprite != null)
            {
                insideSprite.sprite = partialSprite;
            }
        }
	}
}
