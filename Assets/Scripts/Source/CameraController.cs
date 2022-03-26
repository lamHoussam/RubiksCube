using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(25, 250)] [SerializeField] private float m_speed;
    [Range(5, 20)] [SerializeField] private float m_distance;
    [SerializeField] private Transform m_lookAt;
    [SerializeField] private bool canRotate = true;
    private void Start() {
        transform.position = m_lookAt.position - (transform.forward * m_distance);
    }

    private void Update() {
        if(canRotate){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            transform.position = m_lookAt.position - (transform.forward * m_distance);
            transform.RotateAround(m_lookAt.position, transform.up, horizontal * m_speed * Time.deltaTime);
            transform.RotateAround(m_lookAt.position, transform.right, vertical * m_speed * Time.deltaTime);
        }
    }
}
