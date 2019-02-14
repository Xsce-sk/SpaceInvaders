using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    protected TextMeshProUGUI m_TextMeshProUGUI;

    public void UpdateScoreText()
    {
        m_TextMeshProUGUI.text = GameManager.score.ToString();
    }

    void Awake()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
