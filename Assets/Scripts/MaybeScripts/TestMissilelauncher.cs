using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.FileBase.Missle_Launcher.Missle;

public class TestMissilelauncher : MonoBehaviour, ITower
{
  BuildManager build;
  [SerializeField]
 private GameObject _missilePrefab; //holds the missle gameobject to clone
[SerializeField]
private GameObject[] _misslePositions; //array to hold the rocket positions on the turret
[SerializeField]
private float _fireDelay; //fire delay between rockets
[SerializeField]
private float _launchSpeed; //initial launch speed of the rocket
[SerializeField]
private float _power; //power to apply to the force of the rocket
[SerializeField]
private float _fuseDelay; //fuse delay before the rocket launches
[SerializeField]
private float _reloadTime; //time in between reloading the rockets
[SerializeField]
private float _destroyTime = 10.0f; //how long till the rockets get cleaned up
private bool _launched; //bool to check if we launched the rockets
 public List<GameObject> robots = new List<GameObject>();
 [SerializeField]
  bool inRange;
  [SerializeField]
  Transform partToRotate;
  public int warFundsNeeded { get; set; } = 100;
  [SerializeField]
  Transform target;
  float turnSpeed = 25;
  [SerializeField]
  private int _warFundsNeeded;
  [SerializeField]
  private int _upgradeCost;
  public int WarFundsNeeded { get => _warFundsNeeded; set => _warFundsNeeded = value; }
  public int UpgradeAmount { get => _upgradeCost; set => _upgradeCost = value; }
  [SerializeField]
  private GameObject _upgradeModel;
  public GameObject UpgradeModel { get { return _upgradeModel; } set { UpgradeModel = value; } }
  public GameObject CurrentTower { get; set; }
  public int TowerID { get; set; }
  [SerializeField]
  private int _towerID;
  public Vector3 TowerPlace { get; set; }

    private void Start()
    {
        CurrentTower = this.gameObject;
        build = BuildManager.Instance;
        _towerID = TowerID;
    }


    private void Update()
    {if (target != null)
            Turn();
        if (target == null)
        {
            inRange = false;
            return;
        }
        if ( inRange == true &&  _launched == false) //check for space key and if we launched the rockets
    {
            Turn();
            _launched = true; //set the launch bool to true
        StartCoroutine(FireRocketsRoutine()); //start a coroutine that fires the rockets. 
    }
      
       
    }

    
    private void ResetTartget()
    {
        target = null;

    }
    IEnumerator FireRocketsRoutine()
{
        WaitForSeconds fireYeild = new WaitForSeconds(_fireDelay);
        WaitForSeconds reloadYeild = new WaitForSeconds(_reloadTime);
        yield return null;
       
    for (int i = 0; i < _misslePositions.Length; i++) //for loop to iterate through each missle position
        {
            if (target == null)
            {
                break;
            }

            GameObject rocket = Instantiate(_missilePrefab) as GameObject; //instantiate a rocket

        rocket.transform.parent = _misslePositions[i].transform; //set the rockets parent to the missle launch position 
        rocket.transform.localPosition = Vector3.zero; //set the rocket position values to zero
        rocket.transform.localEulerAngles = new Vector3(-90, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction
        rocket.transform.parent = null; //set the rocket parent to null

        rocket.GetComponent<GameDevHQ.FileBase.Missle_Launcher.Missle.Missle>().AssignMissleRules(_launchSpeed, _power, _fuseDelay, _destroyTime); //assign missle properties 

        _misslePositions[i].SetActive(false); //turn off the rocket sitting in the turret to make it look like it fired
          
            yield return fireYeild; //wait for the firedelay
    }

    for (int i = 0; i < _misslePositions.Length; i++) //itterate through missle positions
    {
        yield return reloadYeild; //wait for reload time
        _misslePositions[i].SetActive(true); //enable fake rocket to show ready to fire
    }

    _launched = false; //set launch bool to false
}
    void Turn()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    void UpdateTarget()
    {
        // update target sort closest in list 


        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (var r in robots)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, r.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = r;
            }
            if (closestEnemy != null )
            {
                target = closestEnemy.transform;
                inRange = true;

            }
            else 
            {
                target = null;
                inRange = false;

            }

        }


    }

    private IEnumerator OnTriggerStay(Collider other)
    {

        IDamage Dam = other.GetComponent<IDamage>();
        EnemyClass enemy = other.GetComponent<EnemyClass>();
        WaitForSeconds fireYeild = new WaitForSeconds(3);


        if (other.tag == "Robot")
        {

            if (Dam != null)
                UpdateTarget();
            Dam.Damage();

            yield return fireYeild;
            if (enemy.health < 1)
            {
                inRange = false;
                target = null;
                robots.Remove(other.gameObject);

            }
        }
      

    }

    private void OnTriggerEnter(Collider other)
    {
        robots.Add(other.gameObject);

    }
    private void OnTriggerExit(Collider other)
    {
        robots.Remove(other.gameObject);

    }
    private void OnEnable()
    {
        EnemyClass.Dying += ResetTartget;
    }
    private void OnDisable()
    {
        EnemyClass.Dying -= ResetTartget;

    }
}



