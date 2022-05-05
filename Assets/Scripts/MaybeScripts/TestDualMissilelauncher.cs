using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.FileBase.Missle_Launcher_Dual_Turret.Missle;

public class TestDualMissilelauncher : MonoBehaviour,ITower
{
    BuildManager build;
    [SerializeField]
    private GameObject _missilePrefab; //holds the missle gameobject to clone
    [SerializeField]
    private GameObject[] _misslePositionsLeft; //array to hold the rocket positions on the turret
    [SerializeField]
    private GameObject[] _misslePositionsRight; //array to hold the rocket positions on the turret
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
    [SerializeField]
    Transform target;
    float turnSpeed = 75;

    public int WarFundsNeeded { get  ; set; }
    public int UpgradeAmount { get; set ; }

    public GameObject UpgradeModel { get; set; }

    public GameObject CurrentTower { get ; set; }
    public int TowerID { get ; set ; }
    public Vector3 TowerPlace { get ; set ; }

    private void Start()
        {
            
            build = BuildManager.Instance;
           
        }


        private void Update()
        {
            if (target != null)
                Turn();
            if (target == null)
            {
                inRange = false;
            _launched = false;
                return;
            }
            if (inRange == true && _launched == false) //check for space key and if we launched the rockets
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
        void Turn()
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        }

        IEnumerator FireRocketsRoutine()

    {
        WaitForSeconds fireYeild = new WaitForSeconds(_fireDelay);
        WaitForSeconds reloadYeild = new WaitForSeconds(_reloadTime);
        for (int i = 0; i < _misslePositionsLeft.Length; i++) //for loop to iterate through each missle position
        {

            GameObject rocketLeft = Instantiate(_missilePrefab); //instantiate a rocket
            GameObject rocketRight = Instantiate(_missilePrefab); //instantiate a rocket

            rocketLeft.transform.parent = _misslePositionsLeft[i].transform; //set the rockets parent to the missle launch position 
            rocketRight.transform.parent = _misslePositionsRight[i].transform; //set the rockets parent to the missle launch position 

            rocketLeft.transform.localPosition = Vector3.zero; //set the rocket position values to zero
            rocketRight.transform.localPosition = Vector3.zero; //set the rocket position values to zero

            rocketLeft.transform.localEulerAngles = new Vector3(0, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction
            rocketRight.transform.localEulerAngles = new Vector3(0, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction

            rocketLeft.transform.parent = null; //set the rocket parent to null
            rocketRight.transform.parent = null; //set the rocket parent to null

            rocketLeft.GetComponent<Missle>().AssignMissleRules(_launchSpeed, _power, _fuseDelay, _destroyTime); //assign missle properties 
            rocketRight.GetComponent<Missle>().AssignMissleRules(_launchSpeed, _power, _fuseDelay, _destroyTime); //assign missle properties 

            _misslePositionsLeft[i].SetActive(false); //turn off the rocket sitting in the turret to make it look like it fired
            _misslePositionsRight[i].SetActive(false); //turn off the rocket sitting in the turret to make it look like it fired

            yield return fireYeild; //wait for the firedelay
            if (target == null)
            {
                break;
            }

        }

            for (int i = 0; i < _misslePositionsLeft.Length; i++) //itterate through missle positions
        {
            yield return reloadYeild; //wait for reload time
            _misslePositionsLeft[i].SetActive(true); //enable fake rocket to show ready to fire
            _misslePositionsRight[i].SetActive(true); //enable fake rocket to show ready to fire
        }

        _launched = false; //set launch bool to false

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
                if (closestEnemy != null)
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
