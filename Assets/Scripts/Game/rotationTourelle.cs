using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationTourelle : MonoBehaviour
{
    [SerializeField] GameObject _canon = default;

    private Vector3 _target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canon != null)
        {
            _target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
           Input.mousePosition.y, Input.mousePosition.z));

            Vector3 difference = _target - _canon.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            _canon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (rotationZ - 90));
        }
    }
}
