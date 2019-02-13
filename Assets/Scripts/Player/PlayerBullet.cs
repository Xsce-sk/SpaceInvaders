using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject playerFire;

    private GameObject m_PlayerFire;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = 0;
        m_Rigidbody2D.AddForce(Vector2.up * speed, ForceMode2D.Impulse);

        SpawnFire();
    }

    void SpawnFire()
    {
        m_PlayerFire = Instantiate(playerFire, m_Transform.position, Quaternion.identity) as GameObject;
        m_PlayerFire.transform.SetParent(m_Transform.parent);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
