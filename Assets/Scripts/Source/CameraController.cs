using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_distance;
    [SerializeField] private Transform m_lookAt;
    [SerializeField] private bool canRotate = true;
    private void Start() {
        
    }

    private void Update() {
        if(canRotate){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            transform.position = m_lookAt.position - (transform.forward * m_distance);
            transform.RotateAround(m_lookAt.position, transform.up, horizontal * m_speed);
            transform.RotateAround(m_lookAt.position, transform.right, vertical * m_speed);
        }
    }
}
