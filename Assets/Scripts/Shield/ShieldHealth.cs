using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public Sprite HealthySprite;
    public Sprite BrokenGlassSprite;
    public Sprite BrokenWiresSprite;
    public Sprite DestroyedSprite;

    private int m_Health;

    protected SpriteRenderer m_SpriteRenderer;

    public void ResetHealth()
    {
        m_Health = 4;
        UpdateSprite();
    }

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        ResetHealth();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        LoseHealth();
    }

    void LoseHealth()
    {
        if (m_Health > 0)
        {
            m_Health -= 1;
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        tag = "Shield";
        switch (m_Health)
        {
            case 4:
                m_SpriteRenderer.sprite = HealthySprite;
                break;
            case 3:
                m_SpriteRenderer.sprite = BrokenGlassSprite;
                break;
            case 2:
                m_SpriteRenderer.sprite = BrokenWiresSprite;
                break;
            case 1:
                m_SpriteRenderer.sprite = DestroyedSprite;
                break;
            case 0:
                m_SpriteRenderer.sprite = null;
                tag = "Untagged";
                break;
            default:
                break;
        }
    }
}
