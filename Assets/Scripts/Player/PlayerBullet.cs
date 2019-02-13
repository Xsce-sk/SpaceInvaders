using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public int bulletCharge;
    public GameObject playerFirePrefab;
    public Sprite bulletSprite;
    public Sprite chargeBulletSprite;
    public Sprite superBulletSprite;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;
    protected SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_Rigidbody2D.gravityScale = 0;
        m_Rigidbody2D.AddForce(Vector2.up * speed, ForceMode2D.Impulse);

        SpawnFire();
    }

    void SpawnFire()
    {
        GameObject spawnedFire = Instantiate(playerFirePrefab, m_Transform.position, Quaternion.identity) as GameObject;
        spawnedFire.transform.localScale = Vector3.one * charge;
        spawnedFire.transform.SetParent(m_Transform.parent);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Shield"))
        {
            LoseCharge()
        }
    }

    void LoseCharge()
    {
        bulletCharge -= 1;

        if (bulletCharge <= 1)
        {
            Destroy(gameObject);
        }

        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (bulletCharge)
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
