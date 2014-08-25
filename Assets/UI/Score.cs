using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public static Score score;

	public Color positiveColor;
	public Color negativeColor;
	public float multiplier = 1000000;
	public double value;

	[SerializeField] TextMesh pointPrefab;

	private TextMesh text;

	// Use this for initialization
	void Awake () {
		score = this;
		text = GetComponent<TextMesh> ();
	}

	public void AddScore(float amount)
	{
		value += amount;
	}

	public void AddScore(float amount, Vector3 position, float angle)
	{
		AddScore (amount);
		position.z--;
		TextMesh point = Instantiate (pointPrefab, position, Quaternion.Euler (0, 0, angle)) as TextMesh;
		point.color = positiveColor;
		if(amount < 0)
		{
			point.color = negativeColor;
			point.text = "-";
		}
		point.text += "$" + (int)Mathf.Abs(amount * multiplier);
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (value > 0)
			text.color = positiveColor;
		else
			text.color = negativeColor;

		text.text = "$"+((long)(value * multiplier)).ToString("N0");
	}
}
