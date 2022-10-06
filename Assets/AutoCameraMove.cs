using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCameraMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
    }
}
