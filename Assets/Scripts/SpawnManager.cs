using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    [SerializeField]
    Transform SpawnPoint, Endpoint;
    [SerializeField]
    GameObject[] Enemies;
    [SerializeField]
     GameObject EnemyContainer;
    [SerializeField]
    int EnemyCount;
    public int enemiesStillActive;
  public static  int EnemyAlive;
    [SerializeField]
    int amountSpawned;
  public int wave,maxWaves;
   // public int Killed;
    public int SpawnDelay;
    WaitForSeconds startYeild;
    WaitForSeconds spawnYeild;
    float startDelay = 3;
    bool wavesDone;
    UI_Manager ui;
    [SerializeField]
    int enemiesSpawned;
    public List<GameObject> objectsCreated = new List<GameObject>();
    public static SpawnManager Instance
      {
       
        get
        {

            

            if (_instance == null)
            {
                Debug.LogError("Spawn Manager is NULL");

            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        
    }
    private void Start()

    {
        startYeild = new WaitForSeconds(startDelay);
        spawnYeild = new WaitForSeconds(SpawnDelay);
        MakeEnemies();
        StartCoroutine(SpawnRoute());
        // StartWave();
        ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();

    }
    public Transform spawnPos
    {
        get
        {
            return SpawnPoint;
        }
    }
    public Transform EndSpawn
    {
        get
            {
            return Endpoint;
        }
    }


    public void MakeEnemies()
    {
        
        
        for(int i =0; i < 10; i++)
        {
            var spawns = Enemies[Random.Range(0, Enemies.Length)];
            var enemyInstance = Instantiate(spawns, SpawnPoint.position, Quaternion.identity, EnemyContainer.transform);
            //var em =  Instantiate(spawns, SpawnPoint.position, Quaternion.identity,EnemyContainer.transform);
            objectsCreated.Add(enemyInstance);
            spawns.SetActive(false);
          

        }
        //if (Killed >= EnemyCount)
        //{
        //    NextWave();
        //}
    }
    IEnumerator SpawnRoute()
    {
        EnemyCount = 0;
        amountSpawned = 10 * wave;
        while (wavesDone == false)
        {
            if (wave > maxWaves)
                wavesDone = true;
            yield return startYeild;
            var emeny = objectsCreated.FirstOrDefault((em) => em.activeInHierarchy == false);
          
            if (enemiesSpawned < amountSpawned)
            {
                enemiesSpawned++;
                enemiesStillActive++;
                emeny.SetActive(true);
                EnemyAlive++;
                EnemyCount++;

            }

            

            
         
               

             
               
            
            if(enemiesSpawned == amountSpawned && enemiesStillActive <1)
            {
                wave++;
                ui.UpdateDisplay();
                EnemyCount = 0;
                enemiesSpawned = 0;
                amountSpawned = 10 * wave;
                var spawns = Enemies[Random.Range(0, Enemies.Length)];
                var enemyInstance = Instantiate(spawns, SpawnPoint.position, Quaternion.identity, EnemyContainer.transform);
                if(amountSpawned < objectsCreated.Count)
                {
                    for (int i = 0; i < amountSpawned; i++)
                        objectsCreated.Add(enemyInstance);

                }

            }
            yield return spawnYeild;
        }
    

    }
    public void TestWave(SpawnerWaves wave)
    {
        //clear the current object list 
        //populate with editor data
        objectsCreated.Clear();
        var children = EnemyContainer.GetComponentsInChildren<Transform>(true);
        for(int i = 0; i< children.Length; i++)
        {
            if (children[i].name == "EnemyContainer")
                continue;
            Destroy(children[i].gameObject);
        }
        for( int i=0; i < wave.wavesSequence.Count; i++)
        {
            var enemyInstance = Instantiate(wave.wavesSequence[i], SpawnPoint.position, Quaternion.identity, EnemyContainer.transform);

            objectsCreated.Add(enemyInstance);
            enemyInstance.SetActive(false);

        }

    }
   
     
    }
    
