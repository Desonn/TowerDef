using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ai : MonoBehaviour
{


    NavMeshAgent _agent;
    UI_Manager _uiManager;
    bool reset;


    // Start is called before the first frame update
    void Start()
    {
    
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("NavAgent is null");
        }
        _agent.SetDestination(SpawnManager.Instance.EndSpawn.position);
        //_agent.SetDestination(PoolManager.Instance.Endpoint.position);
       
        _uiManager = GameObject.Find("Canvas-UI").GetComponent<UI_Manager>();
    }





    public void ResetSetDestination()
         
    {
        reset = false;
        _agent.enabled = true;
        _agent.isStopped = false;
        _agent.SetDestination(SpawnManager.Instance.EndSpawn.position);
    }

    public void TurnOn()
    {
        if (this.gameObject.activeInHierarchy == false)
        {
            _agent.SetDestination(SpawnManager.Instance.EndSpawn.position);
            this.gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {

        if (_agent != null)
        {
            _agent.enabled = true;
            _agent.SetDestination(SpawnManager.Instance.EndSpawn.position);

            




            TurnOn();

                _agent.enabled = true;
            EnemyClass.NewPath += ResetSetDestination;
            //Mech2.NewPath2 += ResetSetDestination;

        }



    }
    private void OnDisable()
    {
        //Mech2.NewPath2 -= ResetSetDestination;
        EnemyClass.NewPath -= ResetSetDestination;
    }
}
