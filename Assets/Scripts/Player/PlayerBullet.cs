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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Enemy") && !col.gameObject.name.Contains("Bullet"))
        {
            float enemyPoints = 40;

            if (col.gameObject.name.Contains("Bottom Enemy"))
                enemyPoints = 10;
            else if (col.gameObject.name.Contains("Middle Enemy"))
                enemyPoints = 20;

            PlayerUtilities.score += enemyPoints;

            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
