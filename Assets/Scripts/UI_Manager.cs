using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    Text _wartext;
    [SerializeField]
    Text _countText; 
    [SerializeField]
    Text _livestext;
    [SerializeField]
    Text _statustext;
    float countdownTime=5;
    [SerializeField]
    int _lives;
    BuildManager build;
    [SerializeField]
    WarFunds war;
    [SerializeField]
    GameObject upgradeGat;
    [SerializeField]
    GameObject upgradeMissile;
    [SerializeField]
    GameObject sell;
    PlacementScript placenum;
    ITower _upGradeTower;
    private ITower _currentTower;
    public static Action Upgrade1;
    [SerializeField]
    private Sprite[] normal;
    [SerializeField]
    private Sprite[] caution;
    [SerializeField]
    private Sprite[] warning;
    [SerializeField]
    Sprite[] Gat;
    [SerializeField]
    Sprite[] mis;
    [SerializeField]
    Image armory;
    [SerializeField]
    Image warfunds;
    [SerializeField]
    Image restart;
    [SerializeField]
    Image playback;
    [SerializeField]
    Image liveswave;
    [SerializeField]
    Image gatImg;
    [SerializeField]
    Image misImg;
    WaitForSeconds menuYeild;
    [SerializeField] Text waveTxt;
    // Start is called before the first frame update
    void Start()
    {
        StartCountdown();
        _upGradeTower = GetComponent<ITower>();
        war = WarFunds.Instance;
        _wartext.text = "$" + war.CurrentWarFunds;
        build = BuildManager.Instance;
        placenum = GetComponent<PlacementScript>();
        menuYeild = new WaitForSeconds(5);
        waveTxt.text = SpawnManager.Instance.wave + "/" + SpawnManager.Instance.maxWaves;
    }

    // Update is called once per frame
    void Update()
    {
        _wartext.text = "" + war.CurrentWarFunds;
        _livestext.text = _lives.ToString();
    }
    void StartCountdown()
    {
        StartCoroutine(CountDownToStart());
    }

    public void GatlingGun()
    {// check warfunds amount
        if (war.CurrentWarFunds < 50)
        {
            gatImg.sprite = Gat[1];
            return;

        }
        else
        {
            gatImg.sprite = Gat[0];
            build.SetTower(0);
        }
     }
    public void Missle()
    {
       

        if (war.CurrentWarFunds < 100)
        {
            misImg.sprite = mis[1];
            return;
        }
        else
        {
            misImg.sprite = mis[0];
            // check warfunds amount
            build.SetTower(1);


        }
    }
    public void UpgradeUI(ITower currentTower)
    {
        _upGradeTower = currentTower;

        if (build._currentTowerIndex == 0)
            StartCoroutine(UnHideGat());
        //if(build._currentTowerIndex == 1) 
        //{ 
            
        //StartCoroutine(UnHideMissile());
        //}

    }
   
    public IEnumerator UnHideGat()
    {

       
        {

            upgradeGat.SetActive(true);
            sell.SetActive(true);
            yield return menuYeild;
            upgradeGat.SetActive(false);
            sell.SetActive(false);
        }
    }
    public IEnumerator UnHideMissile()
    {
        

        {

            upgradeMissile.SetActive(true);
            sell.SetActive(true);
            yield return menuYeild;
            upgradeMissile.SetActive(false);
            sell.SetActive(false);
        }
    }
    IEnumerator CountDownToStart()
    {
        WaitForSeconds startyeild = new WaitForSeconds(0.5f);
        while(countdownTime > 0)
        {
          
            _countText.text = countdownTime.ToString();
            yield return startyeild;
            countdownTime--;
            
        }
        _countText.gameObject.SetActive(false);
    }

    public void UpdateDisplay()
    {
        waveTxt.text = SpawnManager.Instance.wave + "/" + SpawnManager.Instance.maxWaves;
    }
    public void YesButton()
    {

        if (_upGradeTower.CurrentTower == null)
            Debug.Log("tower is null");
        
            DestroyImmediate(_upGradeTower.CurrentTower, true);
         
         GameObject _newTower = Instantiate(_upGradeTower.UpgradeModel, _upGradeTower.TowerPlace, Quaternion.identity);
        //_upGradeTower = _newTower.GetComponent<ITower>();
        //_upGradeTower.CurrentTower = _newTower;

    }

    public void SellButton()
    {

        if (_upGradeTower.CurrentTower == null)
            Debug.Log("tower is null");
        _upGradeTower.WarFundsNeeded += war.CurrentWarFunds;
        DestroyImmediate(_upGradeTower.CurrentTower, true);
       
       

    }
    public void LivesLeft()
    {
        _lives--;
        if(_lives <= 60 && _lives >30)
        {
            _statustext.text = "Good";
            armory.sprite = normal[0];
            liveswave.sprite = normal[1];
            playback.sprite = normal[2];
            restart.sprite = normal[3];
            warfunds.sprite = normal[4];
        }
        if (_lives <= 30 && _lives > 10)
        {
            _statustext.text = "Caution";
            armory.sprite = caution[0];
            liveswave.sprite = caution[1];
            playback.sprite = caution[2];
            restart.sprite = caution[3];
            warfunds.sprite = caution[4];
        }
        if (_lives <= 10)
        {
            _statustext.text = "Danger";
            armory.sprite = warning[0];
            liveswave.sprite = warning[1];
            playback.sprite = warning[2];
            restart.sprite = warning[3];
            warfunds.sprite = warning[4];
        }

        if (_lives < 0)
            _lives = 0;
    }
    private void OnEnable()
    {
        PlacementScript.onUpgrade += UpgradeUI;
        EndZone.LivesLost += LivesLeft;
        GameManager.Restart += StartCountdown; 
    }
    private void OnDisable()
    {
        PlacementScript.onUpgrade -= UpgradeUI;
        EndZone.LivesLost -= LivesLeft;
        GameManager.Restart -= StartCountdown;
    }
    public void UpgradeWeapon1()
    {
        
        Upgrade1?.Invoke();
    }
}