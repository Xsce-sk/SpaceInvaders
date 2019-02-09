using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBullet;
    private GameObject prevBullet;

    void Awake()
    {
        prevBullet = null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Mouse0)) && prevBullet == null) 
        {
            prevBullet = Instantiate(playerBullet, this.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
