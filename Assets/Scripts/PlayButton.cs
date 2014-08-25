using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
    public Sprite normalSprite;
    public Sprite overSprite;
    public Sprite pressSprite;
    public string gameScene = "Game";

    private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnMouseEnter()
    {
        sprite.sprite = overSprite;
    }
    void OnMouseExit()
    {
        sprite.sprite = normalSprite;
    }

    void OnMouseDown()
    {
        sprite.sprite = pressSprite;
    }

    void OnMouseUp()
    {
        sprite.sprite = overSprite;
		Time.timeScale = 1;
        Application.LoadLevel(gameScene);
    }
}
