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

        private Vector3 m_rotationVector;
        private float m_targetAngle;
        private float m_lerpSpeed;
        private bool m_startRotation = false;
        private float m_angle, m_coeff;
        private void Start() {
            m_cube = gameObject.GetComponentInParent<Cube>();
        }

        public void SetRotate(bool reverse, float lerpSpeed) {
            if(m_startRotation) return;
            m_coeff = reverse ? -1 : 1;
            m_targetAngle = m_coeff * 90;

            //m_rotationVector = transform.eulerAngles + m_rotationAxis * m_targetAngle;
            m_lerpSpeed = lerpSpeed;

            m_startRotation = true;

            m_angle = 0;
                        
        }

        public void Rotate(){
            transform.Rotate(transform.forward, m_coeff * m_lerpSpeed, Space.World);

            //transform.eulerAngles += m_coeff * m_lerpSpeed * m_rotationAxis;
            m_angle += m_coeff * m_lerpSpeed;

            if(Mathf.Abs(m_angle) >= 90) m_startRotation = false;
        }

        private void Update() {
            if(m_startRotation){
                Debug.Log("Rotating");
                Rotate();
            }
        }

        private void OnMouseDown() {
            Debug.Log($"Hit : {m_color}");
            m_cube.SelectSide(m_color);
        }

        
    }

}   
