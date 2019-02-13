using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int lives = 0;
    public static int charge = 1;
    public static int difficulty = 1;

    private static GameObject m_StartMenu;
    private static GameObject m_ExplosionPrefab;

    protected Transform m_Transform;

    void Start()
    {
        m_Transform = transform;
        m_ExplosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");

        m_StartMenu = m_Transform.GetChild(0).gameObject;
    }

    public static void AddScore(int points)
    {
        print("Adding " + points + " Points");
        score += points;
    }

    public static void LoseLife()
    {
        lives -= 1;
        print("Losing Life, Current Life: " + lives);
    }

    public static void IncreaseCharge()
    {
        if (charge < 3)
        {
            charge++;
            print("Gaining Charge, Current Charge: " + charge);
        }
    }

    public static void ResetCharge()
    {
        print("Reset Charge");
        charge = 1;
    }

    public static void SpawnExplosion(Vector3 position, float scale)
    {
        GameObject spawnedExplosion = Instantiate(m_ExplosionPrefab, position, Quaternion.identity) as GameObject;
        spawnedExplosion.transform.localScale = Vector3.one * scale;
    }

    public void IncreaseDiffculty()
    {
        difficulty -= 1;
    }

    public void GameOver()
    {
        print("Game Over");
    }

    public void StartGame()
    {
        m_StartMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
