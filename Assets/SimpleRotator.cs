using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField]
    float _sensitivity = 3;

    Vector3 _localEulerAngles = new Vector3(0, 0, 0);

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _localEulerAngles.x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivity;
            _localEulerAngles.y += Input.GetAxis("Mouse Y") * _sensitivity;
            transform.localEulerAngles = new Vector3(-Mathf.Clamp(_localEulerAngles.y, -90, 90), _localEulerAngles.x, 0);
        }
      
    }


}
