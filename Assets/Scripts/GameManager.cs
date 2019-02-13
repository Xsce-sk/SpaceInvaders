using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int score = 0;
    private static int lives = 0;
    private static int difficulty = 1;

    void Awake()
    {
        score = 0;
    }

    public static void AddScore(int points)
    {
        score += points;
    }

    public static void LoseLife()
    {
        lives -= 1;
    }

    public static void IncreaseDiffculty()
    {
        difficulty -= 1;
    }

    public static void GameOver()
    {
        print("Game Over");
    }

    public static void StartGame()
    {
        
    }
}
