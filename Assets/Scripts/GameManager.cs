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

    private static Vector3 m_OriginalCameraPosition;
    private static bool m_CanIncreaseCharge;
    private static bool m_CanPause;
    private static GameObject m_StartMenu;
    private static GameObject m_GameOverMenu;
    private static GameObject m_PauseMenu;
    private static GameObject m_InGameUI;
    private static GameObject m_ExplosionPrefab;
    
    protected static Transform m_Transform;
    protected static Transform m_CameraTransform;
    protected static Transform m_PlayerTransform;
    protected static Transform m_EnemiesTransform;
    protected static Transform m_ShieldsTransform;
    protected static Transform m_InGameUITransform;

    protected static PlayerController m_PlayerController;
    protected static EnemyController m_EnemyController;
    protected static ShieldController m_ShieldController;
    protected static LivesText m_LivesText;
    protected static ScoreText m_ScoreText;
    protected static LevelText m_LevelText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_Transform = transform;
        m_CameraTransform = Camera.main.transform;
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_EnemiesTransform = GameObject.FindGameObjectWithTag("Enemies").transform;
        m_ShieldsTransform = GameObject.FindGameObjectWithTag("Shields").transform;
        m_InGameUITransform = m_Transform.GetChild(3);

        m_PlayerController = m_PlayerTransform.gameObject.GetComponent<PlayerController>();
        m_EnemyController = m_EnemiesTransform.gameObject.GetComponent<EnemyController>();
        m_ShieldController = m_ShieldsTransform.gameObject.GetComponent<ShieldController>();
        m_LivesText = GetComponentInChildren<LivesText>();
        m_ScoreText = GetComponentInChildren<ScoreText>();
        m_LevelText = GetComponentInChildren<LevelText>();
        
        m_OriginalCameraPosition = m_CameraTransform.position;
        m_StartMenu = m_Transform.GetChild(0).gameObject;
        m_GameOverMenu = m_Transform.GetChild(1).gameObject;
        m_PauseMenu = m_Transform.GetChild(2).gameObject;
        m_InGameUI = m_Transform.GetChild(3).gameObject;
        m_ExplosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");

        m_CanPause = false;
        DisableInGameUI();
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

    public static void EnableIncreaseCharge()
    {
        m_CanIncreaseCharge = true;
    }

    public static void DisableIncreaseCharge()
    {
        m_CanIncreaseCharge = false;
    }

    public static void IncreaseCharge()
    {
        if (charge < 3 && m_CanIncreaseCharge)
        {
            charge++;
            m_PlayerController.UpdateParticleSystem();
        }
    }

    public static void ResetCharge()
    {
        charge = 1;
        m_PlayerController.UpdateParticleSystem();
        DisableIncreaseCharge();
    }

    public static void SpawnExplosion(Vector3 position, float scale)
    {
        GameObject spawnedExplosion = Instantiate(m_ExplosionPrefab, position, Quaternion.identity) as GameObject;
        spawnedExplosion.transform.localScale = Vector3.one * scale;
        instance.StartCoroutine("CameraShake", scale);
    }

    public static void IncreaseDiffculty()
    {
        difficulty += 1;
        m_LevelText.UpdateLevelText();
    }

    public static void GameOver()
    {
        m_GameOverMenu.SetActive(true);
        m_GameOverMenu.GetComponentInChildren<ScoreText>().UpdateScoreText();

        instance.DisableInGameUI();

        Time.timeScale = 0;
        m_EnemyController.StopShooting();
        m_CanPause = false;
    }

    public void StartGame()
    {
        m_StartMenu.SetActive(false);
        m_GameOverMenu.SetActive(false);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
        {
            Destroy(bullet);
        }

        foreach (GameObject ragdoll in GameObject.FindGameObjectsWithTag("Ragdoll"))
        {
            Destroy(ragdoll);
        }

        Time.timeScale = 1;
        ResetCharge();
        score = 0;
        lives = 3;
        difficulty = 1;

        m_LivesText.UpdateLivesText();
        m_ScoreText.UpdateScoreText();
        m_LevelText.UpdateLevelText();

        m_PlayerController.StartGame();
        m_ShieldController.StartGame();
        m_EnemyController.StartGame();
        m_EnemyController.StartShooting();
        EnableInGameUI();
        m_CanPause = true;
    }

    public static void NewLevel()
    {
        instance.DisableInGameUI();
        IncreaseDiffculty();
        lives++;

        m_LivesText.UpdateLivesText();

        m_EnemyController.StartLevel();
        m_EnemyController.StopShooting();
        m_EnemyController.StartShooting();
        instance.EnableInGameUI();
        m_CanPause = true;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        m_PauseMenu.SetActive(true);
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        m_PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_CanPause)
        {
            if (!m_PauseMenu.activeInHierarchy)
            {
                PauseGame();
            }
            else if (m_PauseMenu.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    void DisableInGameUI()
    {
        m_InGameUITransform.localScale = Vector3.zero;
    }

    void EnableInGameUI()
    {
        instance.StartCoroutine("EnableInGameUIOverTime");
    }

    IEnumerator CameraShake(float amount)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 currentPosition = m_CameraTransform.position;
            Vector3 targetPosition = new Vector3(Random.Range(currentPosition.x - 0.1f * amount, currentPosition.x + 0.1f * amount),
                                                 Random.Range(currentPosition.y - 0.1f * amount, currentPosition.y + 0.1f * amount),
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

    IEnumerator EnableInGameUIOverTime()
    {
        yield return new WaitForSeconds(2);
        m_InGameUITransform.localScale = Vector3.one;
    }
}