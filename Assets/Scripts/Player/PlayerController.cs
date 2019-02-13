using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Material chargeBulletMaterial;
    public Material superBulletMaterial;

    protected Transform m_Transform;
    protected GameObject m_Ship;
    protected Transform m_ShipTransform;
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
        m_Ship = m_ShipTransform.gameObject;
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
        m_ParticleSystemRenderer = GetComponentInChildren<ParticleSystemRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ragdoll"))
        {
            GameManager.LoseLife();
        }
    }
}
