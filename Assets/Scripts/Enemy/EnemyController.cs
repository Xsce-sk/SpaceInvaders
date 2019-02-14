using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float moveDownDuration = 1;
    public float defaultShootDelay = 10;
    public float delayShootRange = 1;
    public GameObject topEnemy;
    public GameObject middleEnemy;
    public GameObject bottomEnemy;
    public GameObject enemyBullet;

    private GameObject[,] m_EnemyGrid = new GameObject[5, 11];
    private int m_Direction = 1;
    private bool m_InitialSpawn = true;
    private bool m_IsMovingDown = false;
    private bool m_CanShoot = true;
    private float m_EnemyDifficulty = 0;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    public void StartShooting()
    {
        StartCoroutine("ShootLoop");
    }

    public void StopShooting()
    {
        StopCoroutine("ShootLoop");
    }

    public void StartGame()
    {
        m_InitialSpawn = false;
        
        m_Transform.position = Vector3.up * 30;
        ClearEnemyGrid();
        FillEnemyGrid();

        Vector3 currentPosition = m_Transform.position;
        Vector3 targetPosition = new Vector3(0, 12, 0);
        StartCoroutine(MoveTo(currentPosition, targetPosition, 2));
        StartCoroutine(MoveRightAfterTime(2));

        StartCoroutine("IncreaseDifficulty");
    }

    public void StartLevel()
    {
        StartCoroutine(FreezeTime());

        
        m_Transform.position = Vector3.up * 30;
        ClearEnemyGrid();
        FillEnemyGrid();
        
        Vector3 currentPosition = m_Transform.position;
        Vector3 targetPosition = new Vector3(0, 12 - GameManager.difficulty * 2, 0);
        StartCoroutine(MoveTo(currentPosition, targetPosition, 2));
        StartCoroutine(MoveRightAfterTime(2));
    }

    void Start()
    {
        m_Transform = transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_Rigidbody2D.gravityScale = 0;
    }

    void Update()
    {
        if (CheckEmpty() && !m_InitialSpawn)
        {
            GameManager.NewLevel();
        }
        print(m_EnemyDifficulty);
    }

    bool CheckEmpty()
    {
        bool isEmpty = true;

        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 11; column++)
            {
                if (m_EnemyGrid[row, column] != null)
                    isEmpty = false;
            }
        }

        return isEmpty;
    }

    void FillEnemyGrid()
    {
        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 11; column++)
            {
                GameObject enemyType = GetEnemyType(row);
                Vector2 enemyPosition = new Vector2(m_Transform.position.x + - 10 + (column * 2), m_Transform.position.y + 6 - (row * 2));
                GameObject spawnedEnemy = Instantiate(enemyType, enemyPosition, Quaternion.identity) as GameObject;
                m_EnemyGrid[row, column] = spawnedEnemy;
                spawnedEnemy.transform.SetParent(m_Transform);
            }
        }
    }

    void ClearEnemyGrid()
    {
        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 11; column++)
            {
                m_EnemyGrid[row, column] = null;
            }
        }
    }

    GameObject GetEnemyType(int row)
    {
        GameObject enemyType = null;

        switch (row)
        {
            case 0:
                enemyType = topEnemy;
                break;
            case 1:
            case 2:
                enemyType = middleEnemy;
                break;
            case 3:
            case 4:
                enemyType = bottomEnemy;
                break;
            default:
                enemyType = null;
                break;
        }

        return enemyType;
    }

    IEnumerator ShootLoop()
    {
        while (!CheckEmpty())
        {
            float additionalDelay = Random.Range(0, delayShootRange);
            float delay = Mathf.Clamp(defaultShootDelay - m_EnemyDifficulty, 0, defaultShootDelay)  + additionalDelay;
            yield return new WaitForSeconds(delay);

            if (m_CanShoot)
                Shoot();
        }
    }

    void Shoot()
    {
        int shootColumn = GetShootColumn();
        Vector3 shootPosition = GetShootPosition(shootColumn);
        shootPosition = new Vector3(shootPosition.x, shootPosition.y - 0.5f, shootPosition.z);
        GameObject bullet = Instantiate(enemyBullet, shootPosition, Quaternion.identity) as GameObject;
        bullet.transform.SetParent(m_Transform);
    }

    int GetShootColumn()
    {
        int column = 0;

        bool isEmptyColumn = true;
        while (isEmptyColumn)
        {
            column = Random.Range(0, 11);
            for (int row = 0; row < 5; row++)
            {
                if (m_EnemyGrid[row, column] != null)
                {
                    isEmptyColumn = false;
                }
            }
        }

        return column;
    }

    Vector3 GetShootPosition(int column)
    {
        Vector3 position = Vector3.zero;

        int lowestRow = 0;
        for (int row = 0; row < 5; row++)
        {
            if (m_EnemyGrid[row, column] != null)
            {
                lowestRow = row;
            }
        }

        return m_EnemyGrid[lowestRow, column].transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_IsMovingDown)
        {
            StartCoroutine("MoveDown");
        }

    }

    IEnumerator MoveDown()
    {
        m_IsMovingDown = true;

        Vector3 currentPosition = m_Transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y - 2, currentPosition.z);

        yield return MoveTo(currentPosition, targetPosition, moveDownDuration);

        m_Direction *= -1;
        m_Rigidbody2D.AddForce(Vector3.right * m_Direction * (moveSpeed + m_EnemyDifficulty), ForceMode2D.Impulse);

        m_IsMovingDown = false;
    }

    IEnumerator MoveTo(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            m_Transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveRightAfterTime(float duration)
    {
        m_CanShoot = false;

        yield return new WaitForSeconds(duration);
        m_CanShoot = true;

        m_Rigidbody2D.velocity = Vector2.zero;
        m_EnemyDifficulty = GameManager.difficulty - 1;

        m_Direction = 1;
        m_Rigidbody2D.AddForce(Vector3.right * m_Direction * (moveSpeed + m_EnemyDifficulty), ForceMode2D.Impulse);
    }

    IEnumerator FreezeTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f * 1f);

        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.02f;
            yield return new WaitForSeconds(0.01f * Time.timeScale);
        }
    }

    IEnumerator IncreaseDifficulty()
    {
        while(true)
        {
            m_EnemyDifficulty += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
