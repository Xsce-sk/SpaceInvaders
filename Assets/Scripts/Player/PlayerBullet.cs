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
        string colliderName = collision.gameObject.name;

        if (colliderName.Contains("Enemy") && !colliderName.Contains("Bullet"))
        {
            float enemyPoints = 40;

            if (colliderName.Contains("Bottom Enemy"))
            {
                enemyPoints = 10;
            }
            else if (colliderName.Contains("Middle Enemy"))
            {
                enemyPoints = 20;
            }

            PlayerUtilities.score += enemyPoints;

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (colliderName.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
