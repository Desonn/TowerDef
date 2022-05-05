using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    int WarFundsNeeded { get; set; }
    int UpgradeAmount { get; set; }
    GameObject UpgradeModel { get; }
    GameObject CurrentTower { get; set; }
    int TowerID { get; set; }// 0 = gat 1 = missile 2 = dual gat 3 = dual missile
    Vector3 TowerPlace { get; set; }



}