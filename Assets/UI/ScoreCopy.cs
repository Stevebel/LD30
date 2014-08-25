using UnityEngine;
using System.Collections;

public class ScoreCopy : MonoBehaviour
{
	private TextMesh mesh;
	private TextMesh score;

	void Start()
	{
		mesh = GetComponent<TextMesh>();
		score = Score.score.GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update ()
	{
		mesh.text = score.text;
		mesh.color = score.color;
	}
}
