using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Material chargeBulletMaterial;
    public Material superBulletMaterial;

    private bool m_IsInvincible;

    protected Transform m_Transform;
    protected Transform m_ShipTransform;
    protected SpriteRenderer m_ShipSpriteRenderer;
    protected ParticleSystem m_ParticleSystem;
    protected ParticleSystemRenderer m_ParticleSystemRenderer;

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

        m_IsInvincible = false;
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
}
