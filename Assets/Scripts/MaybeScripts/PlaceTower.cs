using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
  public  ParticleSystem Tower;
    [SerializeField]
    
    public  bool canplace = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void OnMouseEnter()
    {
 
        var color = Tower.main;
        if (Tower != null)
        {
            
            color.startColor = new Color(0,1,0,0);
        }
    }

    public void OnMouseExit()
    {
        var color = Tower.main;
        if (Tower != null)
        {
            color.startColor = new Color(1, 0, 0, 0);
        }
    }
}
