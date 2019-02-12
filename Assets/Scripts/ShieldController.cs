using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Sprite HealthySprite;
    public Sprite BrokenGlassSprite;
    public Sprite BrokenWiresSprite;
    public Sprite DestroyedSprite;

    private int m_Health;

    protected SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_Health = 4;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        LoseHealth();
    }

    void LoseHealth()
    {
        m_Health -= 1;

        if (m_Health == 0)
        {
            Destroy(gameObject);
        }

        UpdateSprite();
    }

    void UpdateSprite()
    {
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
            default:
                break;
        }
    }
}
