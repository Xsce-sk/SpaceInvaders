using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    protected Transform m_CameraTransform;
    protected Transform m_Transform;

    void Start()
    {
        m_Transform = transform;
        m_CameraTransform = Camera.main.gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield") ||
            collision.CompareTag("Player"))
        {
            GameManager.SpawnExplosion(m_Transform.position, 4);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator FreezeTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f * 0.25f);

        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.02f;
            yield return new WaitForSeconds(0.01f * Time.timeScale);
        }
    }
}
