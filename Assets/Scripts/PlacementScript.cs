using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlacementScript : MonoBehaviour
{
    ParticleSystem _part;
    GameObject _tower;
    WarFunds war;
    bool _spotAv = true;
    bool _upgrade = false;
    BuildManager build;
    public static event Action<ITower> onUpgrade;
   
    private ITower _currentTower;
    //public int Towernum;
   
    UI_Manager ui;

    // Start is called before the first frame update
    void Start()
    {
        war = WarFunds.Instance;
        build = BuildManager.Instance;
        _part = GetComponent<ParticleSystem>();
        ui = GetComponent<UI_Manager>();

    }


    // Update is called once per frame

    private void OnMouseDown()
    {//only place if no tower here

        PickTower();

        if (war.CurrentWarFunds > _currentTower.UpgradeAmount)
        {   // 
            Debug.Log("trying to upgrade");
            //pop up upgrade button
            onUpgrade?.Invoke(_currentTower);
            


        }

       
            build.OffRadiusColor();

    }
    private void OnMouseEnter()
    {
        build.SnapTower(this.transform.position);


        build.UpdateRadiusColor();
        _part.Play(true);

        if (_spotAv == false)
        {
            build.OffRadiusColor();

        }
     

    }

    void PickTower()
    {
        if (_spotAv == true && build._towerDecoys[0].activeInHierarchy == true)
        {
            if (war.CurrentWarFunds > build._towersToPlace[build._currentTowerIndex].GetComponent<ITower>().WarFundsNeeded)
            {
                _currentTower = build._towersToPlace[build._currentTowerIndex].GetComponent<ITower>();

                build.PlaceTower(this.transform.position, _currentTower);
                war.CurrentWarFunds -= _currentTower.WarFundsNeeded;
                _spotAv = false;
                _upgrade = true;
                build.TowerOff();
                return;
            }
        }
        if (_spotAv == true && build._towerDecoys[1].activeInHierarchy == true)
        {
            if (war.CurrentWarFunds > build._towersToPlace[build._currentTowerIndex].GetComponent<ITower>().WarFundsNeeded)
            {
                _currentTower = build._towersToPlace[build._currentTowerIndex].GetComponent<ITower>();

                build.PlaceTower(this.transform.position, _currentTower);
                war.CurrentWarFunds -= _currentTower.WarFundsNeeded;
                _spotAv = false;
                _upgrade = true;
                build.TowerOff();
                return;
            }
        }
    }

    private void OnMouseExit()
    {
        _part.Stop(true);
        build.SnapTower(transform.position, true);
        // give control back to towermanager
        // turn radius red
        build.OffRadiusColor();

    }

   
    
     void OnEnable()
    {
        //UI_Manager.Upgrade1 += Upgrade;
    }

    void OnDisable()
    {
    //    UI_Manager.Upgrade1 -= Upgrade;
    }
}

