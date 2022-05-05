using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Mech2 : EnemyClass, IDamage
{
    //public int Health { get; set; }
    //int newHealth;
    WarFunds war;
    [SerializeField]
    Transform partToRotate;
    [SerializeField]
    Transform target;
    float turnSpeed = 8;
    [SerializeField]
    bool inRange;
    public List<GameObject> towers = new List<GameObject>();
  public  List<Renderer> _renders = new List<Renderer>();
    WaitForSeconds respawnyeild;
    float respawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //   _audioSource = explosion.GetComponent<AudioSource>();
        war = WarFunds.Instance;
        //    Health = base.health;
        //    anim = GetComponent<Animator>();
        //    child = this.transform.Find("Explosion_Charging").gameObject;

        _renders = GetComponentsInChildren<Renderer>().ToList();
        respawnyeild = new WaitForSeconds(respawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        Turn();
    }
    void Turn()
    {
        Vector3 dir = target.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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
    //public void Damage()
    //{



    //    if (health < 1)
    //    {

    //        isDead = true;

    //        StartCoroutine(Death());

    //    }

    //}

    public override IEnumerator Death()
    {

        float fill = 0f;
        var ai = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        if (isDead == true)
        {

            Dying?.Invoke();
            StartCoroutine(Fade());
            explosion.SetActive(true);


            anim.Play("Death");
            //  _audioSource.Play();

            //this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            ai = false;

            yield return respawnyeild;
            explosion.SetActive(false);
            anim.WriteDefaultValues();
            this.gameObject.SetActive(false);
            SpawnManager.Instance.enemiesStillActive--;
            war.CurrentWarFunds += 50;
            this.gameObject.transform.position = SpawnManager.Instance.spawnPos.position;
            NewPath?.Invoke();
            ResetHealth();
            isDead = false;
            StopCoroutine(Fade());
            fill = 0f;
            _renders.ForEach(rend => rend.material.SetFloat("_fillAmount", fill));
        }
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

        private void OnTriggerEnter(Collider other)
        {
            towers.Add(other.gameObject);

        }
        private void OnTriggerExit(Collider other)
        {
            towers.Remove(other.gameObject);

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

}



