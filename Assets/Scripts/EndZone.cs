using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using System;

public class EndZone : MonoBehaviour
{
    public static Action LivesLost;
    bool _isON;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            LivesLost?.Invoke();
           
            other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            other.gameObject.transform.position = SpawnManager.Instance.spawnPos.position;
            other.gameObject.SetActive(false);
            _isON = true;
            
            
            
           
        }
    }

    
}
