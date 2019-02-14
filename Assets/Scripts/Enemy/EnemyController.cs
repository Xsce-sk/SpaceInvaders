using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3;
    public float moveDownDuration = 1;
    public float defaultShootDelay = 3;
    public float delayShootRange = 1;
    public GameObject topEnemy;
    public GameObject middleEnemy;
    public GameObject bottomEnemy;
    public GameObject enemyBullet;

    private GameObject[,] m_EnemyGrid = new GameObject[5, 11];
    private int m_Direction = 1;
    private bool m_IsMovingDown = false;

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

    public void StartLevel()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Transform.position = Vector3.up * 30;
        FillEnemyGrid();
        
        Vector3 currentPosition = m_Transform.position;
        Vector3 targetPosition = new Vector3(0, 12, 0);
        StartCoroutine(MoveTo(currentPosition, targetPosition, 2));
        StartCoroutine(MoveRightAfterTime(2));
    }

    void Start()
    {
        m_Transform = transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_Rigidbody2D.gravityScale = 0;
        
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
        while (m_EnemyGrid != null)
        {
            float additionalDelay = Random.Range(0, delayShootRange);
            float delay = defaultShootDelay + additionalDelay;
            yield return new WaitForSeconds(delay);

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
        m_Rigidbody2D.AddForce(Vector3.right * m_Direction * moveSpeed, ForceMode2D.Impulse);

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
        yield return new WaitForSeconds(duration);
        m_Direction = 1;
        m_Rigidbody2D.AddForce(Vector3.right * m_Direction * moveSpeed, ForceMode2D.Impulse);
    }
}
