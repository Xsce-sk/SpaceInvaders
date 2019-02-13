using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = 0;
        m_Rigidbody2D.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ||
            collision.CompareTag("Shield"))
        {
            GameManager.SpawnExplosion(m_Transform.position, 1);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
