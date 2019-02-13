﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    private static int score = 0;
    private static int life = 3;
    private static int difficulty = 1;
    
    void Awake()
    {
        score = 0;
        life = 3;
    }

    public static void AddScore(int points)
    {
        score += points;
    }

    public static void LoseLife()
    {
        life -= 1;
    }

    public static void IncreaseDiffculty()
    {
        difficulty -= 1;
    }

    public static void GameOver()
    {
        print("Game Over");
    }

    public static void RestartLevel()
    {
        print("Restart Level");
    }
}
