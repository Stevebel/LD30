using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float secondsLeft = 120;
	private TextMesh text;
	[SerializeField] GameObject gameOver;

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<TextMesh> ();
		gameOver.SetActive (false);
	}

	void TimerEnded()
	{
		Debug.Log ("Game over");
		Time.timeScale = 0;
		gameOver.SetActive (true);
	}

	// Update is called once per frame
	void Update ()
	{
		secondsLeft -= Time.deltaTime;
		if (secondsLeft < 0) {
			secondsLeft = 0;
			TimerEnded();
		}

		int minutesLeft = (int)(secondsLeft / 60f);
		text.text = minutesLeft + ":" + (secondsLeft % 60f).ToString ("00.##");
	}
}
