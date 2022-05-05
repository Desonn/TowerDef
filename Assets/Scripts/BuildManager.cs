using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour

{
    private static BuildManager _instance;

    public static BuildManager Instance
      {
       
        get
        {

            

            if (_instance == null)
            {
                Debug.LogError("Build Manager is NULL");

            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    
  public  GameObject[] _towerDecoys;
   public GameObject[] _towersToPlace;
    public GameObject[] _upgradesToPlace;
    public  int _currentTowerIndex;// 0 = gat & 1 = missle
    public int _currentTowerDecoys;// 0 = gat & 1 = missle
    bool _placingTower;
 public   int towerNumber;
    ITower Funds;
    WarFunds war;
    public List<GameObject> ActiveTowers = new List<GameObject>();

    private void Start()
    {
        war = WarFunds.Instance;
    }
    private void Update()
    {
        if(_placingTower == true)
        {
            Ray rayorigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayorigin, out hitinfo))
            {
                _towerDecoys[_currentTowerIndex].transform.position = hitinfo.point;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _towerDecoys[_currentTowerIndex].SetActive(false);
        }
    }
    public void  SetTower(int tower)
    {
       
        _currentTowerIndex = tower;
        _towerDecoys[_currentTowerIndex].SetActive(true);
        _placingTower = true;
    }
    public void SnapTower(Vector3 pos, bool followmouse = false)
    {
        _towerDecoys[_currentTowerIndex].transform.position = pos;
        _placingTower = followmouse;
    }
    public void UpdateRadiusColor()
    {
        _towerDecoys[_currentTowerIndex].GetComponent<ParticleSystem>().startColor = Color.green;
    }
    public void OffRadiusColor()
    {
        _towerDecoys[_currentTowerIndex].GetComponent<ParticleSystem>().startColor = Color.red;
    }
    public void PlaceTower(Vector3 pos, ITower currentTower)
    {
      GameObject  obj = Instantiate(_towersToPlace[_currentTowerIndex], pos, Quaternion.identity);
        currentTower.CurrentTower = obj;
        currentTower.TowerPlace = pos;
       
        ActiveTowers.Add(obj);
       

    }

    public void TowerOff()
    {
        _towerDecoys[_currentTowerIndex].SetActive(false);
    }
   
}
