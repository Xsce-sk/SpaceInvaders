using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int score = 0;
    public static int lives = 3;
    public static int charge = 1;
    public static int difficulty = 1;

    protected static Vector3 m_OriginalCameraPosition;
    private static GameObject m_StartMenu;
    private static GameObject m_ExplosionPrefab;

    protected static Transform m_Transform;
    protected static Transform m_CameraTransform;
    protected static Transform m_PlayerTransform;
    protected static Transform m_EnemiesTransform;

    protected static PlayerController m_PlayerController;
    protected static LivesText m_LivesText;
    protected static ScoreText m_ScoreText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_Transform = transform;
        m_CameraTransform = Camera.main.transform;
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_EnemiesTransform = GameObject.FindGameObjectWithTag("Player").transform;


        m_PlayerController = m_PlayerTransform.gameObject.GetComponent<PlayerController>();
        m_LivesText = GetComponentInChildren<LivesText>();
        m_ScoreText = GetComponentInChildren<ScoreText>();
        
        m_OriginalCameraPosition = m_CameraTransform.position;
        m_StartMenu = m_Transform.GetChild(0).gameObject;
        m_ExplosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
    }

    public static void AddScore(int points)
    {
        score += points;
        m_ScoreText.UpdateScoreText();
    }

    public static void LoseLife()
    {
        lives -= 1;

        if (lives <= 0)
        {
            GameOver();
        }

        m_LivesText.UpdateLivesText();
        ResetCharge();
    }

    public static void IncreaseCharge()
    {
        if (charge < 3)
        {
            charge++;
            m_PlayerController.UpdateParticleSystem();
        }
    }

    public static void ResetCharge()
    {
        charge = 1;
        m_PlayerController.UpdateParticleSystem();
    }

    public static void SpawnExplosion(Vector3 position, float scale)
    {
        GameObject spawnedExplosion = Instantiate(m_ExplosionPrefab, position, Quaternion.identity) as GameObject;
        spawnedExplosion.transform.localScale = Vector3.one * scale;
        instance.StartCoroutine("CameraShake", scale);
    }

    public static void IncreaseDiffculty()
    {
        difficulty -= 1;
    }

    public static void GameOver()
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

    IEnumerator CameraShake(float amount)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 currentPosition = m_CameraTransform.position;
            Vector3 targetPosition = new Vector3(Random.Range(currentPosition.x - 0.25f * amount, currentPosition.x + 0.25f * amount),
                                                 Random.Range(currentPosition.y - 0.25f * amount, currentPosition.y + 0.25f * amount),
                                                 currentPosition.z);
            yield return MoveTo(currentPosition, targetPosition, 0.1f);
        }

        yield return MoveTo(m_CameraTransform.position, m_OriginalCameraPosition, 0.1f);
        m_CameraTransform.position = m_OriginalCameraPosition;
    }

    IEnumerator MoveTo(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            m_CameraTransform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
