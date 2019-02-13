using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject playerFirePrefab;

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
        GameObject spawnedFire = Instantiate(playerFirePrefab, m_Transform.position, Quaternion.identity) as GameObject;
        spawnedFire.transform.SetParent(m_Transform.parent);
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
