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
        StartCoroutine("SpawnAnimation");
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator SpawnAnimation()
    {
        yield return CameraShake();
        //yield return FreezeTime();
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPosition = m_CameraTransform.position;

        for (int i = 0; i < 3; i++)
        {
            Vector3 currentPosition = m_CameraTransform.position;
            Vector3 targetPosition = new Vector3(Random.Range(currentPosition.x - 0.25f, currentPosition.x + 0.25f),
                                                 Random.Range(currentPosition.y - 0.25f, currentPosition.y + 0.25f),
                                                 currentPosition.z);
            yield return MoveTo(currentPosition, targetPosition, 0.1f);
        }

        yield return MoveTo(m_CameraTransform.position, originalPosition, 0.1f);
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
