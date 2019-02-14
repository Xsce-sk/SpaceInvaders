using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    protected TextMeshProUGUI m_TextMeshProUGUI;

    public void UpdateLevelText()
    {
        m_TextMeshProUGUI.text = GameManager.difficulty.ToString();
    }

    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
