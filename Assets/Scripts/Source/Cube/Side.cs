using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube {
    public class Side : MonoBehaviour
    {
        [SerializeField] private Center center;
        public Center m_center => center;
        [SerializeField] private FaceColor color;
        [SerializeField] private Cube m_cube;
        public FaceColor m_color => color;

        [SerializeField] private Vector3 m_targetRotation;
        private float m_targetAngle;
        private float m_lerpSpeed;
        private bool m_startRotation = false;
        private float m_angle, m_coeff;
        private Vector3 m_rotationAxis;
        private void Start() {
            m_cube = gameObject.GetComponentInParent<Cube>();

            m_rotationAxis = -GetComponent<BoxCollider>().center.normalized; 
        }

        public void SetRotate(bool reverse, float lerpSpeed) {
            if(m_startRotation) return;
            m_coeff = reverse ? 1 : -1;
            m_targetAngle = m_coeff * 90;

            m_targetRotation = transform.eulerAngles + m_rotationAxis * m_targetAngle;

            Debug.Log(transform.forward);
            m_lerpSpeed = lerpSpeed;

            m_startRotation = true;
            m_cube.m_rotationLocked = true;

            m_angle = 0;

            //m_angle = transform.rotation + transform.forward * 90;
        }

        public void Rotate(){
            if(m_angle < 90){
                transform.Rotate(transform.forward, m_coeff * m_lerpSpeed * Time.deltaTime, Space.World);
                m_angle += m_coeff * m_lerpSpeed * Time.deltaTime;
            }
            
            if(Mathf.Abs(m_angle) >= 90){ 
                transform.rotation = Quaternion.Euler(m_targetRotation);
                OnStopRotation(); 
            }
        }

        private void Update() {
            if(m_startRotation) Rotate();
        }

        public void OnStopRotation(){
            Debug.Log("End");
            m_startRotation = false;
            m_cube.m_rotationLocked = false;
        }

        private void OnMouseDown() {
            Debug.Log($"Hit : {m_color}");
            m_cube.SelectSide(m_color);
        }

        private Vector3 ProdByMember(Vector3 a, Vector3 b){
            return new Vector3(
                a.x * b.x, a.y * b.y, a.z * b.z
            );
        }

        
    }

}   
