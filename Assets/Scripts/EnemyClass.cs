using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyClass : MonoBehaviour,IDamage
{
    public int Health { get; set; }

    public int health;
    [SerializeField]
    protected int warFundsGiven;
    int newHealth;
    public bool isDead;
    public static Action Dying;
    public static Action NewPath;
    public GameObject explosion;
  public  Animator anim;
   
    [SerializeField]
    protected AudioSource _audioSource;
   
    [SerializeField]
    
  
    public void Start()
    {
        
        Init();
      
    }     

    public void Init()
    {
        Health = health;
        anim = GetComponent<Animator>();
        _audioSource = explosion.GetComponent<AudioSource>();
        

    }
    public virtual void Damage()
    {


        if (isDead == false)
            health--;
        if (health < 1)
        {

            isDead = true;

            StartCoroutine(Death());
          

        }




    }
    public void ResetHealth()
    {
        newHealth = 100;
        health = newHealth;
    }
   public virtual IEnumerator Death()
    {
        yield return null;
       
    }
   
}
