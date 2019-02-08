using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingManager : MonoBehaviour
{
    public float waitTime = 1f;
    private float timeToShoot;
    //public float shootChance = 0.5f;
    public List<GameObject> col0 = new List<GameObject>();
    public List<GameObject> col1 = new List<GameObject>();
    public List<GameObject> col2 = new List<GameObject>();
    public List<GameObject> col3 = new List<GameObject>();
    public List<GameObject> col4 = new List<GameObject>();
    public List<GameObject> col5 = new List<GameObject>();
    public List<GameObject> col6 = new List<GameObject>();
    public List<GameObject> col7 = new List<GameObject>();
    public List<GameObject> col8 = new List<GameObject>();
    public List<GameObject> col9 = new List<GameObject>();
    public List<GameObject> col10 = new List<GameObject>();
    public float minTime = 0.5f;
    public float maxTime = 2f;
    private List<List<GameObject>> columns = new List<List<GameObject>>();

    public GameObject enemyBullet;
    // Start is called before the first frame update
    void Awake()
    {
        waitTime = Random.Range(minTime, maxTime);
        timeToShoot = Time.time + waitTime;
        columns.Add(col0);
        columns.Add(col1);
        columns.Add(col2);
        columns.Add(col3);
        columns.Add(col4);
        columns.Add(col5);
        columns.Add(col6);
        columns.Add(col7);
        columns.Add(col8);
        columns.Add(col9);
        columns.Add(col10);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeToShoot && columns.Count > 0)
        {
            waitTime = Random.Range(minTime, maxTime);
            timeToShoot = Time.time + waitTime;

            UpdateLists(columns);

            if (columns.Count > 0)
            {
                int index = Random.Range(0, columns.Count);

                Instantiate(enemyBullet, columns[index][0].transform.position, Quaternion.identity);
            }
        }
    }

    void UpdateLists(List<List<GameObject>> cols)
    {
        for(int x = cols.Count - 1; x >= 0; --x)
        {
            for(int y = cols[x].Count - 1; y >= 0; --y)
            {
                if (cols[x][y] == null)
                    cols[x].RemoveAt(y);
            }

            if (cols[x].Count == 0)
                cols.RemoveAt(x);
        }
    }
}
