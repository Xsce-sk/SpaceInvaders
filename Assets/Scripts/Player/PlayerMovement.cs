using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 30;
    public float tiltAmount = 3;
    public GameObject shipSprite;

    float m_HorizontalAxis;

    protected Rigidbody2D m_RigidBody2D; 

    void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        m_HorizontalAxis = Input.GetAxis("Horizontal");

        Move();
        Tilt();
    }

    void Move()
    {
        m_RigidBody2D.velocity = new Vector2(m_HorizontalAxis * moveSpeed, 0);
    }

    void Tilt()
    {
        shipSprite.transform.eulerAngles = new Vector3(0, 0, -m_HorizontalAxis * tiltAmount);
    }
}
