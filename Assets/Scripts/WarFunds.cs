using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarFunds : MonoBehaviour
{
    
    private static WarFunds _instance;

    public static WarFunds Instance
    {

        get
        {



            if (_instance == null)
            {
                Debug.LogError("Money Manager is NULL");

            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }



    public  int CurrentWarFunds;
    public int StartAmount;
    public int Cost;

    private void Start()
    {
        CurrentWarFunds = StartAmount;
    }

  

    private void Update()
    {
        if(CurrentWarFunds == 0)
        {
            CurrentWarFunds = 0;
        }
    }

}
