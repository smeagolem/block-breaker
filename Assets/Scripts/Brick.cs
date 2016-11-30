﻿using UnityEngine;

public class Brick : MonoBehaviour
{

    public int Health;
    public static int BreakableCount = 0;
    private Ball _Ball;
    private LevelManager _LevelManager;

    void Start()
    {
        _Ball = FindObjectOfType<Ball>();
        _LevelManager = FindObjectOfType<LevelManager>();
        BreakableCount++;
    }

    void Update()
    {
        if (Health <= 0)
        {
            BreakableCount--;
            _LevelManager.BrickDestroyed();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == _Ball.tag)
        {
            Health--;
        }
    }
}