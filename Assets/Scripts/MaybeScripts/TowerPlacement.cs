using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
     GameObject[] _decoyTower;
    bool _placeTower;
    [SerializeField]
    GameObject[] Towers;
    PlaceTower _canPlace;
    Camera _cam;
   

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray rayOrigin = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {

            _decoyTower[0].transform.position = hitInfo.point;
            _decoyTower[1].transform.position = hitInfo.point;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _decoyTower[0].SetActive(true);
                _decoyTower[1].SetActive(true);
            }
            if (Input.GetMouseButtonDown(1))
            {
                _decoyTower[0].SetActive(false);
                _decoyTower[1].SetActive(false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.transform.gameObject.tag == "Tower" && _decoyTower[0].activeInHierarchy == true)
                {
                    
                    //check hitpoint = open spot & if it is can place it there
                    Instantiate(Towers[0], hitInfo.point, Quaternion.identity);
                    hitInfo.transform.gameObject.tag = "Upgrade";

                }
                else if (hitInfo.transform.gameObject.tag == "Tower" && _decoyTower[1].activeInHierarchy == true)
                {
                    Instantiate(Towers[1], hitInfo.point, Quaternion.identity);
                    hitInfo.transform.gameObject.tag = "Upgrade";
                }


            }

        }
    }
}

