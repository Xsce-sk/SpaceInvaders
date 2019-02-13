using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite clickSprite;

    protected Button m_PlayButton;

    void Start()
    {
        m_PlayButton = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_PlayButton.image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_PlayButton.image.sprite = defaultSprite;
    }

    public void StartGame()
    {

    }
}
