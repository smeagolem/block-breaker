﻿using UnityEngine;

public class Brick : MonoBehaviour
{

    public int Health;
    public static int BreakableCount = 0;
    public Sprite[] Sprites;
    public GameObject Smoke;
    private Ball _Ball;
    private LevelManager _LevelManager;
    private Color _Color;

    void Start()
    {
        _Ball = FindObjectOfType<Ball>();
        _LevelManager = FindObjectOfType<LevelManager>();
        _Color = Random.ColorHSV(0, 1, 0.5f, 0.5f, 1, 1, 1, 1);
        GetComponent<SpriteRenderer>().color = _Color;
        BreakableCount++;
    }

    void Update()
    {
        if (Health <= 0)
        {
            BreakableCount--;
            _LevelManager.BrickDestroyed();
            // TODO: Play shatter sound effect.
            PuffSmoke();
            Destroy(gameObject);
        }
        else
        {
            if (Sprites[Health - 1] == null)
            {
                Debug.LogError("Cannot find brick sprite: Sprites[" + (Health - 1) + "]");
            }
            else if (GetComponent<SpriteRenderer>().sprite.name != Sprites[Health - 1].name)
            {
                GetComponent<SpriteRenderer>().sprite = Sprites[Health - 1];
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == _Ball.tag)
        {
            Health--;
            // TODO: Play crack sound effect.
        }
    }

    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(Smoke, transform.position, Quaternion.identity);
        ParticleSystem ps = smokePuff.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psMain = ps.main;
        psMain.startColor = _Color;
    }
}
