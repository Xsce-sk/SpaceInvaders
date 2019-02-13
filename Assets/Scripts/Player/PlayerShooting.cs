using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [Serializable]
    public class ShootEvent : UnityEvent<PlayerShooting>
    { }

    public GameObject playerBullet;
    public ShootEvent OnShoot;

    private GameObject m_PreviousBullet;
    private Vector3 m_SpawnPosition;

    protected Transform m_Transform;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
    }

    void Update()
    {
        if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Mouse0)) && m_PreviousBullet == null) 
        {
            OnShoot.Invoke(this);

            GetSpawnPosition();
            SpawnBullet();
        }
    }

    void GetSpawnPosition()
    {
        Vector3 currentPosition = m_Transform.position;
        m_SpawnPosition = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
    }

    void SpawnBullet()
    {
        m_PreviousBullet = Instantiate(playerBullet, m_SpawnPosition, Quaternion.identity) as GameObject;
        m_PreviousBullet.transform.SetParent(m_Transform);
    }
}
