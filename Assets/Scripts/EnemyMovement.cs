using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //private static float enemyMovementMod = 1f;
    [SerializeField] private Transform trans;
    [SerializeField] private static float enemyMovementMod = 1f;
    public float speed = 10;
    public float downTimer = 10;
    public Rigidbody2D rb2d;
    //private static bool moveDown = false;
    private static Vector2 hVelocity;
    private static Vector2 vVelocity;

    void Awake()
    {
        enemyMovementMod = 1f;
        trans = this.transform;
        rb2d = GetComponent<Rigidbody2D>();
        hVelocity = new Vector2(1, 0) * enemyMovementMod * speed;
        vVelocity = new Vector2(0, 0);
    }

    void Start()
    {

    }


    void Update()
    {
        /*enemyManager.transform.position += new Vector3(1, 0, 0) * enemyMovementMod * Time.deltaTime;
        if(moveDown)
        {
            trans.position -= new Vector3(0, 1, 0) * indent;
        }*/

        rb2d.velocity = hVelocity + vVelocity;
    }

    public void ChangeDirection()
    {
        enemyMovementMod *= -1f;
        hVelocity = new Vector3(1, 0, 0) * enemyMovementMod * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("A");
        if (col.gameObject.name.Contains("Wall"))
        {
            Debug.Log("B");
            ChangeDirection();
            StartCoroutine("GoDown");
        }
    }

    IEnumerator GoDown()
    {
        vVelocity = new Vector2(0, -1) * speed;
        Vector2 temp = hVelocity;
        hVelocity = Vector2.zero;
        yield return new WaitForSeconds(downTimer * Time.deltaTime);
        hVelocity = temp;
        vVelocity = new Vector2(0, 0);
    }
}
