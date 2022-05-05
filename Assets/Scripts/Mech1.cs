using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations;
using System.Linq;
public class Mech1 : EnemyClass, IDamage
{
    //public int Health { get; set; }
   
     WarFunds war;
    [SerializeField]
    Transform target;
    [SerializeField]
    Transform partToRotate;
    float turnSpeed = 8;
    [SerializeField]
    bool inRange;
    public List<GameObject> towers = new List<GameObject>();
    List<Renderer> _renders = new List<Renderer>();
    [SerializeField]
    Behaviour _parental;
    WaitForSeconds respawnyeild;
    float respawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        respawnyeild = new WaitForSeconds(respawnTime);
        base.Start();
        war = WarFunds.Instance;
        _renders = GetComponentsInChildren<Renderer>().ToList();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        Turn();
    }

    void UpdateTarget()
    {
        // update target sort closest in list 


        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (var r in towers)
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

    public override IEnumerator  Death()
    {
        var ai = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        float fill = 0f;
        if (isDead == true)
        {
           
            Dying?.Invoke();
            GetComponentInChildren<ParentConstraint>().constraintActive = false;
            explosion.SetActive(true);
            anim.Play("Base Layer.Mech_1_Dying");
            StartCoroutine(Fade());
            //  _audioSource.Play();
            //this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            ai = false;
            yield return respawnyeild;

            explosion.SetActive(false);

            anim.WriteDefaultValues();
            this.gameObject.SetActive(false);
            war.CurrentWarFunds += 50;
            SpawnManager.Instance.enemiesStillActive--;
            this.gameObject.transform.position = SpawnManager.Instance.spawnPos.position;
            GetComponentInChildren<ParentConstraint>().constraintActive = true;
            NewPath?.Invoke();
            ResetHealth();
            isDead = false;
            StopCoroutine(Fade());
            fill = 0f;
            _renders.ForEach(rend => rend.material.SetFloat("_fillAmount", fill));
        }


    }
    void Turn()
    {
        Vector3 dir = target.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation);
    }

    private IEnumerator OnTriggerStay(Collider other)
    {

        //IDamage Dam = other.GetComponent<IDamage>();




        if (other.tag == "PlacedTower")
        {
            UpdateTarget();
            //if (Dam != null)
            //    UpdateTarget();
            //Dam.Damage();

            yield return null;
        }
        //if (Dam.Health < 1)
        //{
        //    towers.Remove(other.gameObject);
        //    inRange = false;
        //    target = null;

        //}

    }
    public IEnumerator Fade()

    {
        float fill = 0f;

        while (fill < 1f)
        {
           
            fill += Time.deltaTime / 3;
            _renders.ForEach(rend => rend.material.SetFloat("_fillAmount", fill));
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        towers.Add(other.gameObject);

    }
    private void OnTriggerExit(Collider other)
    {
        towers.Remove(other.gameObject);

    }

}

    


