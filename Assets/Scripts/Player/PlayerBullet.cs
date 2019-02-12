using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;

    protected Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = 0;
        m_Rigidbody2D.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TopEnemy"))
        {
            PlayerUtilities.score += 40;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("MiddleEnemy"))
        {
            PlayerUtilities.score += 20;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("BottomEnemy"))
        {
            PlayerUtilities.score += 10;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
