using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float moveRange;

    protected Transform m_Transform;

    void Start()
    {
        m_Transform = transform;

        m_Transform.position = spawnPosition;
        
        StartCoroutine(RandomFly());
    }

    private void Update()
    {
        if (m_Transform.position.x < -20 || m_Transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            GameManager.AddScore(Random.Range(1,4) * 50 + 100);
            GameManager.SpawnExplosion(m_Transform.position, 2);
            Destroy(gameObject);
        }
    }

    IEnumerator RandomFly()
    {
        while (true)
        {
            Vector3 currentPosition = m_Transform.position;
            Vector3 targetPosition = new Vector3(currentPosition.x + Random.Range(0.5f, 1) * moveRange, Random.Range(spawnPosition.y - 3, spawnPosition.y + 3), currentPosition.z);
            yield return MoveTo(currentPosition, targetPosition, Random.Range(0.3f, 0.6f));
        }
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
}
