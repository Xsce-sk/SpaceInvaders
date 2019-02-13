using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject playerFirePrefab;
    public Sprite bulletSprite;
    public Sprite chargeBulletSprite;
    public Sprite superBulletSprite;

    private int m_Health;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;
    protected SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_Health = GameManager.charge;
        UpdateSprite();

        SpawnFire();

        m_Rigidbody2D.gravityScale = 0;
        m_Rigidbody2D.AddForce(Vector2.up * speed * m_Health, ForceMode2D.Impulse);
    }

    void SpawnFire()
    {
        Vector3 spawnPosition = new Vector3(m_Transform.position.x, m_Transform.position.y + (0.25f * m_Health), m_Transform.position.z);
        GameObject spawnedFire = Instantiate(playerFirePrefab, spawnPosition, Quaternion.identity) as GameObject;
        spawnedFire.transform.localScale = Vector3.one * m_Health;
        spawnedFire.transform.SetParent(m_Transform.parent);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Shield"))
        {
            GameManager.SpawnExplosion(m_Transform.position, 2);
            LoseHealth();
        }
    }

    void LoseHealth()
    {
        m_Health -= 1;

        if (m_Health <= 0)
        {
            Destroy(gameObject);
        }

        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (m_Health)
        {
            case 3:
                m_SpriteRenderer.sprite = superBulletSprite;
                break;
            case 2:
                m_SpriteRenderer.sprite = chargeBulletSprite;
                break;
            case 1:
                m_SpriteRenderer.sprite = bulletSprite;
                break;
            default:
                break;
        };
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
