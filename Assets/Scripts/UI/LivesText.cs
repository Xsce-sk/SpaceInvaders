using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesText : MonoBehaviour
{
    protected TextMeshProUGUI m_TextMeshProUGUI;

    public void UpdateLivesText()
    {
        m_TextMeshProUGUI.text = GameManager.lives.ToString();
    }

    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
