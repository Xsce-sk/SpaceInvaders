using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(0, 1) * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Contains("Enemy") && !col.gameObject.name.Contains("Bullet"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);

            //addPoints and stuff
        }

        if (col.gameObject.name.Contains("Wall"))
            Destroy(this.gameObject);
    }
}
