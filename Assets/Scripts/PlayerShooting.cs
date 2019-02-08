using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBullet;
    public float waitTime = 0.5f;
    private float timeToShoot;

    void Awake()
    {
        timeToShoot = Time.time + waitTime;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + waitTime;
            Instantiate(playerBullet, this.transform.position, Quaternion.identity);
        }
    }
}
