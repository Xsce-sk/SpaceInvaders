using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public int enemyScore;
    public GameObject ragdoll;

    private GameObject m_Ragdoll;

    protected Transform m_Transform;
    protected Rigidbody2D m_RagdollRigidbody2D;

    void Start()
    {
        m_Transform = transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            PlayerUtilities.score += enemyScore;
            CreateRagdoll(collision.transform.position);
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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
