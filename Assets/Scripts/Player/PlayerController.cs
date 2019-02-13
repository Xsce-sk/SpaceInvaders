using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Transform m_Transform;
    protected GameObject m_Ship;
    protected Transform m_ShipTransform;

    void Start()
    {
        m_Transform = transform;
        m_ShipTransform = m_Transform.GetChild(0);
        m_Ship = m_ShipTransform.gameObject;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ragdoll"))
        {
            GameManager.LoseLife();
        }
    }
}
