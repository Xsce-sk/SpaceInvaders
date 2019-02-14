using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    protected Transform m_Transform;

    public void StartGame()
    {
        foreach (ShieldHealth shield in GetComponentsInChildren<ShieldHealth>())
        {
            shield.ResetHealth();
        }

        m_Transform.position = Vector3.up * -33;
        Vector3 currentPosition = m_Transform.position;
        Vector3 targetPosition = new Vector3(0, -15, 0);
        StartCoroutine(MoveTo(currentPosition, targetPosition, 2));
    }

    void Start()
    {
        m_Transform = GetComponent<Transform>();

        m_Transform.position = Vector3.up * -33;
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
