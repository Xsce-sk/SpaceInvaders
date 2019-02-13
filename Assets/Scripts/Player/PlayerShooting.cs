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

    public GameObject regularBullet;
    public GameObject chargeBullet;
    public GameObject superBullet;
    public ShootEvent OnShoot;

    private GameObject m_PreviousBullet;
    private Vector3 m_SpawnPosition;
    private int m_Charge;

    protected Transform m_Transform;

    public void increaseCharge()
    {
        if (m_Charge < 3)
        {
            m_Charge++;
        }
    }

    public void resetCharge()
    {
        m_Charge = 0;
    }

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
        m_PreviousBullet = Instantiate(regularBullet, m_SpawnPosition, Quaternion.identity) as GameObject;
        m_PreviousBullet.transform.SetParent(m_Transform);
    }
}
