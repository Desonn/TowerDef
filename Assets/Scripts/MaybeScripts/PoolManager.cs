using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Transform SpawnPoint, Endpoint;
    public List<GameObject> Enemy = new List<GameObject>();
    public GameObject EnemyContainer;
    private static PoolManager _instance;
    public static PoolManager Instance
    {

        get
        {
            if (_instance == null)
            {
                Debug.LogError("Pool is Null");

            }
            return _instance;
        }



    }
     private void Start()

    {
        MakeEnemies();
        

    }
    public void MakeEnemies()
    {


        for (int i = 0; i < 10; i++)
        {
            var spawns = Enemy[Random.Range(0, Enemy.Count)];

            Instantiate(spawns, SpawnPoint.position, Quaternion.identity, EnemyContainer.transform);

            spawns.SetActive(false);
        }
    }
    private void Awake()
    {
        _instance = this;
    }
   
}

