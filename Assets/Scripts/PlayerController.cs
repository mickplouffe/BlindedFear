using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Transform _raycastPoint;
    [SerializeField]
    private float _fireDelay = 0.25f;
    [SerializeField]
    private float _fireDistance = 250f;
    private float _newFireTime;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") != 0 && Time.time >= _newFireTime)
        {
            _newFireTime = Time.time + _fireDelay;
            Debug.Log("Fire!");

            Ray ray = new Ray(_raycastPoint.position, _raycastPoint.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _fireDistance))
            {
                Debug.Log("We hit " + hitInfo.collider.name);
            }
        }    
    }
}
