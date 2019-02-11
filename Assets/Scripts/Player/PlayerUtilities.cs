using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    public static float score = 0f;
    public static float life = 3f;
    
    void Awake()
    {
        score = 0f;
        life = 3f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(life);
    }

    public void AddScore(float enemyScore)
    {
        score += enemyScore;
    }

    public void LoseLife()
    {
        life -= 1f;
    }
}
