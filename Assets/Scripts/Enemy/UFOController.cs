using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    public Vector3 spawnPosition;

    protected Transform m_Transform;

    void Start()
    {
        m_Transform = transform;

        m_Transform.position = spawnPosition;
    }

    private void Update()
    {
        if (m_Transform.position.x < -19 || m_Transform.position.x > 19)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            GameManager.AddScore(Random.Range(1,4) * 50 + 100);
            Destroy(gameObject);
        }
    }

    void CreateRagdoll(Vector3 collisionPosition)
    {
        m_Ragdoll = Instantiate(ragdoll, m_Transform.position, Quaternion.identity) as GameObject;
        float clampedXDirection = Mathf.Clamp(m_Transform.position.x - collisionPosition.x, -0.3f, 0.3f);
        Vector3 direction = new Vector3(clampedXDirection, 1, 0);

        m_RagdollRigidbody2D = m_Ragdoll.GetComponent<Rigidbody2D>();
        m_RagdollRigidbody2D.AddForce(direction * 300);
        m_RagdollRigidbody2D.AddTorque(-direction.x * 1000);
    }
}
