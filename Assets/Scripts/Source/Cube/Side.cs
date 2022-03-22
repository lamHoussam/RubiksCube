using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube {
    public class Side : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationAxis;
        [SerializeField] private Center center;
        public Center m_center => center;
        [SerializeField] private FaceColor color;
        [SerializeField] private Cube m_cube;
        public FaceColor m_color => color;

        private Vector3 m_rotationVector;
        private float m_targetAngle;
        private float m_lerpSpeed;
        private bool m_startRotation = false;
        private void Start() {
            m_cube = gameObject.GetComponentInParent<Cube>();
        }

        public void SetRotate(bool reverse, float lerpSpeed) {
            m_targetAngle = reverse ? -90 : 90;
            
            m_rotationVector = transform.eulerAngles + rotationAxis * m_targetAngle;
            m_lerpSpeed = lerpSpeed;

            m_startRotation = true;
            transform.eulerAngles = m_rotationVector;
        }

        private void Update() {
            if(m_startRotation){
            }
        }

        private void OnMouseDown() {
            Debug.Log($"Hit : {m_color}");
            m_cube.SelectSide(m_color);
        }

        
    }

}   
