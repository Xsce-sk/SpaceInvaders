using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class ShootEvent : UnityEvent<PlayerController>
    { }

    public float moveSpeed = 30;
    public float tiltAmount = 3;
    public GameObject playerBullet;
    public ShootEvent OnShoot;
    public Material chargeBulletMaterial;
    public Material superBulletMaterial;

    private bool m_IsInvincible;
    private bool m_CanShoot;
    private bool m_CanMove;
    private float m_HorizontalAxis;
    private GameObject m_PreviousBullet;
    private Vector3 m_SpawnPosition;

    protected Transform m_Transform;
    protected Transform m_ShipTransform;
    protected SpriteRenderer m_ShipSpriteRenderer;
    protected ParticleSystem m_ParticleSystem;
    protected ParticleSystemRenderer m_ParticleSystemRenderer;
    protected Rigidbody2D m_RigidBody2D;

    public void StartGame()
    {
        m_Transform.position = Vector3.up * -38.5f;
        Vector3 targetPosition = new Vector3(0, -20.5f, -5);
        StartCoroutine(MoveToThenAllowControl(m_Transform.position, targetPosition, 2));
    }

    public void UpdateParticleSystem()
    {
        switch (GameManager.charge)
        {
            case 1:
                m_ParticleSystem.Stop();
                break;
            case 2:
                m_ParticleSystem.Play();
                m_ParticleSystem.startSize = 0.5f;
                m_ParticleSystemRenderer.material = chargeBulletMaterial;
                break;
            case 3:
                m_ParticleSystem.Play();
                m_ParticleSystem.startSize = 1;
                m_ParticleSystemRenderer.material = superBulletMaterial;
                break;
            default:
                break;
        };
    }

    void Start()
    {
        m_Transform = transform;
        m_ShipTransform = m_Transform.GetChild(0);
        m_ShipSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
        m_ParticleSystemRenderer = GetComponentInChildren<ParticleSystemRenderer>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();

        m_Transform.position = Vector3.up * -38.5f;

        m_IsInvincible = false;
        m_CanMove = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ragdoll") ||
            collision.CompareTag("EnemyBullet"))
        {
            if (!m_IsInvincible)
            {
                GameManager.LoseLife();
                StartCoroutine("InvincibleFrames");
            }
        }
    }

    IEnumerator InvincibleFrames()
    {
        m_IsInvincible = true;

        for (int i = 0; i < 5; i++)
        {
            m_ShipSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            m_ShipSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        m_IsInvincible = false;
    }

    IEnumerator MoveToThenAllowControl(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            m_Transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_CanShoot = true;
        m_CanMove = true;
    }

    void Update()
    {
        m_HorizontalAxis = Input.GetAxis("Horizontal");

        if (((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Mouse0))) && m_CanShoot)
        {
            if (!m_PreviousBullet)
            {
                OnShoot.Invoke(this);

                GetSpawnPosition();
                SpawnBullet();
                GameManager.EnableIncreaseCharge();
            }
            else
            {
                GameManager.ResetCharge();
            }
        }
    }

    void FixedUpdate()
    {
        if (m_CanMove)
        {
            Move();
            Tilt();
        }
    }

    void Move()
    {
        m_RigidBody2D.velocity = new Vector2(m_HorizontalAxis * moveSpeed, 0);
    }

    void Tilt()
    {
        m_ShipTransform.eulerAngles = new Vector3(0, 0, -m_HorizontalAxis * tiltAmount);
    }

    void GetSpawnPosition()
    {
        Vector3 currentPosition = m_Transform.position;
        m_SpawnPosition = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
    }

    void SpawnBullet()
    {
        m_PreviousBullet = Instantiate(playerBullet, m_SpawnPosition, Quaternion.identity) as GameObject;
        m_PreviousBullet.transform.SetParent(m_Transform);
    }
}
