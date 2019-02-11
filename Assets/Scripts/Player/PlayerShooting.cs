using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject playerFire;

    private GameObject m_PreviousBullet;
    private GameObject m_PlayerFire;
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
            GetSpawnPosition();
            m_PreviousBullet = Instantiate(playerBullet, m_SpawnPosition, Quaternion.identity) as GameObject;
            m_PreviousBullet.transform.SetParent(m_Transform);

            m_PlayerFire = Instantiate(playerFire, m_SpawnPosition, Quaternion.identity) as GameObject;
            m_PlayerFire.transform.SetParent(m_Transform);
        }
    }

    void GetSpawnPosition()
    {
        Vector3 currentPosition = m_Transform.position;
        m_SpawnPosition = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
    }
}
