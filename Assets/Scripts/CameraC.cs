using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    [SerializeField]
    float _panSpeed =30f;
    [SerializeField]
    float _screenBorder = 10f;
    [SerializeField]
    float _scrollSpeed = 5f;
    [SerializeField]
    float _minY = 10f;
    [SerializeField]
    float _maxY = 80f;
    [SerializeField]
    Transform posx, posx2,posz,posz2;
    bool _moveCamera = true;

    // Update is called once per frame
    void Update()

    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(transform.position);
        if (Input.GetKeyDown(KeyCode.Escape))
            _moveCamera = !_moveCamera;

        if (!_moveCamera )
            return;

        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        var _direction = new Vector3();

        if (vert > 0 || Input.mousePosition.y >= Screen.height - _screenBorder)
        {
            _direction = Vector3.forward;
          
        }
        if (vert < 0 || Input.mousePosition.y <=  _screenBorder)
        {
            _direction = Vector3.back;
           
        }
        if (horz > 0  || Input.mousePosition.x >= Screen.width - _screenBorder)
        {
            _direction = Vector3.right;
           
        }
        if (horz < 0  || Input.mousePosition.x <=   _screenBorder)
        {
            _direction = Vector3.left;
           
        }
        transform.Translate(_direction * _panSpeed * Time.deltaTime, Space.World);
     float _scrollWheel =   Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= _scrollWheel * 100* _scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
        
        transform.position = pos;
        CameraLimit();
    }


    void CameraLimit()
    {
        if(transform.position.x < posx.position.x)
        {
            transform.position = new Vector3(posx.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > posx2.position.x)
        {
            transform.position = new Vector3(posx2.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z < posz.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, posz.position.z);
        }
        if (transform.position.z > posx2.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, posx2.position.z);
        }
    }
}
