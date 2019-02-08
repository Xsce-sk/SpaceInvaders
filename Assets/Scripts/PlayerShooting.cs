using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBullet;
    public float waitTime = 0.5f;
    private float timeToShoot;
    private GameObject prevBullet;

    void Awake()
    {
        timeToShoot = Time.time + waitTime;
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
            timeToShoot = Time.time + waitTime;
            prevBullet = Instantiate(playerBullet, this.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
