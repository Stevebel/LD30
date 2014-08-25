using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public static Score score;

	public Color positiveColor;
	public Color negativeColor;
	public float multiplier = 1000000;
	public double value;
	private TextMesh text;
	// Use this for initialization
	void Start () {
		score = this;
		text = GetComponent<TextMesh> ();
	}

	public void AddScore(float amount){
		value += amount;
	}
	// Update is called once per frame
	void Update () {
		if (value > 0) {
			text.color = positiveColor;
		}else{
			text.color = negativeColor;
		}
		text.text = "$"+((long)(value * multiplier)).ToString("N0");
	}
}
